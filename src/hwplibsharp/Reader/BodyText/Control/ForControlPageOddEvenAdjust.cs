// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/ForControlPageOddEvenAdjust.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// Ȧ/¦�� ���� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public static class ForControlPageOddEvenAdjust
    {
        /// <summary>
        /// Ȧ/¦�� ���� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="pgoea">Ȧ/¦�� ���� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(ControlPageOddEvenAdjust pgoea, CompoundStreamReader sr)
        {
            pgoea.GetHeader()!.Property.Value = sr.ReadUInt4();
        }
    }

}