// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/tbl/ForTable.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Table;


namespace HwpLib.Reader.BodyText.Control.Tbl
{

    /// <summary>
    /// ǥ ���� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForTable
    {
        /// <summary>
        /// ǥ ���� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="table">ǥ ���� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(Table table, CompoundStreamReader sr)
        {
            table.Property.Value = sr.ReadUInt4();

            table.RowCount = sr.ReadUInt2();
            table.ColumnCount = sr.ReadUInt2();
            table.CellSpacing = sr.ReadUInt2();
            table.LeftInnerMargin = sr.ReadUInt2();
            table.RightInnerMargin = sr.ReadUInt2();
            table.TopInnerMargin = sr.ReadUInt2();
            table.BottomInnerMargin = sr.ReadUInt2();

            for (int index = 0; index < table.RowCount; index++)
            {
                table.AddCellCountOfRow(sr.ReadUInt2());
            }

            table.BorderFillId = sr.ReadUInt2();

            if (sr.FileVersion.IsOver(5, 0, 1, 0))
            {
                ZoneInfo(table, sr);
            }
        }

        /// <summary>
        /// zone info�� �д´�.
        /// </summary>
        /// <param name="table">ǥ ���� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ZoneInfo(Table table, CompoundStreamReader sr)
        {
            int count = sr.ReadUInt2();
            for (int index = 0; index < count; index++)
            {
                var zi = table.AddNewZoneInfo();
                zi.StartColumn = sr.ReadUInt2();
                zi.StartRow = sr.ReadUInt2();
                zi.EndColumn = sr.ReadUInt2();
                zi.EndRow = sr.ReadUInt2();
                zi.BorderFillId = sr.ReadUInt2();
            }
        }
    }

}