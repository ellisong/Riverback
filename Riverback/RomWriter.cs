using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Riverback.Properties;

namespace Riverback
{
    class RomWriter
    {
        private const string Checksum = "UMIKAWLEVEL";
        private const string XmlFilename = "romwriterdata.xml";
        private const byte ClearByte = 0xFF;
        private const int RomOriginalSize = 0x100000;
        private const int WriteLevelAddress = 0x100000;
        private const int WriteLevelSearchSize = 0x100;
        private const int ExpandRomSize = 0x200000;
        private const int ChecksumSize = 11;
        private const int ImportLevelLength = LevelDataSize + LevelHeader.LevelHeaderSize + ChecksumSize;
        private const int LevelDataSize = Level.LevelTileAmount * 3 + Level.LevelTileIndexSize + Level.LevelPaletteIndexAmount;

        private readonly XElement _root;
        private readonly byte[] _romdata;

        public RomWriter(byte[] romdata)
        {
            if (romdata == null) {
                throw new ArgumentNullException("The romdata argument is required for the RomWriter() class");
            }
            _romdata = romdata;
            _root = XElement.Load(XmlFilename);
        }

        private bool CheckEmptySpace(int pointer, int size)
        {
            for (int x = 0; x < size; x++) {
                if (_romdata[pointer + x] != ClearByte) {
                    return false;
                }
            }
            return true;
        }

        private void FillEmptySpace(int pointer, int size)
        {
            for (int x = 0; x < size; x++) {
                _romdata[pointer + x] = ClearByte;
            }
        }

        public static byte[] ExpandRom(byte[] romdata)
        {
            if (romdata.Length <= ExpandRomSize) {
                byte[] expandedRomData = new byte[ExpandRomSize];
                Array.ConstrainedCopy(romdata, 0, expandedRomData, 0, romdata.Length);
                for (int x = romdata.Length; x < ExpandRomSize; x++) {
                    expandedRomData[x] = ClearByte;
                }
                return expandedRomData;
            }
            return romdata;
        }

        //private void WriteLevelHeaderPointer(LevelHeader levelHeader, ushort pointer)
        //{
        //    byte[] data = DataFormatter.ConvertRomPointerToUInt16Pointer(pointer);
        //    Array.ConstrainedCopy(data, 
        //                          0, 
        //                          _romdata, 
        //                          levelHeader.HeaderPointerAddress,
        //                          2);
        //}

        private void WriteLevelHeader(LevelHeader levelHeader)
        {
            byte[] data = levelHeader.Serialize();
            Array.ConstrainedCopy(data, 0, _romdata, levelHeader.HeaderAddress, LevelHeader.LevelHeaderSize);
        }

        public byte[] ExportLevel(LevelHeader levelHeader, Level level)
        {
            List<byte> data = new List<byte>();
            data.AddRange(Encoding.ASCII.GetBytes(Checksum));
            data.AddRange(levelHeader.Serialize());
            data.AddRange(level.Serialize(false));
            return data.ToArray();
        }

        public bool ImportLevel(byte[] data, Level level, LevelHeader levelHeader)
        {
            if (data.Length != ImportLevelLength) {
                MessageBox.Show(Resources.RomWriter_ImportLevel_InvalidLevel,
                                Resources.RomWriter_ImportLevel_Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return false;
            }
            int offset = 0;

            byte[] str = Encoding.ASCII.GetBytes(Checksum);
            byte[] checksum = new byte[ChecksumSize];
            Array.ConstrainedCopy(data, offset, checksum, 0, ChecksumSize);
            if (str.SequenceEqual(checksum) == false) {
                MessageBox.Show(Resources.RomWriter_ImportLevel_InvalidLevel,
                                Resources.RomWriter_ImportLevel_Error,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return false;
            }

            offset += ChecksumSize;
            byte[] header = new byte[LevelHeader.LevelHeaderSize];
            Array.ConstrainedCopy(data, offset, header, 0, LevelHeader.LevelHeaderSize);
            levelHeader.Deserialize(header, 0);

            offset += LevelHeader.LevelHeaderSize;

            byte[] levelData = new byte[LevelDataSize];
            Array.ConstrainedCopy(data, offset, levelData, 0, LevelDataSize);
            level.Update(levelData);

            return true;
        }

        public void WriteLevel(Level level, LevelHeader levelHeader)
        {
            byte[] data = level.Serialize();

            level.LevelHeader = levelHeader;
            int originalLevelPointer = 0;
            int originalLevelSize = 0;
            XElement xmlLevel = _root.Element("level");
            if (xmlLevel != null)
            {
                var offsets = from el in xmlLevel.Elements("offset")
                              select el;
                foreach (XElement el in offsets) {
                    if (Convert.ToInt32((string)el.Attribute("number"), 16) == level.LevelHeader.HeaderNumber) {
                        originalLevelPointer = Convert.ToInt32((string)el.Attribute("start"), 16);
                        originalLevelSize = Convert.ToInt32((string)el.Attribute("end"), 16) - originalLevelPointer + 1;
                        break;
                    }
                }
            }
            
            if (data.Length <= originalLevelSize) {
                FillEmptySpace(originalLevelPointer, originalLevelSize);
                if (level.LevelHeader.LevelPointer >= WriteLevelAddress) {
                    FillEmptySpace(level.LevelHeader.LevelPointer, level.CompressedDataSize);
                }
                level.LevelHeader.LevelPointer = originalLevelPointer;
                WriteLevelHeader(level.LevelHeader);
                Array.ConstrainedCopy(data, 0, _romdata, originalLevelPointer, data.Length);
            } else if (_romdata.Length > RomOriginalSize) {
                int levelPointer = WriteLevelAddress;
                while (levelPointer < ExpandRomSize) {
                    if (CheckEmptySpace(levelPointer, data.Length)) {
                        if (level.LevelHeader.LevelPointer >= WriteLevelAddress) {
                            FillEmptySpace(level.LevelHeader.LevelPointer, level.CompressedDataSize);
                        }
                        level.LevelHeader.LevelPointer = levelPointer;
                        WriteLevelHeader(level.LevelHeader);
                        Array.ConstrainedCopy(data, 0, _romdata, levelPointer, data.Length);
                        break;
                    }
                    levelPointer += WriteLevelSearchSize;
                }
            }
        }
    }
}
