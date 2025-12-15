namespace HwpLib.Object.BodyText.Control.CtrlHeader;

/// <summary>
/// 책갈피 컨트롤을 위한 컨트롤 헤더 레코드
/// </summary>
public class CtrlHeaderBookmark : CtrlHeader
{
    /// <summary>
    /// 생성자
    /// </summary>
    public CtrlHeaderBookmark()
        : base(ControlType.Bookmark.GetCtrlId())
    {
    }

    public override void Copy(CtrlHeader from)
    {
    }
}
