namespace HwpLib.Object.DocInfo.CharShape
{

    /// <summary>
    /// 강조점 종류
    /// </summary>
    public enum EmphasisSort : byte
    {
        /// <summary>없음</summary>
        None = 0,
        DotAbove = 1,
        RingAbove = 2,
        Tilde = 3,
        Caron = 4,
        Side = 5,
        Colon = 6,
        GraveAccent = 7,
        AcuteAccent = 8,
        Circumflex = 9,
        Macron = 10,
        HookAbove = 11,
        DotBelow = 12
    }

    public static class EmphasisSortExtensions
    {
        public static byte GetValue(this EmphasisSort sort) => (byte)sort;

        public static EmphasisSort FromValue(byte value) => value switch
        {
            0 => EmphasisSort.None,
            1 => EmphasisSort.DotAbove,
            2 => EmphasisSort.RingAbove,
            3 => EmphasisSort.Tilde,
            4 => EmphasisSort.Caron,
            5 => EmphasisSort.Side,
            6 => EmphasisSort.Colon,
            7 => EmphasisSort.GraveAccent,
            8 => EmphasisSort.AcuteAccent,
            9 => EmphasisSort.Circumflex,
            10 => EmphasisSort.Macron,
            11 => EmphasisSort.HookAbove,
            12 => EmphasisSort.DotBelow,
            _ => EmphasisSort.None
        };
    }

}