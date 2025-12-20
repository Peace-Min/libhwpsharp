namespace HwpLib.Object.BodyText.Control.CtrlHeader
{

    /// <summary>
    /// 기본 컨트롤 헤더 레코드
    /// </summary>
    public class CtrlHeaderDefault : CtrlHeader
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="ctrlId">컨트롤 아이디</param>
        public CtrlHeaderDefault(uint ctrlId)
            : base(ctrlId)
        {
        }

        public override void Copy(CtrlHeader from)
        {
            // nothing
        }
    }

}