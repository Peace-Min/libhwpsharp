// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/secd/ForCtrlHeaderSecd.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.CtrlHeader;


namespace HwpLib.Reader.BodyText.Control.Secd
{

    /// <summary>
    /// ���� ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForCtrlHeaderSecd
    {
        /// <summary>
        /// ���� ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="header">���� ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(CtrlHeaderSectionDefine header, CompoundStreamReader sr)
        {
            header.Property.Value = sr.ReadUInt4();
            header.ColumnGap = sr.ReadUInt2();
            header.VerticalLineAlign = sr.ReadUInt2();
            header.HorizontalLineAlign = sr.ReadUInt2();
            header.DefaultTabGap = sr.ReadUInt4();
            header.NumberParaShapeId = sr.CorrectParaShapeId(sr.ReadUInt2());
            header.PageStartNumber = sr.ReadUInt2();
            header.ImageStartNumber = sr.ReadUInt2();
            header.TableStartNumber = sr.ReadUInt2();
            header.EquationStartNumber = sr.ReadUInt2();

            if (!sr.IsEndOfRecord() && sr.FileVersion.IsOver(5, 0, 1, 2))
            {
                header.DefaultLanguage = sr.ReadUInt2();
            }

            if (sr.IsEndOfRecord()) return;

            sr.SkipToEndRecord();
        }
    }

}