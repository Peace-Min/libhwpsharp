using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Equation;


namespace HwpLib.Reader.BodyText.Control.Eqed
{

    /// <summary>
    /// ���� ���� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForEQEdit
    {
        /// <summary>
        /// ���� ���� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="eqEdit">���� ���� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(EQEdit eqEdit, CompoundStreamReader sr)
        {
            eqEdit.Property = sr.ReadUInt4();
            eqEdit.Script.Bytes = sr.ReadHWPString();
            eqEdit.LetterSize = sr.ReadUInt4();
            eqEdit.LetterColor.Value = sr.ReadUInt4();

            if (sr.IsEndOfRecord()) return;

            eqEdit.BaseLine = sr.ReadSInt2();

            if (sr.IsEndOfRecord()) return;

            eqEdit.Unknown = sr.ReadUInt2();

            if (sr.IsEndOfRecord()) return;

            eqEdit.VersionInfo.Bytes = sr.ReadHWPString();

            if (sr.IsEndOfRecord()) return;

            eqEdit.FontName.Bytes = sr.ReadHWPString();
        }
    }

}