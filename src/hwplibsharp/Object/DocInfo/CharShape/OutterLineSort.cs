namespace HwpLib.Object.DocInfo.CharShape;

/// <summary>
/// 외곽선 종류
/// </summary>
public enum OutterLineSort : byte
{
    /// <summary>없음</summary>
    None = 0,
    /// <summary>실선</summary>
    Solid = 1,
    /// <summary>점선</summary>
    Dot = 2,
    /// <summary>굵은 실선(두꺼운 선)</summary>
    BoldSolid = 3,
    /// <summary>쇄선(긴 점선)</summary>
    Dash = 4,
    /// <summary>일점쇄선 (-.-.-.-.)</summary>
    DashDot = 5,
    /// <summary>이점쇄선 (-..-..-..)</summary>
    DashDotDot = 6
}

public static class OutterLineSortExtensions
{
    public static byte GetValue(this OutterLineSort sort) => (byte)sort;

    public static OutterLineSort FromValue(byte value) => value switch
    {
        0 => OutterLineSort.None,
        1 => OutterLineSort.Solid,
        2 => OutterLineSort.Dot,
        3 => OutterLineSort.BoldSolid,
        4 => OutterLineSort.Dash,
        5 => OutterLineSort.DashDot,
        6 => OutterLineSort.DashDotDot,
        _ => OutterLineSort.None
    };
}
