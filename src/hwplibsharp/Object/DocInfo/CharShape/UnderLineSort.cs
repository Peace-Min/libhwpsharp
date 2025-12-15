namespace HwpLib.Object.DocInfo.CharShape;

/// <summary>
/// 밑줄 종류
/// </summary>
public enum UnderLineSort : byte
{
    /// <summary>없음</summary>
    None = 0,
    /// <summary>글자 아래</summary>
    Bottom = 1,
    /// <summary>글자 가운데</summary>
    Middle = 2,
    /// <summary>글자 위</summary>
    Top = 3
}

public static class UnderLineSortExtensions
{
    public static byte GetValue(this UnderLineSort sort) => (byte)sort;

    public static UnderLineSort FromValue(byte value) => value switch
    {
        0 => UnderLineSort.None,
        1 => UnderLineSort.Bottom,
        2 => UnderLineSort.Middle,
        3 => UnderLineSort.Top,
        _ => UnderLineSort.None
    };
}
