using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Bookmark;
using HwpLib.Object.BodyText.Control.Gso.TextBox;


namespace HwpLib.Reader.BodyText.Control.Gso.Part
{

    /// <summary>
    /// �ۻ��ڸ� �б� ���� ��ü
    /// </summary>
    public static class ForTextBox
    {
        /// <summary>
        /// �ۻ��ڸ� �д´�.
        /// </summary>
        /// <param name="textBox">�ۻ���</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(TextBox textBox, CompoundStreamReader sr)
        {
            ListHeader(textBox.ListHeader, sr);
            ForParagraphList.Read(textBox.ParagraphList, sr);
        }

        /// <summary>
        /// �ۻ����� ���� ����Ʈ ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="lh">�ۻ����� ���� ����Ʈ ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ListHeader(ListHeaderForTextBox lh, CompoundStreamReader sr)
        {
            lh.ParaCount = sr.ReadSInt4();
            lh.Property.Value = sr.ReadUInt4();
            lh.LeftMargin = sr.ReadUInt2();
            lh.RightMargin = sr.ReadUInt2();
            lh.TopMargin = sr.ReadUInt2();
            lh.BottomMargin = sr.ReadUInt2();
            lh.TextWidth = sr.ReadUInt4();

            if (sr.IsEndOfRecord()) return;

            sr.Skip(8); // unknown bytes

            if (sr.IsEndOfRecord()) return;

            int temp = sr.ReadSInt4();
            lh.EditableAtFormMode = (temp == 1);

            short flag = sr.ReadUInt1();
            if (flag == 0xff)
            {
                FieldName(lh, sr);
            }
        }

        /// <summary>
        /// �ʵ� �̸��� �д´�.
        /// </summary>
        /// <param name="lh">�ۻ����� ���� ����Ʈ ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void FieldName(ListHeaderForTextBox lh, CompoundStreamReader sr)
        {
            var ps = new ParameterSet();
            ForParameterSet.Read(ps, sr);

            if (ps.Id != 0x21b) return;

            foreach (var pi in ps.ParameterItemList)
            {
                if (pi.Id == 0x4000 && pi.Type == ParameterType.String)
                {
                    lh.FieldName = pi.Value_BSTR;
                }
            }
        }
    }

}