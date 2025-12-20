using HwpLib.CompoundFile;


namespace HwpLib.Reader.BodyText.Paragraph
{

    /// <summary>
    /// ���� ���� �±� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForParaRangeTag
    {
        /// <summary>
        /// ���� ���� �±� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="prt">���� ���� �±�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(HwpLib.Object.BodyText.Paragraph.RangeTag.ParaRangeTag prt, CompoundStreamReader sr)
        {
            // ���ڵ� ũ�� / 8 = �׸� �� (RangeStart 4����Ʈ + RangeEnd 4����Ʈ + Data 3����Ʈ + Sort 1����Ʈ = 12����Ʈ�� �ƴ϶� 8����Ʈ��)
            // Writer�� ����: RangeStart 4����Ʈ, RangeEnd 4����Ʈ, Data 3����Ʈ, Sort 1����Ʈ = �� 12����Ʈ
            int count = (int)(sr.CurrentRecordHeader!.Size / 12);

            for (int i = 0; i < count; i++)
            {
                var item = prt.AddNewRangeTagItem();

                item.RangeStart = sr.ReadUInt4();
                item.RangeEnd = sr.ReadUInt4();
                byte[] data = sr.ReadBytes(3);
                item.SetData(data);
                item.Sort = sr.ReadSInt1();
            }
        }
    }


}