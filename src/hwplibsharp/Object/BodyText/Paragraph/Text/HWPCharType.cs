namespace HwpLib.Object.BodyText.Paragraph.Text;

/// <summary>
/// 한글(HWP) 글자의 종류
/// </summary>
public enum HWPCharType
{
    /// <summary>
    /// 일반
    /// </summary>
    Normal,

    /// <summary>
    /// 문자 컨트롤
    /// </summary>
    ControlChar,

    /// <summary>
    /// 인라인 컨트롤
    /// </summary>
    ControlInline,

    /// <summary>
    /// 확장 컨트롤
    /// </summary>
    ControlExtend,
}
