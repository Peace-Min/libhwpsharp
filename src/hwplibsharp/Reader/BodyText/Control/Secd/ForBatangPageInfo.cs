// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/secd/ForBatangPageInfo.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.SectionDefine;


namespace HwpLib.Reader.BodyText.Control.Secd
{

    /// <summary>
    /// ������ ������ �б� ���� ��ü
    /// </summary>
    public static class ForBatangPageInfo
    {
        /// <summary>
        /// ������ ������ �д´�.
        /// </summary>
        /// <param name="bpi">������ ����</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(BatangPageInfo bpi, CompoundStreamReader sr)
        {
            ListHeader(bpi.ListHeader, sr);
            ForParagraphList.Read(bpi.ParagraphList, sr);
        }

        /// <summary>
        /// �������� ���� ����Ʈ ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="lh">�������� ���� ����Ʈ ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ListHeader(ListHeaderForBatangPage lh, CompoundStreamReader sr)
        {
            lh.ParaCount = sr.ReadSInt4();
            lh.Property.Value = sr.ReadUInt4();
            lh.TextWidth = sr.ReadUInt4();
            lh.TextHeight = sr.ReadUInt4();
            sr.SkipToEndRecord();
        }
    }

}