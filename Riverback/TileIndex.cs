using System;
using System.Collections.Generic;

namespace Riverback
{
    public class TileIndex
    {
        private int maxIndexAmount;
        public int MaxIndexAmount { get { return maxIndexAmount; } }
        private bool[] tileIndex;
        private List<int> bankTileIndex;

        public TileIndex(int maxIndexAmount)
        {
            this.maxIndexAmount = maxIndexAmount;
            tileIndex = new bool[maxIndexAmount];
            bankTileIndex = new List<int>();
        }

        public bool this[int index]
        {
            get
            {
                return tileIndex[index];
            }
            set
            {
                setTileIndexValue(index, value);
                sortBankTileIndex();
            }
        }

        public void setTileIndexArray(bool[] tileIndex)
        {
            if (this.tileIndex.Length == tileIndex.Length) {
                bankTileIndex.Clear();
                for (int i = 0; i < tileIndex.Length; i++) {
                    // This statement avoids an unneeded sort
                    this[i] = false;
                    setTileIndexValue(i, tileIndex[i]);
                }
                // Should be already sorted after copying
            } else {
                throw new ArgumentException("The tileIndex parameter is not the same length as the class' tileIndex");
            }
        }

        public void setTileIndexList(List<bool> tileIndex)
        {
            if (this.tileIndex.Length == tileIndex.Count) {
                bankTileIndex.Clear();
                for (int i = 0; i < tileIndex.Count; i++) {
                    // This statement avoids an unneeded sort
                    this[i] = false;
                    setTileIndexValue(i, tileIndex[i]);
                }
                // Should be already sorted after copying
            } else {
                throw new ArgumentException("The tileIndex parameter is not the same length as the class' tileIndex");
            }
        }

        public int getBankTileIndexSize()
        {
            return bankTileIndex.Count;
        }

        public int getBankIndex(int tileNum)
        {
            return bankTileIndex.FindIndex(x => x == tileNum);
        }

        public int getBankIndexTile(int index)
        {
            return bankTileIndex[index];
        }

        private void setTileIndexValue(int index, bool value)
        {
            if ((!tileIndex[index]) && (value)) {
                bankTileIndex.Add(index);
            } else if ((tileIndex[index]) && (!value)) {
                bankTileIndex.Remove(index);
            }
            tileIndex[index] = value;
        }

        private void sortBankTileIndex()
        {
            bankTileIndex.Sort();
        }
    }
}
