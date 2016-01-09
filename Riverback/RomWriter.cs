using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Riverback
{
    class RomWriter
    {
        private const String XML_FILENAME = "romwriterdata.xml";
        private const byte CLEAR_BYTE = 0xFF;
        public const int ROM_ORIGINAL_SIZE = 0x100000;
        public const int WRITE_LEVEL_ADDRESS = 0x100000;
        public const int WRITE_LEVEL_SEARCH_SIZE = 0x200;
        public const int EXPAND_ROM_SIZE = 0x200000;

        private XElement root;
        private byte[] romdata;

        public RomWriter(byte[] romdata)
        {
            if (romdata == null)
                throw new ArgumentNullException("The romdata argument is required for the RomWriter() class");
            this.romdata = romdata;
            root = XElement.Load(XML_FILENAME);
        }

        private bool checkEmptySpace(int pointer, int size)
        {
            for (int x = 0; x < size; x++) {
                if (romdata[pointer + x] != CLEAR_BYTE)
                    return false;
            }
            return true;
        }

        private void fillEmptySpace(int pointer, int size)
        {
            for (int x = 0; x < size; x++) {
                romdata[pointer + x] = CLEAR_BYTE;
            }
        }

        public static byte[] expandRom(byte[] romdata)
        {
            if (romdata.Length <= EXPAND_ROM_SIZE) {
                byte[] expandedRomData = new byte[EXPAND_ROM_SIZE];
                Array.ConstrainedCopy(romdata, 0, expandedRomData, 0, romdata.Length);
                for (int x = romdata.Length; x < EXPAND_ROM_SIZE; x++) {
                    expandedRomData[x] = CLEAR_BYTE;
                }
                return expandedRomData;
            }
            return romdata;
        }

        private void writeLevelHeaderPointer(LevelHeader levelHeader, ushort pointer)
        {
            byte[] data = DataFormatter.convertRomPointerToUInt16Pointer(pointer);
            Array.ConstrainedCopy(data, 
                                  0, 
                                  romdata, 
                                  levelHeader.headerPointerAddress, 
                                  LevelHeader.LEVEL_HEADER_POINTER_SIZE);
        }

        private void writeLevelHeader(LevelHeader levelHeader)
        {
            byte[] data = levelHeader.serialize();
            Array.ConstrainedCopy(data, 0, romdata, (int)levelHeader.headerAddress, (int)LevelHeader.LEVEL_HEADER_SIZE);
        }

        public void writeLevel(Level level)
        {
            byte[] data = level.serialize();

            int originalLevelPointer = 0;
            int originalLevelSize = 0;
            XElement xmlLevel = root.Element("level");
            IEnumerable<XElement> offsets =
                from el in xmlLevel.Elements("offset")
                where Convert.ToInt32((string)el.Attribute("start"), 16) == level.LevelHeader.levelPointer
                select el;
            foreach (XElement el in offsets) {
                originalLevelPointer = Convert.ToInt32((string)el.Attribute("start"), 16);
                originalLevelSize = Convert.ToInt32((string)el.Attribute("end"), 16) - originalLevelPointer + 1;
            }

            if (data.Length <= originalLevelSize) {
                writeLevelHeader(level.LevelHeader);
                Array.ConstrainedCopy(data, 0, romdata, originalLevelPointer, data.Length);
            } else {
                if (romdata.Length > ROM_ORIGINAL_SIZE) {
                    int levelPointer = WRITE_LEVEL_ADDRESS;
                    while (levelPointer < EXPAND_ROM_SIZE) {
                        if (checkEmptySpace(levelPointer, data.Length)) {
                            fillEmptySpace(level.LevelHeader.levelPointer, level.CompressedDataSize);
                            level.LevelHeader.levelPointer = levelPointer;
                            writeLevelHeader(level.LevelHeader);
                            Array.ConstrainedCopy(data, 0, romdata, levelPointer, data.Length);
                            break;
                        }
                        levelPointer += WRITE_LEVEL_SEARCH_SIZE;
                    }
                }
            }
        }
    }
}
