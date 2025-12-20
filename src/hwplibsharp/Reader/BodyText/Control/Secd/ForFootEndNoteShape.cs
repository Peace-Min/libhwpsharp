using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.SectionDefine;
using HwpLib.Object.DocInfo.BorderFill;


namespace HwpLib.Reader.BodyText.Control.Secd
{

    /// <summary>
    /// ����/���� ��� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForFootEndNoteShape
    {
        /// <summary>
        /// ����/���� ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="fens">����/���� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(FootEndNoteShape fens, CompoundStreamReader sr)
        {
            fens.Property.Value = sr.ReadUInt4();
            fens.UserSymbol.Bytes = sr.ReadWChar();
            fens.BeforeDecorativeLetter.Bytes = sr.ReadWChar();
            fens.AfterDecorativeLetter.Bytes = sr.ReadWChar();
            fens.StartNumber = sr.ReadUInt2();
            fens.DivideLineLength = sr.ReadUInt4();
            fens.DivideLineTopMargin = sr.ReadUInt2();
            fens.DivideLineBottomMargin = sr.ReadUInt2();
            fens.BetweenNotesMargin = sr.ReadUInt2();
            fens.DivideLine.Type = BorderTypeExtensions.FromValue(sr.ReadUInt1());
            fens.DivideLine.Thickness = BorderThicknessExtensions.FromValue(sr.ReadUInt1());
            fens.DivideLine.Color.Value = sr.ReadUInt4();

            if (sr.IsEndOfRecord()) return;

            fens.Unknown = sr.ReadUInt4();
        }
    }

}