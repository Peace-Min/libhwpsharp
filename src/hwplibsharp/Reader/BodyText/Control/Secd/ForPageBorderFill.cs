// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/secd/ForPageBorderFill.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.SectionDefine;


namespace HwpLib.Reader.BodyText.Control.Secd
{

    /// <summary>
    /// �� �׵θ�/��� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForPageBorderFill
    {
        /// <summary>
        /// �� �׵θ�/��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="pbf">�� �׵θ�/��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(PageBorderFill pbf, CompoundStreamReader sr)
        {
            pbf.Property.Value = sr.ReadUInt4();
            pbf.LeftGap = sr.ReadUInt2();
            pbf.RightGap = sr.ReadUInt2();
            pbf.TopGap = sr.ReadUInt2();
            pbf.BottomGap = sr.ReadUInt2();
            pbf.BorderFillId = sr.ReadUInt2();
        }
    }

}