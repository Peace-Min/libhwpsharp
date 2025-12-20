using HwpLib.CompoundFile;


namespace HwpLib.Reader.BodyText.Paragraph
{

    /// <summary>
    /// ���� ��� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForParaHeader
    {
        /// <summary>
        /// ���� ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="ph">���� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(HwpLib.Object.BodyText.Paragraph.Header.ParaHeader ph, CompoundStreamReader sr)
        {
            // ���� ����Ʈ���� ������ ���ܿ��ο� ���ڼ��� �д´�.
            uint value = sr.ReadUInt4();
            ph.LastInList = (value & 0x80000000) != 0;
            ph.CharacterCount = value & 0x7fffffff;

            ph.ControlMask.Value = sr.ReadUInt4();
            ph.ParaShapeId = sr.ReadUInt2();
            ph.StyleId = sr.ReadUInt1();
            ph.DivideSort.Value = sr.ReadUInt1();
            ph.CharShapeCount = sr.ReadUInt2();
            ph.RangeTagCount = sr.ReadUInt2();
            ph.LineAlignCount = sr.ReadUInt2();
            ph.InstanceID = sr.ReadUInt4();

            if (!sr.IsEndOfRecord() && sr.FileVersion.IsOver(5, 0, 3, 2))
            {
                ph.IsMergedByTrack = sr.ReadUInt2();
            }

            // ���� ����Ʈ�� ������ �ǳʶڴ�
            sr.SkipToEndRecord();
        }
    }


}