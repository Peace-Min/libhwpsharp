namespace HwpLib.Object.BodyText.Control.CtrlHeader;

using HwpLib.Object.BodyText.Control.CtrlHeader.Header;

/// <summary>
/// 꼬리말 컨트롤을 위한 컨트롤 헤더 레코드
/// </summary>
public class CtrlHeaderFooter : CtrlHeader
{
    /// <summary>
    /// 생성자
    /// </summary>
    public CtrlHeaderFooter()
        : base(ControlType.Footer.GetCtrlId())
    {
    }

    /// <summary>
    /// 꼬리말이 적용될 범위(페이지 종류)
    /// </summary>
    public HeaderFooterApplyPage ApplyPage { get; set; }

    /// <summary>
    /// 생성 순서 (??)
    /// </summary>
    public int CreateIndex { get; set; }

    public void Copy(CtrlHeaderFooter from)
    {
        ApplyPage = from.ApplyPage;
        CreateIndex = from.CreateIndex;
    }

    public override void Copy(CtrlHeader from)
    {
        CtrlHeaderFooter from2 = (CtrlHeaderFooter)from;
        ApplyPage = from2.ApplyPage;
        CreateIndex = from2.CreateIndex;
    }
}
