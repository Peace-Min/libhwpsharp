// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/gso/ForControlLine.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach;
using HwpLib.Object.Etc;


namespace HwpLib.Reader.BodyText.Control.Gso
{

    /// <summary>
    /// �� ��Ʈ���� ������ �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForControlLine
    {
        /// <summary>
        /// �� ��Ʈ���� ������ �κ��� �д´�.
        /// </summary>
        /// <param name="line">�� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadRest(ControlLine line, CompoundStreamReader sr)
        {
            sr.ReadRecordHeader();
            if (sr.CurrentRecordHeader?.TagId == HWPTag.ShapeComponentLine)
            {
                ShapeComponentLine(line.ShapeComponentLine, sr);
            }
        }

        /// <summary>
        /// �� ��ü �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="scl">�� ��ü �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentLine(ShapeComponentLine scl, CompoundStreamReader sr)
        {
            scl.StartX = sr.ReadSInt4();
            scl.StartY = sr.ReadSInt4();
            scl.EndX = sr.ReadSInt4();
            scl.EndY = sr.ReadSInt4();
            int temp = sr.ReadSInt4();
            scl.IsStartedRightOrBottom = (temp == 1);
        }
    }

}