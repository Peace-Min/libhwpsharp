using HwpLib.CompoundFile;


namespace HwpLib.Reader.BodyText.Paragraph
{

    /// <summary>
    /// ������ ���̾ƿ� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForParaLineSeg
    {
        /// <summary>
        /// ������ ���̾ƿ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="p">���� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(HwpLib.Object.BodyText.Paragraph.Paragraph p, CompoundStreamReader sr)
        {
            p.CreateLineSeg();
            int count = p.Header.LineAlignCount;
            if (count != 0)
            {
                var pls = p.LineSeg!;
                for (int i = 0; i < count; i++)
                {
                    var item = pls.AddNewLineSegItem();
                    ParaLineSegItem(item, sr);
                }
            }
            else
            {
                sr.SkipToEndRecord();
            }
        }

        /// <summary>
        /// �� ������ ���̾ƿ� ������ �д´�.
        /// </summary>
        /// <param name="item">�� ������ ���̾ƿ� ����</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ParaLineSegItem(HwpLib.Object.BodyText.Paragraph.LineSeg.LineSegItem item, CompoundStreamReader sr)
        {
            item.TextStartPosition = sr.ReadUInt4();
            item.LineVerticalPosition = sr.ReadSInt4();
            item.LineHeight = sr.ReadSInt4();
            item.TextPartHeight = sr.ReadSInt4();
            item.DistanceBaseLineToLineVerticalPosition = sr.ReadSInt4();
            item.LineSpace = sr.ReadSInt4();
            item.StartPositionFromColumn = sr.ReadSInt4();
            item.SegmentWidth = sr.ReadSInt4();
            item.Tag.Value = sr.ReadUInt4();
        }
    }


}