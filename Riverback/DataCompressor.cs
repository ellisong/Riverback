﻿using System.Collections.Generic;
using System.Linq;

namespace Riverback
{
    public class LengthCandidate {
        public int FrontLength;
        public int BehindLength;

        public LengthCandidate(int frontLength, int behindLength)
        {
            FrontLength = frontLength;
            BehindLength = behindLength;
        }
    }

    public static class DataCompressor
    {
        public static byte[] Compress(byte[] data)
        {
            List<byte> compressedData = new List<byte>();
            int pointer = 0;
            List<byte> posByteList = new List<byte>();
            bool[] posBitList = new bool[8];
            int posBitLength = 0;
            List<byte> behind = new List<byte>();
            List<byte> front = new List<byte>();
            List<LengthCandidate> lengthCandidates = new List<LengthCandidate>();
            List<LengthCandidate> lengthResults = new List<LengthCandidate>();
            
            while (pointer < data.Length) {
                behind.Clear();
                front.Clear();
                lengthCandidates.Clear();
                lengthResults.Clear();
                if (posBitLength >= 8) {
                    posByteList.Add(DataFormatter.BitsIntoByte(posBitList));
                    posBitLength = 0;
                }

                for (int pos = 0; pos < 16; pos++) {
                    if ((pointer - pos - 1) >= 0) {
                        behind.Insert(0, data[pointer - pos - 1]);
                    } else {
                        behind.Insert(0, 0);
                        break;
                    }
                }

                for (int pos = 0; pos < 16; pos++) {
                    if ((pointer + pos) < data.Length) {
                        front.Add(data[pointer + pos]);
                        List<int> indexList = GetIndicesForSublistInList(behind, front);
                        indexList.Sort();
                        indexList.Reverse();
                        if (indexList.Count > 0) {
                            lengthCandidates.Add(new LengthCandidate(pos + 1, behind.Count - indexList[0]));
                        }
                    }
                }

                if (lengthCandidates.Count > 0) {
                    foreach (LengthCandidate candidate in lengthCandidates) {
                        int frontLength = candidate.FrontLength;
                        int behindLength = candidate.BehindLength;
                        int frontLengthCounter = frontLength;
                        List<byte> frontCandidate = front.GetRange(0, frontLengthCounter);
                        List<byte> behindCandidate = behind.GetRange(behind.Count - behindLength, behindLength);
                        while (frontLengthCounter < 0x10000) {
                            if (pointer + frontLengthCounter >= data.Length) {
                                break;
                            }
                            frontCandidate.Add(data[pointer + frontLengthCounter]);
                            if (CheckSublistRepetitionInList(frontCandidate, behindCandidate)) {
                                frontLengthCounter += 1;
                            } else {
                                break;
                            }
                        }
                        lengthResults.Add(new LengthCandidate(frontLengthCounter, behindLength));
                    }
                    var query1 = from candidate in lengthResults
                                 orderby candidate.FrontLength descending
                                 select candidate;
                    foreach (LengthCandidate candidate in query1) {
                        int frontLength = candidate.FrontLength;
                        int behindLength = candidate.BehindLength;
                        if (frontLength <= 16) {
                            var query2 = from candidate2 in lengthResults
                                         orderby ((100000 * candidate2.FrontLength) - candidate2.BehindLength) descending
                                         select candidate2;
                            foreach (LengthCandidate candidate2 in query2) {
                                frontLength = candidate2.FrontLength;
                                behindLength = candidate2.BehindLength;
                                break;
                            }
                        } else {
                            var query3 = from candidate3 in lengthResults
                                         orderby ((100000 * candidate3.FrontLength) + candidate3.BehindLength) descending
                                         select candidate3;
                            foreach (LengthCandidate candidate3 in query3) {
                                frontLength = candidate3.FrontLength;
                                behindLength = candidate3.BehindLength;
                                break;
                            }
                        }

                        if (frontLength > 255) {
                            posBitList[posBitLength++] = true;
                            byte compressedByte = (byte)(0x10 - behindLength);
                            compressedData.Add(compressedByte);
                            compressedData.Add(0);
                            compressedData.Add((byte)(frontLength - 1 & 0x00FF));
                            compressedData.Add((byte)((frontLength - 1) / 256));
                            pointer += frontLength;
                        } else if (frontLength > 16) {
                            posBitList[posBitLength++] = true;
                            byte compressedByte = (byte)(0x10 - behindLength);
                            compressedData.Add(compressedByte);
                            compressedData.Add((byte)(frontLength-1));
                            pointer += frontLength;
                        } else if (frontLength > 1) {
                            posBitList[posBitLength++] = true;
                            byte compressedByte = (byte)(0x10 - behindLength);
                            compressedByte += ((byte)((frontLength - 1 & 0x000F) << 4));
                            compressedData.Add(compressedByte);
                            pointer += frontLength;
                        } else {
                            posBitList[posBitLength++] = false;
                            compressedData.Add(data[pointer++]);
                        }
                        break;
                    }
                } else {
                    posBitList[posBitLength++] = false;
                    compressedData.Add(data[pointer++]);
                }
            }
            if (posBitLength >= 8) {
                posByteList.Add(DataFormatter.BitsIntoByte(posBitList));
                posBitLength = 0;
            }
            posBitList[posBitLength++] = true;
            while (posBitLength < 8) {
                posBitList[posBitLength++] = false;
            }
            posByteList.Add(DataFormatter.BitsIntoByte(posBitList));
            for (int x = 0; x < 4; x++) {
                compressedData.Add(0);
            }
            InsertPosBytesIntoData(ref compressedData, posByteList);
            return compressedData.ToArray();
        }

