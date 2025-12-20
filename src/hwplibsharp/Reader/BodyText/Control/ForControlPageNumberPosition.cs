using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// �� ��ȣ ��ġ ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public static class ForControlPageNumberPosition
    {
        /// <summary>
        /// �� ��ȣ ��ġ ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="pgnp">�� ��ȣ ��ġ ��Ʈ�� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(ControlPageNumberPosition pgnp, CompoundStreamReader sr)
        {
            CtrlHeader(pgnp.GetHeader()!, sr);
        }

        /// <summary>
        /// �� ��ȣ ��ġ ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="header">�� ��ȣ ��ġ ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void CtrlHeader(CtrlHeaderPageNumberPosition header, CompoundStreamReader sr)
        {
            header.Property.Value = sr.ReadUInt4();
            header.Number = sr.ReadUInt2();
            header.UserSymbol.Bytes = sr.ReadWChar();
            header.BeforeDecorationLetter.Bytes = sr.ReadWChar();
            header.AfterDecorationLetter.Bytes = sr.ReadWChar();
        }
    }

}