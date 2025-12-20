using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// �� ��ȣ ���� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public static class ForControlNewNumber
    {
        /// <summary>
        /// �� ��ȣ ���� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="nwno">�� ��ȣ ���� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(ControlNewNumber nwno, CompoundStreamReader sr)
        {
            CtrlHeader(nwno.GetHeader()!, sr);
        }

        /// <summary>
        /// �� ��ȣ ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="header">�� ��ȣ ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void CtrlHeader(CtrlHeaderNewNumber header, CompoundStreamReader sr)
        {
            header.Property.Value = sr.ReadUInt4();
            header.Number = sr.ReadUInt2();
        }
    }

}