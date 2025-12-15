using HwpLib.Object;
using HwpLib.Reader;

namespace HwpLibSharp.Test;

/// <summary>
/// 배포용 HWP 파일 읽기 테스트
/// </summary>
[TestClass]
public class ReadingDistributionHwpFileTest
{
    [TestMethod]
    public void ReadDistributionFile_ShouldSucceed()
    {
        // Arrange
        var filePath = TestHelper.GetSamplePath("distribution.hwp");
        
        // Act
        var hwpFile = HWPReader.FromFile(filePath);
        
        // Assert
        Assert.IsNotNull(hwpFile);
        Assert.IsTrue(hwpFile.BodyText.SectionList.Count > 0, "배포용 파일 읽기 성공");
    }
}
