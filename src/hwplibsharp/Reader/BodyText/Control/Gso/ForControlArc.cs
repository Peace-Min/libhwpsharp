using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach.Arc;
using HwpLib.Object.Etc;
using HwpLib.Reader.BodyText.Control.Gso.Part;


namespace HwpLib.Reader.BodyText.Control.Gso
{

    /// <summary>
    /// ȣ ��Ʈ���� ������ �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForControlArc
    {
        /// <summary>
        /// ȣ ��Ʈ���� ������ �κ��� �д´�.
        /// </summary>
        /// <param name="arc">ȣ ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadRest(ControlArc arc, CompoundStreamReader sr)
        {
            sr.ReadRecordHeader();

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ListHeader)
            {
                arc.CreateTextBox();
                ForTextBox.Read(arc.TextBox!, sr);

                if (!sr.IsImmediatelyAfterReadingHeader)
                {
                    sr.ReadRecordHeader();
                }
            }

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ShapeComponentArc)
            {
                ShapeComponentArc(arc.ShapeComponentArc, sr);
            }
        }

        /// <summary>
        /// ȣ ��ü �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="sca">ȣ ��ü �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentArc(ShapeComponentArc sca, CompoundStreamReader sr)
        {
            sca.ArcType = ArcTypeExtensions.FromValue(sr.ReadUInt1());
            sca.CenterX = sr.ReadSInt4();
            sca.CenterY = sr.ReadSInt4();
            sca.Axis1X = sr.ReadSInt4();
            sca.Axis1Y = sr.ReadSInt4();
            sca.Axis2X = sr.ReadSInt4();
            sca.Axis2Y = sr.ReadSInt4();
        }
    }

}