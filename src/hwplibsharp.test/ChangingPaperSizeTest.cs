using HwpLib.Object.BodyText.Control;
using HwpLib.Reader;
using HwpLib.Writer;

namespace HwpLibSharp.Test;

/// <summary>
/// 용지 크기 변경 테스트
/// </summary>
[TestClass]
public class ChangingPaperSizeTest
{
    // 표준 용지 크기 (HWP 좌표)
    private const long A4Width = 59528;
    private const long A4Height = 84188;
    private const long A3Width = 84188;
    private const long A3Height = 119055;
    private const long B4Width = 72850;
    private const long B4Height = 103039;
    private const long B5Width = 51591;
    private const long B5Height = 72850;
    private const long LegalWidth = 61200;
    private const long LegalHeight = 100800;
    private const long A5Width = 41818;
    private const long A5Height = 59528;

    [TestMethod]
    public void ChangePaperSize_ToA3_ShouldSucceed()
    {
        // Arrange
        var filePath = TestHelper.GetBasicSamplePath("blank.hwp");
        var hwpFile = HWPReader.FromFile(filePath);

        Assert.IsNotNull(hwpFile);

        // Act
        var section = hwpFile.BodyText.SectionList[0];
        var firstParagraph = section.GetParagraph(0);

        // 섹션 정의 컨트롤 찾기
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
        var writePath = TestHelper.GetResultPath("result-changing-papersize-a3-single.hwp");
        HWPWriter.ToFile(hwpFile, writePath);
        Assert.IsTrue(File.Exists(writePath));
    }

    [TestMethod]
    [DataRow(A4Width, A4Height, "a4")]
    [DataRow(A3Width, A3Height, "a3")]
    [DataRow(B4Width, B4Height, "b4")]
    [DataRow(B5Width, B5Height, "b5")]
    [DataRow(LegalWidth, LegalHeight, "legal")]
    [DataRow(A5Width, A5Height, "a5")]
    public void ChangePaperSize_ToDifferentSizes_ShouldSucceed(long width, long height, string sizeName)
    {
        // Arrange
        var filePath = TestHelper.GetBasicSamplePath("blank.hwp");
        var hwpFile = HWPReader.FromFile(filePath);

        Assert.IsNotNull(hwpFile);

        // Act
        var section = hwpFile.BodyText.SectionList[0];
        var firstParagraph = section.GetParagraph(0);

        if (firstParagraph.ControlList != null)
        {
            foreach (var control in firstParagraph.ControlList)
            {
                if (control is ControlSectionDefine sectionDefine)
                {
                    sectionDefine.PageDef.PaperWidth = width;
                    sectionDefine.PageDef.PaperHeight = height;
                }
            }
        }

        // Assert
        var writePath = TestHelper.GetResultPath($"result-changing-papersize-{sizeName}.hwp");
        HWPWriter.ToFile(hwpFile, writePath);
        Assert.IsTrue(File.Exists(writePath));
    }

    /// <summary>
    /// mm를 HWP 좌표로 변환
    /// </summary>
    private long MmToHwp(double mm)
    {
        return (long)(mm * 72000.0 / 254.0 + 0.5);
    }
}
