// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/gso/ForControlPolygon.java
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
    /// �ٰ��� ��Ʈ���� ������ �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForControlPolygon
    {
        /// <summary>
        /// �ٰ��� ��Ʈ���� ������ �κ��� �д´�.
        /// </summary>
        /// <param name="polygon">�ٰ��� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadRest(ControlPolygon polygon, CompoundStreamReader sr)
        {
            sr.ReadRecordHeader();

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ListHeader)
            {
                polygon.CreateTextBox();
                ForTextBox.Read(polygon.TextBox!, sr);

                if (!sr.IsImmediatelyAfterReadingHeader)
                {
                    sr.ReadRecordHeader();
                }
            }

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ShapeComponentPolygon)
            {
                ShapeComponentPolygon(polygon.ShapeComponentPolygon, sr);
            }
        }

        /// <summary>
        /// �ٰ��� ��ü �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="scp">�ٰ��� ��ü �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentPolygon(ShapeComponentPolygon scp, CompoundStreamReader sr)
        {
            int positionCount = sr.ReadSInt4();
            for (int index = 0; index < positionCount; index++)
            {
                var p = scp.AddNewPosition();
                p.X = (uint)sr.ReadSInt4();
                p.Y = (uint)sr.ReadSInt4();
            }
            if (!sr.IsEndOfRecord())
            {
                sr.SkipToEndRecord();
            }
        }
    }

}