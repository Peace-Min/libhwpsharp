// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/ForControlOverlappingLetter.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader;
using HwpLib.Object.Etc;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// ���� ��ħ ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public static class ForControlOverlappingLetter
    {
        /// <summary>
        /// ���� ��ħ ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="tcps">���� ��ħ ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(ControlOverlappingLetter tcps, CompoundStreamReader sr)
        {
            CtrlHeader(tcps.GetHeader()!, sr);
        }

        /// <summary>
        /// ���� ��ħ ��Ʈ���� ��Ʈ�� ��� ���ڵ��� �д´�.
        /// </summary>
        /// <param name="header">���� ��ħ ��Ʈ���� ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void CtrlHeader(CtrlHeaderOverlappingLetter header, CompoundStreamReader sr)
        {
            OverlappingLetters(header, sr);

            if (sr.IsEndOfRecord()) return;

            header.BorderType = sr.ReadUInt1();
            header.InternalFontSize = (byte)sr.ReadSInt1();
            header.ExpendInsideLetter = sr.ReadUInt1();

            CharShapeIds(header, sr);
        }

        /// <summary>
        /// ��ħ ���� �κ��� �д´�.
        /// </summary>
        /// <param name="header">���� ��ħ ��Ʈ���� ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void OverlappingLetters(CtrlHeaderOverlappingLetter header, CompoundStreamReader sr)
        {
            int count = sr.ReadUInt2();
            for (int index = 0; index < count; index++)
            {
                var letter = new HWPString();
                letter.Bytes = sr.ReadWChar();
                header.AddOverlappingLetter(letter);
            }
        }

        /// <summary>
        /// ���� ��� �κ��� �д´�.
        /// </summary>
        /// <param name="header">���� ��ħ ��Ʈ���� ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void CharShapeIds(CtrlHeaderOverlappingLetter header, CompoundStreamReader sr)
        {
            short count = sr.ReadUInt1();
            for (short i = 0; i < count; i++)
            {
                uint charShapeId = sr.ReadUInt4();
                header.AddCharShapeId(charShapeId);
            }
        }
    }

}