using System;
using System.Collections.Generic;

namespace Riverback
{
    public class TileIndex
    {
        public int MaxIndexAmount { get; }
        private readonly bool[] _tileIndex;
        private readonly List<int> _bankTileIndex;

        public TileIndex(int maxIndexAmount)
        {
            MaxIndexAmount = maxIndexAmount;
            _tileIndex = new bool[maxIndexAmount];
            _bankTileIndex = new List<int>();
        }

        public bool this[int index]
        {
            get
            {
                return _tileIndex[index];
            }
            set
            {
                SetTileIndexValue(index, value);
                SortBankTileIndex();
            }
        }

        public void SetTileIndexArray(bool[] tileIndex)
        {
            if (_tileIndex.Length == tileIndex.Length) {
                _bankTileIndex.Clear();
                for (int i = 0; i < tileIndex.Length; i++) {
                    // This statement avoids an unneeded sort
                    this[i] = false;
                    SetTileIndexValue(i, tileIndex[i]);
                }
                // Should be already sorted after copying
            } else {
                throw new ArgumentException("The tileIndex parameter is not the same length as the class' tileIndex");
            }
        }

        public void SetTileIndexList(List<bool> tileIndex)
        {
            if (_tileIndex.Length == tileIndex.Count) {
                _bankTileIndex.Clear();
                for (int i = 0; i < tileIndex.Count; i++) {
                    // This statement avoids an unneeded sort
                    this[i] = false;
                    SetTileIndexValue(i, tileIndex[i]);
                }
                // Should be already sorted after copying
            } else {
                throw new ArgumentException("The tileIndex parameter is not the same length as the class' tileIndex");
            }
        }

        public int GetBankTileIndexSize()
        {
            return _bankTileIndex.Count;
        }

        public int GetBankIndex(int tileNum)
        {
            return _bankTileIndex.FindIndex(x => x == tileNum);
        }

        public int GetBankIndexTile(int index)
        {
            return _bankTileIndex[index];
        }

        private void SetTileIndexValue(int index, bool value)
        {
            if ((!_tileIndex[index]) && (value)) {
                _bankTileIndex.Add(index);
            } else if ((_tileIndex[index]) && (!value)) {
                _bankTileIndex.Remove(index);
            }
            _tileIndex[index] = value;
        }

        private void SortBankTileIndex()
        {
            _bankTileIndex.Sort();
        }
    }
}
