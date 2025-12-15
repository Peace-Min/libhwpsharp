using HwpLib.Object;
using HwpLib.Object.BodyText;
using HwpLib.Object.BodyText.Paragraph;
using HwpLib.Reader;
using HwpLib.Tool.ParagraphAdder;
using HwpLib.Writer;

namespace HwpLibSharp.Test;

/// <summary>
/// 다른 파일에 문단을 복사하여 복사된 한글 파일 객체에 추가하는 테스트
/// </summary>
[TestClass]
public class AddingParagraphBetweenClonedHwpFileTest
{
    [TestMethod]
    public void AddParagraphToClonedFile_ShouldSucceed()
    {
        // Arrange
        var targetFilePath = TestHelper.GetSamplePath("target.hwp");
        var sourceFilePath = TestHelper.GetSamplePath("source.hwp");
        
        var targetHwpFile = HWPReader.FromFile(targetFilePath);
        Assert.IsNotNull(targetHwpFile);
        
        var clonedTargetHwpFile = targetHwpFile.Clone(false);
        Assert.IsNotNull(clonedTargetHwpFile);
        
        // Act - 원본 타겟에 source.hwp에서 문단 추가
        AddHwpFile(targetHwpFile, sourceFilePath);
        var writePath = TestHelper.GetResultPath("result-adding-paragraph.hwp");
        HWPWriter.ToFile(targetHwpFile, writePath);
        
        // Act - 복제된 타겟에 merging-cell.hwp에서 문단 추가
        var mergingCellFilePath = TestHelper.GetSamplePath("merging-cell.hwp");
        AddHwpFile(clonedTargetHwpFile, mergingCellFilePath);
        var clonedWritePath = TestHelper.GetResultPath("result-adding-paragraph-to-cloned-hwpfile.hwp");
        HWPWriter.ToFile(clonedTargetHwpFile, clonedWritePath);
        
        // Assert
        Assert.IsTrue(File.Exists(writePath));
        Assert.IsTrue(File.Exists(clonedWritePath));
    }

    private static void AddHwpFile(HWPFile targetHwpFile, string sourceHwpFilePath)
    {
        IParagraphList targetFirstSection = targetHwpFile.BodyText.SectionList[0];
        
        var sourceHwpFile = HWPReader.FromFile(sourceHwpFilePath);
        Assert.IsNotNull(sourceHwpFile);
        
        // 첫 번째 문단 추가
        {
            Paragraph sourceParagraph = sourceHwpFile.BodyText.SectionList[0].GetParagraph(0);
            
            var paraAdder = new ParagraphAdder(targetHwpFile, targetFirstSection);
            paraAdder.Add(sourceHwpFile, sourceParagraph);
        }
        
        // 두 번째 문단이 있으면 추가
        {
            if (sourceHwpFile.BodyText.SectionList[0].ParagraphCount > 2)
            {
                Paragraph sourceParagraph = sourceHwpFile.BodyText.SectionList[0].GetParagraph(1);
                
                var paraAdder = new ParagraphAdder(targetHwpFile, targetFirstSection);
                paraAdder.Add(sourceHwpFile, sourceParagraph);
            }
        }
    }
}
