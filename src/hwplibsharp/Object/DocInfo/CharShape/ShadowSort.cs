namespace HwpLib.Object.DocInfo.CharShape;

/// <summary>
/// 그림자 종류
/// </summary>
public enum ShadowSort : byte
{
    /// <summary>없음</summary>
    None = 0,
    /// <summary>비연속</summary>
    Discontinuous = 1,
    /// <summary>연속</summary>
    Continuous = 2
}

public static class ShadowSortExtensions
{
    public static byte GetValue(this ShadowSort sort) => (byte)sort;

    public static ShadowSort FromValue(byte value) => value switch
    {
        0 => ShadowSort.None,
        1 => ShadowSort.Discontinuous,
        2 => ShadowSort.Continuous,
        _ => ShadowSort.None
    };
}
