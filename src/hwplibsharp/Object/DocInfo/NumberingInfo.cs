using HwpLib.Object.DocInfo.Numbering;

namespace HwpLib.Object.DocInfo;

/// <summary>
/// 문단 번호 레코드
/// </summary>
public class NumberingInfo
{
    private readonly List<LevelNumbering> _levelNumberingList;
    private int _startNumber;

    public NumberingInfo()
    {
        _levelNumberingList = new List<LevelNumbering>();
        CreateLevelNumberings();
    }

    /// <summary>
    /// 수준(1～10)에 해당하는 문단 번호 정보 객체를 생성한다.
    /// 5.0.2.5 이상 부터 8~10 추가됨
    /// </summary>
    private void CreateLevelNumberings()
    {
        for (int i = 0; i < 10; i++)
        {
            _levelNumberingList.Add(new LevelNumbering());
        }
    }

    /// <summary>
    /// level에 해당하는 문단 번호 정보 객체를 반환한다.
    /// </summary>
    /// <param name="level">문단 번호 정보 객체를 얻고자 하는 수준(1~10)</param>
    /// <returns>level에 해당하는 문단 번호 정보 객체</returns>
    public LevelNumbering GetLevelNumbering(int level)
    {
        if (level >= 1 && level <= 10)
        {
            return _levelNumberingList[level - 1];
        }
        throw new ArgumentException($"invalid level : {level}");
    }

    public IReadOnlyList<LevelNumbering> LevelNumberingList => _levelNumberingList;

    public int StartNumber
    {
        get => _startNumber;
        set => _startNumber = value;
    }

    public NumberingInfo Clone()
    {
        var cloned = new NumberingInfo();
        
        for (int i = 0; i < 10; i++)
        {
            cloned._levelNumberingList[i].Copy(_levelNumberingList[i]);
        }
        
        cloned._startNumber = _startNumber;
        return cloned;
    }
}
