using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader;
using HwpLib.Object.BodyText.Control.Table;
using HwpLib.Tool.ParagraphAdder.DocInfo;

namespace HwpLib.Tool.ParagraphAdder.Control
{
    /// <summary>
    /// 표 컨트롤을 복사하는 클래스
    /// </summary>
    public class TableCopier
    {
        public static void Copy(ControlTable source, ControlTable target, DocInfoAdder? docInfoAdder)
        {
            var sourceH = source.Header;
            var targetH = target.Header;
            targetH?.Copy(sourceH);

            CtrlDataCopier.Copy(source, target, docInfoAdder);
            CopyCaption(source, target, docInfoAdder);
            CopyTable(source.Table, target.Table, docInfoAdder);
            CopyRows(source, target, docInfoAdder);
        }

        private static void CopyCaption(ControlTable source, ControlTable target, DocInfoAdder? docInfoAdder)
        {
            if (source.Caption != null)
            {
                target.CreateCaption();
                CaptionCopier.Copy(source.Caption!, target.Caption!, docInfoAdder);
            }
            else
            {
                target.DeleteCaption();
            }
        }

        private static void CopyTable(Table? source, Table? target, DocInfoAdder? docInfoAdder)
        {
            if (source == null || target == null) return;

            if (target.Property != null && source.Property != null)
            {
                target.Property.Value = source.Property.Value;
            }
            target.RowCount = source.RowCount;
            target.ColumnCount = source.ColumnCount;
            target.CellSpacing = source.CellSpacing;
            target.LeftInnerMargin = source.LeftInnerMargin;
            target.RightInnerMargin = source.RightInnerMargin;
            target.TopInnerMargin = source.TopInnerMargin;
            target.BottomInnerMargin = source.BottomInnerMargin;

            foreach (var cellCountOfRow in source.CellCountOfRowList)
            {
                target.AddCellCountOfRow(cellCountOfRow);
            }

            target.BorderFillId = docInfoAdder == null ? source.BorderFillId : docInfoAdder.ForBorderFillInfo().ProcessById(source.BorderFillId);

            foreach (var zoneInfo in source.ZoneInfoList)
            {
                CopyZoneInfo(zoneInfo, target.AddNewZoneInfo(), docInfoAdder);
            }
        }

        private static void CopyZoneInfo(ZoneInfo source, ZoneInfo target, DocInfoAdder? docInfoAdder)
        {
            target.StartColumn = source.StartColumn;
            target.StartRow = source.StartRow;
            target.EndColumn = source.EndColumn;
            target.EndRow = source.EndRow;
            target.BorderFillId = docInfoAdder == null ? source.BorderFillId : docInfoAdder.ForBorderFillInfo().ProcessById(source.BorderFillId);
        }

        private static void CopyRows(ControlTable source, ControlTable target, DocInfoAdder? docInfoAdder)
        {
            foreach (var row in source.RowList)
            {
                CopyRow(row, target.AddNewRow(), docInfoAdder);
            }
        }

        private static void CopyRow(Row source, Row target, DocInfoAdder? docInfoAdder)
        {
            foreach (var cell in source.CellList)
            {
                CopyCell(cell, target.AddNewCell(), docInfoAdder);
            }
        }

        private static void CopyCell(Cell source, Cell target, DocInfoAdder? docInfoAdder)
        {
            CopyListHeader(source.ListHeader, target.ListHeader, docInfoAdder);
            ParagraphCopier.ListCopy(source.ParagraphList, target.ParagraphList, docInfoAdder);
        }

        private static void CopyListHeader(ListHeaderForCell? source, ListHeaderForCell? target, DocInfoAdder? docInfoAdder)
        {
            if (source == null || target == null) return;

            target.ParaCount = source.ParaCount;
            if (target.Property != null && source.Property != null)
            {
                target.Property.Value = source.Property.Value;
            }
            target.ColIndex = source.ColIndex;
            target.RowIndex = source.RowIndex;
            target.ColSpan = source.ColSpan;
            target.RowSpan = source.RowSpan;
            target.Width = source.Width;
            target.Height = source.Height;
            target.LeftMargin = source.LeftMargin;
            target.RightMargin = source.RightMargin;
            target.TopMargin = source.TopMargin;
            target.BottomMargin = source.BottomMargin;
            target.BorderFillId = docInfoAdder == null ? source.BorderFillId : docInfoAdder.ForBorderFillInfo().ProcessById(source.BorderFillId);
            target.TextWidth = source.TextWidth;
            target.FieldName = source.FieldName;
        }
    }
}
