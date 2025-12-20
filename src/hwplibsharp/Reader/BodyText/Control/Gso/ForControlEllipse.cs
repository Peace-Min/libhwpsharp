using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach;
using HwpLib.Object.Etc;
using HwpLib.Reader.BodyText.Control.Gso.Part;


namespace HwpLib.Reader.BodyText.Control.Gso
{

    /// <summary>
    /// Ÿ�� ��Ʈ���� ������ �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForControlEllipse
    {
        /// <summary>
        /// Ÿ�� ��Ʈ���� ������ �κ��� �д´�.
        /// </summary>
        /// <param name="ellipse">Ÿ�� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadRest(ControlEllipse ellipse, CompoundStreamReader sr)
        {
            sr.ReadRecordHeader();

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ListHeader)
            {
                ellipse.CreateTextBox();
                ForTextBox.Read(ellipse.TextBox!, sr);

                if (!sr.IsImmediatelyAfterReadingHeader)
                {
                    sr.ReadRecordHeader();
                }
            }

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ShapeComponentEllipse)
            {
                ShapeComponentEllipse(ellipse.ShapeComponentEllipse, sr);
            }
        }

        /// <summary>
        /// Ÿ�� ��ü �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="sce">Ÿ�� ��ü �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentEllipse(ShapeComponentEllipse sce, CompoundStreamReader sr)
        {
            sce.Property.Value = sr.ReadUInt4();
            sce.CenterX = sr.ReadSInt4();
            sce.CenterY = sr.ReadSInt4();
            sce.Axis1X = sr.ReadSInt4();
            sce.Axis1Y = sr.ReadSInt4();
            sce.Axis2X = sr.ReadSInt4();
            sce.Axis2Y = sr.ReadSInt4();
            sce.StartX = sr.ReadSInt4();
            sce.StartY = sr.ReadSInt4();
            sce.EndX = sr.ReadSInt4();
            sce.EndY = sr.ReadSInt4();
            sce.StartX2 = sr.ReadSInt4();
            sce.StartY2 = sr.ReadSInt4();
            sce.EndX2 = sr.ReadSInt4();
            sce.EndY2 = sr.ReadSInt4();
        }
    }

}