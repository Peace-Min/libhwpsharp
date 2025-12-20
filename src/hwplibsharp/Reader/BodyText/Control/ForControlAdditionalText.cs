using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader;
using HwpLib.Object.BodyText.Control.CtrlHeader.AdditionalText;
using HwpLib.Object.DocInfo.ParaShape;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// ���� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public static class ForControlAdditionalText
    {
        /// <summary>
        /// ���� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="at">���� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(ControlAdditionalText at, CompoundStreamReader sr)
        {
            CtrlHeader(at.GetHeader()!, sr);
        }

        /// <summary>
        /// ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="h">���� ��Ʈ���� ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void CtrlHeader(CtrlHeaderAdditionalText h, CompoundStreamReader sr)
        {
            h.MainText.Bytes = sr.ReadHWPString();
            h.SubText.Bytes = sr.ReadHWPString();
            h.Position = AdditionalTextPositionExtensions.FromValue((byte)sr.ReadUInt4());
            h.Fsizeratio = sr.ReadUInt4();
            h.Option = sr.ReadUInt4();
            h.StyleId = sr.ReadUInt4();
            h.Alignment = AlignmentExtensions.FromValue((byte)sr.ReadUInt4());
        }
    }

}