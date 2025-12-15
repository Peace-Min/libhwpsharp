namespace HwpLib.Object.BodyText.Control.CtrlHeader;

using HwpLib.Object.BodyText.Control.CtrlHeader.PageOddEvenAdjust;

/// <summary>
/// 홀/짝수 조정(페이지 번호 제어) 컨트롤을 위한 컨트롤 헤더 레코드
/// </summary>
public class CtrlHeaderPageOddEvenAdjust : CtrlHeader
{
    /// <summary>
    /// 속성
    /// </summary>
    private PageOddEvenAdjustHeaderProperty property;

    /// <summary>
    /// 생성자
    /// </summary>
    public CtrlHeaderPageOddEvenAdjust()
        : base(ControlType.PageOddEvenAdjust.GetCtrlId())
    {
        property = new PageOddEvenAdjustHeaderProperty();
    }

    /// <summary>
    /// 홀/짝수 조정(페이지 번호 제어) 컨트롤의 속성 객체를 반환한다.
    /// </summary>
    public PageOddEvenAdjustHeaderProperty Property => property;

    public void Copy(CtrlHeaderPageOddEvenAdjust from)
    {
        property.Copy(from.property);
    }

    public override void Copy(CtrlHeader from)
    {
        CtrlHeaderPageOddEvenAdjust from2 = (CtrlHeaderPageOddEvenAdjust)from;
        property.Copy(from2.property);
    }
}
