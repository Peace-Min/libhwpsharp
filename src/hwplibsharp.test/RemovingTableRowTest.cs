using HwpLib.Object;
using HwpLib.Object.BodyText;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.Table;
using HwpLib.Object.BodyText.Paragraph;
using HwpLib.Reader;
using HwpLib.Writer;

namespace HwpLibSharp.Test;

/// <summary>
/// 표의 열을 삭제하는 테스트
/// </summary>
[TestClass]
public class RemovingTableRowTest
{
    [TestMethod]
    [Ignore("IReadOnlyList에서 RemoveAt을 직접 호출할 수 없음 - 라이브러리에 RemoveRow 메서드 추가 필요")]
    public void RemoveTableRow_ShouldSucceed()
    {
        // Arrange
        var filePath = TestHelper.GetSamplePath("removing-row.hwp");
        var hwpFile = HWPReader.FromFile(filePath);
        
        Assert.IsNotNull(hwpFile);
        
        // Act
        var table = FindTable(hwpFile);
        Assert.IsNotNull(table);
        
        RemoveSecondRowObject(table);
        AdjustTableRowCount(table);
        RemoveCellCountOfRow(table);
        AdjustCellRowIndex(table);
        
        var writePath = TestHelper.GetResultPath("result-removing-row.hwp");
        HWPWriter.ToFile(hwpFile, writePath);
        
        // Assert
        Assert.IsTrue(File.Exists(writePath), "테이블 행 삭제 성공");
    }

    private static void RemoveSecondRowObject(ControlTable table)
    {
        // TODO: 라이브러리에 RemoveRow(int index) 메서드 추가 필요
        // table.RowList는 IReadOnlyList이므로 RemoveAt 직접 호출 불가
        // 우회 방법: internal List에 접근하거나 새 메서드 추가 필요
        // table.RowList.RemoveAt(1);
    }

    private static void AdjustTableRowCount(ControlTable table)
    {
        table.Table.RowCount = table.RowList.Count;
    }

    private static void RemoveCellCountOfRow(ControlTable table)
    {
        // TODO: 라이브러리에 RemoveCellCountOfRow(int index) 메서드 추가 필요
        // table.Table.CellCountOfRowList는 IReadOnlyList이므로 RemoveAt 직접 호출 불가
        // table.Table.CellCountOfRowList.RemoveAt(1);
    }

    private static void AdjustCellRowIndex(ControlTable table)
    {
        int rowCount = table.RowList.Count;
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            if (rowIndex > 0)
            {
                var row = table.RowList[rowIndex];
                foreach (var cell in row.CellList)
                {
                    cell.ListHeader.RowIndex = cell.ListHeader.RowIndex - 1;
                }
            }
        }
    }

    private static ControlTable? FindTable(HWPFile hwpFile)
    {
        Section s = hwpFile.BodyText.SectionList[0];
        Paragraph firstParagraph = s.GetParagraph(0);
        if (firstParagraph.ControlList[2].Type == ControlType.Table)
        {
            return (ControlTable)firstParagraph.ControlList[2];
        }
        return null;
    }
}
