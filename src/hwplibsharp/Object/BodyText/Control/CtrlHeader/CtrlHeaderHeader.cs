namespace HwpLib.Object.BodyText.Control.CtrlHeader
{

    using HwpLib.Object.BodyText.Control.CtrlHeader.Header;

    /// <summary>
    /// 머리말 컨트롤을 위한 컨트롤 헤더 레코드
    /// </summary>
    public class CtrlHeaderHeader : CtrlHeader
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public CtrlHeaderHeader()
            : base(ControlType.Header.GetCtrlId())
        {
        }

        /// <summary>
        /// 머리글이 적용될 범위(페이지 종류)
        /// </summary>
        public HeaderFooterApplyPage ApplyPage { get; set; }

        /// <summary>
        /// 생성 순서 (??)
        /// </summary>
        public int CreateIndex { get; set; }

        public override void Copy(CtrlHeader from)
        {
            CtrlHeaderHeader from2 = (CtrlHeaderHeader)from;
            ApplyPage = from2.ApplyPage;
            CreateIndex = from2.CreateIndex;
        }
    }

}