        public static byte[] Decompress(byte[] data, int offset, out int compressedSize)
        {
            int pointer = offset;
            List<byte> writtenData = new List<byte>(0x1000);
            for (int x = 0; x < 16; x++) {
                writtenData.Add(0);
            }
            bool endCondition = false;

            while (endCondition == false) {
                byte currByte = data[pointer++];
                bool[] posBitList = DataFormatter.ByteIntoBits(currByte);
                foreach (bool posBit in posBitList) {
                    currByte = data[pointer++];
                    if (posBit == false) {
                        writtenData.Add(currByte);
                    } else {
                        int totalBytes = ((currByte & 0xF0) >> 4) + 1;
                        int bytesBehind = 0x10 - (currByte & 0x0F);
                        List<byte> behindBuffer = writtenData.GetRange(writtenData.Count - bytesBehind, bytesBehind);
                        if (totalBytes == 1) {
                            totalBytes = data[pointer++] + 1;
                            if (totalBytes == 1) {
                                if ((data[pointer] == 0) && (data[pointer+1] == 0)) {
                                    endCondition = true;
                                    break;
                                }
                                totalBytes = 256 * data[pointer + 1] + data[pointer] + 1;
                                pointer += 2;
                            }
                        }
                        int writtenBytes = 0;
                        while (writtenBytes < totalBytes) {
                            foreach (byte behindByte in behindBuffer) {
                                writtenData.Add(behindByte);
                                writtenBytes += 1;
                                if (writtenBytes >= totalBytes) {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            writtenData.RemoveRange(0, 16);
            compressedSize = pointer - offset + 2;
            return writtenData.ToArray<byte>();
        }

        private static void InsertPosBytesIntoData(ref List<byte> data, List<byte> posByteList)
        {
            int pointer = 0;
            foreach (byte posByte in posByteList) {
                data.Insert(pointer, posByte);
                pointer += 1;
                bool[] posBitList = DataFormatter.ByteIntoBits(posByte);
                foreach (bool posBit in posBitList) {
                    if (posBit) {
                        if ((data[pointer] & 0xF0) == 0) {
                            pointer += 1;
                            if (data[pointer] == 0) {
                                pointer += 1;
                                if ((data[pointer] == 0) && (data[pointer + 1] == 0)) {
                                    return;
                                }
                                pointer += 1;
                            }
                        }
                    }
                    pointer += 1;
                }
            }
        }

        private static bool CheckSublistRepetitionInList(List<byte> list, List<byte> sublist)
        {
            if (sublist.Count <= 0) {
                return false;
            }
            for (int xx = 0; xx < list.Count; xx++) {
                if (list[xx] != sublist[xx % sublist.Count]) {
                    return false;
                }
            }
            return true;
        }

        private static List<int> GetIndicesForSublistInList(List<byte> list, List<byte> sublist)
        {
            List<int> indexList = new List<int>();
            if ((list.Count <= 0) || (sublist.Count <= 0)) {
                return indexList;
            }
            for (int xx = list.Count - sublist.Count; xx >= 0; xx--) {
                int count = 0;
                for (int yy = 0; yy < sublist.Count; yy++) {
                    if (list[xx + yy] == sublist[yy]) {
                        count += 1;
                    } else {
                        break;
                    }
                }
                if (count == sublist.Count) {
                    indexList.Add(xx);
                }
            }
            return indexList;
        }
    }
}
