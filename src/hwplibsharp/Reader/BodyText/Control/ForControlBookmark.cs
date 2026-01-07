// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/ForControlBookmark.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.Etc;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// å���� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public static class ForControlBookmark
    {
        /// <summary>
        /// å���� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="b">å���� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(ControlBookmark b, CompoundStreamReader sr)
        {
            CtrlData(b, sr);
        }

        /// <summary>
        /// ��Ʈ�� ������ ���ڵ带 �д´�.
        /// </summary>
        /// <param name="b">å���� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void CtrlData(ControlBookmark b, CompoundStreamReader sr)
        {
            sr.ReadRecordHeader();
            if (sr.CurrentRecordHeader?.TagId == HWPTag.CtrlData)
            {
                b.CreateCtrlData();
                var ctrlData = ForCtrlData.Read(sr);
                b.SetCtrlData(ctrlData);
            }
        }
    }

}