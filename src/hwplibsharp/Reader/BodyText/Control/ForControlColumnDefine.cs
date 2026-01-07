// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/ForControlColumnDefine.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader;
using HwpLib.Object.BodyText.Control.CtrlHeader.ColumnDefine;
using HwpLib.Object.DocInfo.BorderFill;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// �� ���� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public static class ForControlColumnDefine
    {
        /// <summary>
        /// �� ���� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="cd">�� ���� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(ControlColumnDefine cd, CompoundStreamReader sr)
        {
            CtrlHeader(cd.GetHeader()!, sr);
        }

        /// <summary>
        /// �� ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="h">�� ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void CtrlHeader(CtrlHeaderColumnDefine h, CompoundStreamReader sr)
        {
            h.Property.Value = sr.ReadUInt2();

            int count = h.Property.GetColumnCount();
            bool sameWidth = h.Property.IsSameWidth();

            if (count < 2 || sameWidth)
            {
                h.GapBetweenColumn = sr.ReadUInt2();
                h.Property2 = sr.ReadUInt2();
            }
            else
            {
                h.Property2 = sr.ReadUInt2();
                ColumnInfos(h, sr);
            }

            h.DivideLine.Type = BorderTypeExtensions.FromValue(sr.ReadUInt1());
            h.DivideLine.Thickness = BorderThicknessExtensions.FromValue(sr.ReadUInt1());
            h.DivideLine.Color.Value = sr.ReadUInt4();

            sr.SkipToEndRecord();
        }

        /// <summary>
        /// �� ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ��� �� ���� �κ��� �д´�.
        /// </summary>
        /// <param name="h">�� ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ColumnInfos(CtrlHeaderColumnDefine h, CompoundStreamReader sr)
        {
            int count = h.Property.GetColumnCount();
            for (int index = 0; index < count; index++)
            {
                ColumnInfo ci = h.AddNewColumnInfo();
                ci.Width = sr.ReadUInt2();
                ci.Gap = sr.ReadUInt2();
            }
        }
    }

}