using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// �ڵ� ��ȣ ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public static class ForControlAutoNumber
    {
        /// <summary>
        /// �ڵ� ��ȣ ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="an">�ڵ���ȣ ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(ControlAutoNumber an, CompoundStreamReader sr)
        {
            CtrlHeader(an.GetHeader()!, sr);
        }

        /// <summary>
        /// �ڵ� ��ȣ ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="h">�ڵ� ��ȣ ��Ʈ���� ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void CtrlHeader(CtrlHeaderAutoNumber h, CompoundStreamReader sr)
        {
            h.Property.Value = sr.ReadUInt4();
            h.Number = sr.ReadUInt2();
            h.UserSymbol.Bytes = sr.ReadWChar();
            h.BeforeDecorationLetter.Bytes = sr.ReadWChar();
            h.AfterDecorationLetter.Bytes = sr.ReadWChar();
        }
    }

}