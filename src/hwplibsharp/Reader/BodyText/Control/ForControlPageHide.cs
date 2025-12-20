using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// ���߱� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public static class ForControlPageHide
    {
        /// <summary>
        /// ���߱� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="pghd">���߱� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(ControlPageHide pghd, CompoundStreamReader sr)
        {
            pghd.GetHeader()!.Property.Value = sr.ReadUInt4();
        }
    }

}