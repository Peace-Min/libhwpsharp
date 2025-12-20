using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach.Curve;
using HwpLib.Object.Etc;
using HwpLib.Reader.BodyText.Control.Gso.Part;


namespace HwpLib.Reader.BodyText.Control.Gso
{

    /// <summary>
    /// � ��Ʈ���� ������ �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForControlCurve
    {
        /// <summary>
        /// � ��Ʈ���� ������ �κ��� �д´�.
        /// </summary>
        /// <param name="curve">� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadRest(ControlCurve curve, CompoundStreamReader sr)
        {
            sr.ReadRecordHeader();

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ListHeader)
            {
                curve.CreateTextBox();
                ForTextBox.Read(curve.TextBox!, sr);

                if (!sr.IsImmediatelyAfterReadingHeader)
                {
                    sr.ReadRecordHeader();
                }
            }

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ShapeComponentCurve)
            {
                ShapeComponentCurve(curve.ShapeComponentCurve, sr);
            }
        }

        /// <summary>
        /// � ��ü �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="scc">� ��ü �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentCurve(ShapeComponentCurve scc, CompoundStreamReader sr)
        {
            int positionCount = sr.ReadSInt4();
            for (int index = 0; index < positionCount; index++)
            {
                var p = scc.AddNewPosition();
                p.X = (uint)sr.ReadSInt4();
                p.Y = (uint)sr.ReadSInt4();
            }
            for (int index = 0; index < positionCount - 1; index++)
            {
                var cst = CurveSegmentTypeExtensions.FromValue(sr.ReadUInt1());
                scc.AddCurveSegmentType(cst);
            }
            sr.Skip(4);
        }
    }

}