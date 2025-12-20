using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.SectionDefine;


namespace HwpLib.Reader.BodyText.Control.Secd
{

    /// <summary>
    /// ���� ���� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForPageDef
    {
        /// <summary>
        /// ���� ���� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="pd">���� ���� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(PageDef pd, CompoundStreamReader sr)
        {
            pd.PaperWidth = sr.ReadUInt4();
            pd.PaperHeight = sr.ReadUInt4();
            pd.LeftMargin = sr.ReadUInt4();
            pd.RightMargin = sr.ReadUInt4();
            pd.TopMargin = sr.ReadUInt4();
            pd.BottomMargin = sr.ReadUInt4();
            pd.HeaderMargin = sr.ReadUInt4();
            pd.FooterMargin = sr.ReadUInt4();
            pd.GutterMargin = sr.ReadUInt4();
            pd.Property.Value = sr.ReadUInt4();
        }
    }

}