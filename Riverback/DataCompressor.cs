using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Riverback
{
    public class DataCompressor
    {
        public byte[] compress(byte[] data)
        {
            return null;
        }

        public byte[] decompress(byte[] data, uint offset)
        {
            return null;
        }

        private static void insertPosBytesIntoData(ref List<byte> data, List<byte> posByteList)
        {
            int pointer = 0;
            foreach (byte posByte in posByteList) {
                data.Insert(pointer, posByte);
                pointer += 1;
                bool[] posBitList = DataFormatter.byteIntoBits(posByte);
                foreach (bool posBit in posBitList) {
                    if (posBit) {
                        if ((data[pointer] & 0xF0) == 0) {
                            pointer += 1;
                            if (data[pointer] == 0) {
                                pointer += 1;
                                if ((data[pointer] == 0) && (data[pointer + 1] == 0))
                                    return;
                                pointer += 1;
                            }
                        }
                    }
                    pointer += 1;
                }
            }
        }

        private static bool checkSublistRepetitionInList(List<byte> list, List<byte> sublist)
        {
            for (int xx = 0; xx < list.Count; xx += 1) {
                if (list[xx] != sublist[xx % sublist.Count])
                    return false;
            }
            return true;
        }

        private static List<uint> getIndicesForSublistInList(List<byte> list, List<byte> sublist)
        {
            List<uint> indexList = new List<uint>();
            for (int xx = list.Count - sublist.Count; xx >= 0; xx -= 1) {
                int count = 0;
                for (int yy = 0; yy < sublist.Count; yy += 1) {
                    if (list[xx + yy] == sublist[yy]) {
                        count += 1;
                    } else {
                        break;
                    }
                }
                if (count == sublist.Count)
                    indexList.Add((uint)xx);
            }
            return indexList;
        }
    }
}
