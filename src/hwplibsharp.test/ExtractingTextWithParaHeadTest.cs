using HwpLib.Object;
using HwpLib.Reader;
using HwpLib.Tool.TextExtractor;

namespace HwpLibSharp.Test;

/// <summary>
/// 문단 헤드와 함께 텍스트 추출 테스트
/// </summary>
[TestClass]
public class ExtractingTextWithParaHeadTest
{
    [TestMethod]
    public void ExtractTextWithParaHead_ShouldSucceed()
    {
        // Arrange
        var filePath = TestHelper.GetSamplePath("extracting_ParaHead.hwp");
        var hwpFile = HWPReader.FromFile(filePath);
        
        Assert.IsNotNull(hwpFile);
        
        // Act
        var text = TextExtractor.Extract(hwpFile, TextExtractMethod.AppendControlTextAfterParagraphText);
        
        // Assert
        Assert.IsNotNull(text);
        Console.WriteLine(text);
        Assert.IsTrue(text.Length > 0, "문단 헤드와 함께 텍스트 추출 성공");
    }
}
