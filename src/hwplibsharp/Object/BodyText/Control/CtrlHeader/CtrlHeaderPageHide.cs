namespace HwpLib.Object.BodyText.Control.CtrlHeader
{

    using HwpLib.Object.BodyText.Control.CtrlHeader.PageHide;

    /// <summary>
    /// 감추기 컨트롤을 위한 컨트롤 헤더 레코드
    /// </summary>
    public class CtrlHeaderPageHide : CtrlHeader
    {
        /// <summary>
        /// 속성
        /// </summary>
        private PageHideHeaderProperty property;

        /// <summary>
        /// 생성자
        /// </summary>
        public CtrlHeaderPageHide()
            : base(ControlType.PageHide.GetCtrlId())
        {
            property = new PageHideHeaderProperty();
        }

        /// <summary>
        /// 감추기 컨트롤의 속성 객체를 반환한다.
        /// </summary>
        public PageHideHeaderProperty Property => property;

        public void Copy(CtrlHeaderPageHide from)
        {
            property.Copy(from.property);
        }

        public override void Copy(CtrlHeader from)
        {
            CtrlHeaderPageHide from2 = (CtrlHeaderPageHide)from;
            property.Copy(from2.property);
        }
    }

}