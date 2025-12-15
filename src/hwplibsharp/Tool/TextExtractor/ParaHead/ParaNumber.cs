namespace HwpLib.Tool.TextExtractor.ParaHead
{
    /// <summary>
    /// 문단 번호 관리 클래스
    /// </summary>
    public class ParaNumber
    {
        private const int LevelCount = 10;
        
        private int[] _levelNumbers;
        private int _currentHeadID;

        public ParaNumber()
        {
            _levelNumbers = new int[LevelCount];
            _currentHeadID = -1;
        }

        public bool ChangedParaHead(int headID)
        {
            return _currentHeadID != headID;
        }

        public void Reset(int headID, int level, int valueForLevel1)
        {
            _levelNumbers[0] = valueForLevel1;
            for (int level2 = 1; level2 < LevelCount; level2++)
            {
                if (level >= level2)
                {
                    _levelNumbers[level2] = 1;
                }
                else
                {
                    _levelNumbers[level2] = 0;
                }
            }
            _currentHeadID = headID;
        }

        public int Value(int level)
        {
            return _levelNumbers[level];
        }

        public void Increase(int level)
        {
            for (int level2 = 0; level2 < LevelCount; level2++)
            {
                if (level2 < level)
                {
                    if (_levelNumbers[level2] == 0)
                    {
                        _levelNumbers[level2] = 1;
                    }
                }
                else if (level2 > level)
                {
                    _levelNumbers[level2] = 0;
                }
                else
                {
                    _levelNumbers[level2]++;
                }
            }
        }
    }
}
