namespace HwpLib.Object.BodyText.Control.CtrlHeader;

using HwpLib.Object.Etc;

/// <summary>
/// 찾아보기 표식 컨트롤을 위한 컨트롤 헤더 레코드
/// </summary>
public class CtrlHeaderIndexMark : CtrlHeader
{
    /// <summary>
    /// 키워드 1
    /// </summary>
    private HWPString keyword1;

    /// <summary>
    /// 키워드 2
    /// </summary>
    private HWPString keyword2;

    /// <summary>
    /// 생성자
    /// </summary>
    public CtrlHeaderIndexMark()
        : base(ControlType.IndexMark.GetCtrlId())
    {
        keyword1 = new HWPString();
        keyword2 = new HWPString();
    }

    /// <summary>
    /// 키워드 1을 반환한다.
    /// </summary>
    public HWPString Keyword1 => keyword1;

    /// <summary>
    /// 키워드 2을 반환한다.
    /// </summary>
    public HWPString Keyword2 => keyword2;

    public override void Copy(CtrlHeader from)
    {
        CtrlHeaderIndexMark from2 = (CtrlHeaderIndexMark)from;
        keyword1.Copy(from2.keyword1);
        keyword2.Copy(from2.keyword2);
    }
}
