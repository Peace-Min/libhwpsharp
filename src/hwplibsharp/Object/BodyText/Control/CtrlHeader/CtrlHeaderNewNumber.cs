namespace HwpLib.Object.BodyText.Control.CtrlHeader
{

    using HwpLib.Object.BodyText.Control.CtrlHeader.NewNumber;

    /// <summary>
    /// 새 번호 지정 컨트롤을 위한 컨트롤 헤더 레코드
    /// </summary>
    public class CtrlHeaderNewNumber : CtrlHeader
    {
        /// <summary>
        /// 속성
        /// </summary>
        private NewNumberHeaderProperty property;

        /// <summary>
        /// 생성자
        /// </summary>
        public CtrlHeaderNewNumber()
            : base(ControlType.NewNumber.GetCtrlId())
        {
            property = new NewNumberHeaderProperty();
        }

        /// <summary>
        /// 새 번호 지정 컨트롤의 속성 객체를 반환한다.
        /// </summary>
        public NewNumberHeaderProperty Property => property;

        /// <summary>
        /// 번호
        /// </summary>
        public int Number { get; set; }

        public override void Copy(CtrlHeader from)
        {
            CtrlHeaderNewNumber from2 = (CtrlHeaderNewNumber)from;
            property.Copy(from2.property);
            Number = from2.Number;
        }
    }

}