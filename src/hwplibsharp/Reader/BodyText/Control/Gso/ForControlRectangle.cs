// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/gso/ForControlRectangle.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach;
using HwpLib.Object.Etc;
using HwpLib.Reader.BodyText.Control.Gso.Part;


namespace HwpLib.Reader.BodyText.Control.Gso
{

    /// <summary>
    /// �簢�� ��Ʈ���� ������ �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForControlRectangle
    {
        /// <summary>
        /// �簢�� ��Ʈ���� ������ �κ��� �д´�.
        /// </summary>
        /// <param name="rectangle">�簢�� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadRest(ControlRectangle rectangle, CompoundStreamReader sr)
        {
            sr.ReadRecordHeader();

            if (sr.CurrentRecordHeader?.TagId == HWPTag.CtrlData)
            {
                rectangle.CreateCtrlData();
                var ctrlData = ForCtrlData.Read(sr);
                rectangle.SetCtrlData(ctrlData);

                if (!sr.IsImmediatelyAfterReadingHeader)
                {
                    sr.ReadRecordHeader();
                }
            }

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ListHeader)
            {
                rectangle.CreateTextBox();
                ForTextBox.Read(rectangle.TextBox!, sr);

                if (!sr.IsImmediatelyAfterReadingHeader)
                {
                    sr.ReadRecordHeader();
                }
            }

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ShapeComponentRectangle)
            {
                ShapeComponentRectangle(rectangle.ShapeComponentRectangle, sr);
            }
        }

        /// <summary>
        /// �簢�� ��ü �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="scr">�簢�� ��ü �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentRectangle(ShapeComponentRectangle scr, CompoundStreamReader sr)
        {
            scr.RoundRate = sr.ReadUInt1();
            scr.X1 = sr.ReadSInt4();
            scr.Y1 = sr.ReadSInt4();
            scr.X2 = sr.ReadSInt4();
            scr.Y2 = sr.ReadSInt4();
            scr.X3 = sr.ReadSInt4();
            scr.Y3 = sr.ReadSInt4();
            scr.X4 = sr.ReadSInt4();
            scr.Y4 = sr.ReadSInt4();
        }
    }

}