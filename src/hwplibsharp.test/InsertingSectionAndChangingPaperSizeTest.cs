using HwpLib.Object.BodyText.Control;
using HwpLib.Tool.BlankFileMaker;
using HwpLib.Writer;

namespace HwpLibSharp.Test;

/// <summary>
/// 섹션 추가 및 용지 크기 변경 테스트
/// </summary>
[TestClass]
public class InsertingSectionAndChangingPaperSizeTest
{
    // 표준 용지 크기 (HWP 좌표)
    private const long A4Width = 59528;
    private const long A4Height = 84188;
    private const long A3Width = 84188;
    private const long A3Height = 119055;

    [TestMethod]
    public void InsertSectionAndChangePaperSize_ShouldSucceed()
    {
        // Arrange
        var hwpFile = BlankFileMaker.Make();
        Assert.IsNotNull(hwpFile);

        // Act - 새 섹션 추가
        var newSection = hwpFile.BodyText.AddNewSection();

        // 새 섹션에 빈 문단 추가
        var newParagraph = newSection.AddNewParagraph();

        // 새 섹션의 용지 크기를 A3로 변경
        var firstParagraph = newSection.GetParagraph(0);
        if (firstParagraph.ControlList != null)
        {
            foreach (var control in firstParagraph.ControlList)
            {
                if (control is ControlSectionDefine sectionDefine)
                {
                    sectionDefine.PageDef.PaperWidth = A3Width;
                    sectionDefine.PageDef.PaperHeight = A3Height;
                }
            }
        }

        // Assert
        Assert.HasCount(2, hwpFile.BodyText.SectionList, "섹션이 2개여야 합니다.");

        var writePath = TestHelper.GetResultPath("result-inserting-section.hwp");
        HWPWriter.ToFile(hwpFile, writePath);
        Assert.IsTrue(File.Exists(writePath));
    }
}
