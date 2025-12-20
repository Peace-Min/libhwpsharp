using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach.ObjectLinkLine;
using HwpLib.Object.Etc;


namespace HwpLib.Reader.BodyText.Control.Gso
{

    /// <summary>
    /// ��ü ���ἱ ��Ʈ���� ������ �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForControlObjectLinkLine
    {
        /// <summary>
        /// ��ü ���ἱ ��Ʈ���� ������ �κ��� �д´�.
        /// </summary>
        /// <param name="objectLinkLine">��ü ���ἱ ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadRest(ControlObjectLinkLine objectLinkLine, CompoundStreamReader sr)
        {
            sr.ReadRecordHeader();

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ShapeComponentLine)
            {
                ShapeComponentLine(objectLinkLine.ShapeComponentLine, sr);
            }
        }

        /// <summary>
        /// �� ��ü �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="scl">�� ��ü �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentLine(ShapeComponentLineForObjectLinkLine scl, CompoundStreamReader sr)
        {
            scl.StartX = sr.ReadSInt4();
            scl.StartY = sr.ReadSInt4();
            scl.EndX = sr.ReadSInt4();
            scl.EndY = sr.ReadSInt4();

            scl.Type = LinkLineTypeExtensions.FromValue((byte)sr.ReadUInt4());
            scl.StartSubjectID = sr.ReadUInt4();
            scl.StartSubjectIndex = sr.ReadUInt4();
            scl.EndSubjectID = sr.ReadUInt4();
            scl.EndSubjectIndex = sr.ReadUInt4();

            int countOfCP = (int)sr.ReadUInt4();
            for (int index = 0; index < countOfCP; index++)
            {
                var cp = scl.AddNewControlPoint();
                cp.X = sr.ReadUInt4();
                cp.Y = sr.ReadUInt4();
                cp.Type = sr.ReadUInt2();
            }

            if (sr.IsEndOfRecord()) return;

            sr.SkipToEndRecord();
        }
    }

}