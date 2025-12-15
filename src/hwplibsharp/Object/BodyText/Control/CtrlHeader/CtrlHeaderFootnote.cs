namespace HwpLib.Object.BodyText.Control.CtrlHeader;

using HwpLib.Object.BodyText.Control.SectionDefine;
using HwpLib.Object.Etc;

/// <summary>
/// 각주(Foot Note) 컨트롤을 위한 컨트롤 헤더 레코드
/// </summary>
public class CtrlHeaderFootnote : CtrlHeader
{
    /// <summary>
    /// 앞 장식 문자
    /// </summary>
    private HWPString beforeDecorationLetter;

    /// <summary>
    /// 뒤 장식 문자
    /// </summary>
    private HWPString afterDecorationLetter;

    /// <summary>
    /// 생성자
    /// </summary>
    public CtrlHeaderFootnote()
        : base(ControlType.Footnote.GetCtrlId())
    {
        beforeDecorationLetter = new HWPString();
        afterDecorationLetter = new HWPString();
    }

    /// <summary>
    /// 각주 번호
    /// </summary>
    public uint Number { get; set; }

    /// <summary>
    /// 앞 장식 문자를 반환한다.
    /// </summary>
    public HWPString BeforeDecorationLetter => beforeDecorationLetter;

    /// <summary>
    /// 뒤 장식 문자를 반환한다.
    /// </summary>
    public HWPString AfterDecorationLetter => afterDecorationLetter;

    /// <summary>
    /// 번호 모양
    /// </summary>
    public NumberShape NumberShape { get; set; }

    /// <summary>
    /// 문서 내 각 개체에 대한 고유 아이디
    /// </summary>
    public uint InstanceId { get; set; }

    public override void Copy(CtrlHeader from)
    {
        CtrlHeaderFootnote from2 = (CtrlHeaderFootnote)from;
        Number = from2.Number;
        beforeDecorationLetter.Copy(from2.beforeDecorationLetter);
        afterDecorationLetter.Copy(from2.afterDecorationLetter);
        NumberShape = from2.NumberShape;
        InstanceId = from2.InstanceId;
    }
}
