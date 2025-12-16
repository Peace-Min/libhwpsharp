using HwpLib.Object;
using HwpLib.Tool.BlankFileMaker;
using HwpLib.Writer;

namespace HwpLibSharp.Test;

/// <summary>
/// 빈 파일 생성 테스트
/// </summary>
[TestClass]
public class MakingBlankFileTest
{
    [TestMethod]
    public void MakeBlankFile_ShouldSucceed()
    {
        // Act
        var hwpFile = BlankFileMaker.Make();
        
        // Assert
        Assert.IsNotNull(hwpFile);
        Assert.IsNotNull(hwpFile.BodyText);
        Assert.IsNotEmpty(hwpFile.BodyText.SectionList);
    }

    [TestMethod]
    public void MakeBlankFile_AndSave_ShouldSucceed()
    {
        // Arrange
        var hwpFile = BlankFileMaker.Make();
        var writePath = TestHelper.GetResultPath("result-making-blankfile.hwp");
        
        // Act
        Assert.IsNotNull(hwpFile);
        HWPWriter.ToFile(hwpFile, writePath);
        
        // Assert
        Assert.IsTrue(File.Exists(writePath));
    }
}
