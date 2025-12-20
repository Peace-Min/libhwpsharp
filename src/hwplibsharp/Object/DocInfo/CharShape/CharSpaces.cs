using System;

namespace HwpLib.Object.DocInfo.CharShape
{

    /// <summary>
    /// 언어별 자간에 대한 객체
    /// </summary>
    public class CharSpaces
    {
        private readonly sbyte[] _array;

        public CharSpaces()
        {
            _array = new sbyte[7];
        }

        public sbyte[] Array => _array;

        public void SetArray(sbyte[] array)
        {
            if (array.Length != 7)
                throw new ArgumentException("array length must be 7");
            System.Array.Copy(array, _array, 7);
        }

        public sbyte Hangul
        {
            get => _array[0];
            set => _array[0] = value;
        }

        public sbyte Latin
        {
            get => _array[1];
            set => _array[1] = value;
        }

        public sbyte Hanja
        {
            get => _array[2];
            set => _array[2] = value;
        }

        public sbyte Japanese
        {
            get => _array[3];
            set => _array[3] = value;
        }

        public sbyte Other
        {
            get => _array[4];
            set => _array[4] = value;
        }

        public sbyte Symbol
        {
            get => _array[5];
            set => _array[5] = value;
        }

        public sbyte User
        {
            get => _array[6];
            set => _array[6] = value;
        }

        public void SetForAll(sbyte charSpace)
        {
            for (int i = 0; i < 7; i++)
                _array[i] = charSpace;
        }

        public void Copy(CharSpaces from)
        {
            System.Array.Copy(from._array, _array, from._array.Length);
        }
    }

}