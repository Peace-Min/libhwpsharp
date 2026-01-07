// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/gso/part/ForCaption.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso.Caption;


namespace HwpLib.Reader.BodyText.Control.Gso.Part
{

    /// <summary>
    /// ĸ�� ������ �б� ���� ��ü
    /// </summary>
    public static class ForCaption
    {
        /// <summary>
        /// ĸ�� ������ �д´�.
        /// </summary>
        /// <param name="caption">ĸ�� ����</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(Caption caption, CompoundStreamReader sr)
        {
            ListHeader(caption.ListHeader, sr);
            ForParagraphList.Read(caption.ParagraphList, sr);
        }

        /// <summary>
        /// ĸ�� ������ ���� ����Ʈ ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="listHeader">ĸ�� ������ ���� ����Ʈ ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ListHeader(ListHeaderForCaption listHeader, CompoundStreamReader sr)
        {
            listHeader.ParaCount = sr.ReadSInt4();
            listHeader.Property.Value = sr.ReadUInt4();
            listHeader.CaptionProperty.Value = sr.ReadUInt4();
            listHeader.CaptionWidth = sr.ReadUInt4();
            listHeader.SpaceBetweenCaptionAndFrame = sr.ReadUInt2();
            listHeader.TextWidth = sr.ReadUInt4();
            // ������ ���� 8bytes�� ���� �� ����.
            sr.SkipToEndRecord();
        }
    }

}