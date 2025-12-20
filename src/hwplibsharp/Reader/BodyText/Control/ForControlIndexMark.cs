using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// ã�ƺ��� ǥ�� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public static class ForControlIndexMark
    {
        /// <summary>
        /// ã�ƺ��� ǥ�� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="idxm">ã�ƺ��� ǥ�� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(ControlIndexMark idxm, CompoundStreamReader sr)
        {
            CtrlHeader(idxm.GetHeader()!, sr);
        }

        /// <summary>
        /// ã�ƺ��� ǥ�� ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="header">ã�ƺ��� ǥ�� ��Ʈ���� ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void CtrlHeader(CtrlHeaderIndexMark header, CompoundStreamReader sr)
        {
            header.Keyword1.Bytes = sr.ReadHWPString();
            header.Keyword2.Bytes = sr.ReadHWPString();
            if (!sr.IsEndOfRecord())
            {
                sr.SkipToEndRecord();
            }
        }
    }

}