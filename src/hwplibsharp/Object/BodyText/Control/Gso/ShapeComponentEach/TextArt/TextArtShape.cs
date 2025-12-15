namespace HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach.TextArt;

/// <summary>
/// 글맵시 모양
/// </summary>
public enum TextArtShape : byte
{
    Parallelogram = 0,
    InvertedParallelogram = 1,
    InvertedUpwardCascade = 2,
    InvertedDownwardCascade = 3,
    UpwardCascade = 4,
    DownwardCascade = 5,
    ReduceRight = 6,
    ReduceLeft = 7,
    IsoscelesTrapezoid = 8,
    InvertedIsoscelesTrapezoid = 9,
    TopRibbonRectangle = 10,
    BottomRibbonRectangle = 11,
    ChevronDown = 12,
    Chevron = 13,
    BowTie = 14,
    Hexagon = 15,
    Wave1 = 16,
    Wave2 = 17,
    Wave3 = 18,
    Wave4 = 19,
    LeftTiltCylinder = 20,
    RightTiltCylinder = 21,
    BottomWideCylinder = 22,
    TopWideCylinder = 23,
    ThinCurveUp1 = 24,
    ThinCurveUp2 = 25,
    ThinCurveDown1 = 26,
    ThinCurveDown2 = 27,
    InversedFingernail = 28,
    Fingernail = 29,
    GinkoLeaf1 = 30,
    GinkoLeaf2 = 31,
    InflateRight = 32,
    InflateLeft = 33,
    InflateUpConvex = 34,
    InflateBottomConvex = 35,
    DeflateTop = 36,
    DeflateBottom = 37,
    Deflate = 38,
    Inflate = 39,
    InflateTop = 40,
    InflateBottom = 41,
    Rectangle = 42,
    LeftCylinder = 43,
    Cylinder = 44,
    RightCylinder = 45,
    Circle = 46,
    CurveDown = 47,
    ArchUp = 48,
    ArchDown = 49,
    SingleLineCircle1 = 50,
    SingleLineCircle2 = 51,
    TripleLineCircle1 = 52,
    TripleLineCircle2 = 53,
    DoubleLineCircle = 54,
}

/// <summary>
/// TextArtShape 확장 메서드
/// </summary>
public static class TextArtShapeExtensions
{
    /// <summary>
    /// 파일에 저장되는 정수값을 반환한다.
    /// </summary>
    public static byte GetValue(this TextArtShape shape)
        => (byte)shape;

    /// <summary>
    /// 파일에 저장되는 정수값에 해당되는 enum 값을 반환한다.
    /// </summary>
    public static TextArtShape FromValue(byte value)
    {
        if (Enum.IsDefined(typeof(TextArtShape), value))
        {
            return (TextArtShape)value;
        }
        return TextArtShape.Parallelogram;
    }
}
