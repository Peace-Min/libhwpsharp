using HwpLib.CompoundFile;


namespace HwpLib.Reader.BodyText.Paragraph
{

    /// <summary>
    /// ���� ���� ��� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForParaCharShape
    {
        /// <summary>
        /// ���� ���� ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="pcs">���� ���� ���</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(HwpLib.Object.BodyText.Paragraph.CharShape.ParaCharShape pcs, CompoundStreamReader sr)
        {
            // ���ڵ� ũ�� / 8 = �׸� �� (position 4����Ʈ + charShapeId 4����Ʈ)
            int count = (int)(sr.CurrentRecordHeader!.Size / 8);

            for (int i = 0; i < count; i++)
            {
                uint position = sr.ReadUInt4();
                uint charShapeId = sr.ReadUInt4();
                pcs.AddParaCharShape(position, charShapeId);
            }
        }
    }


}