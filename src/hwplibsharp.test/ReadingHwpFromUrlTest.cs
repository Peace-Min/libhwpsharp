using HwpLib.Object;
using HwpLib.Reader;

namespace HwpLibSharp.Test;

/// <summary>
/// URL로부터 파일을 읽는 테스트
/// </summary>
[TestClass]
public class ReadingHwpFromUrlTest
{
    [TestMethod]
    [Ignore("HWPReader.FromUrl은 아직 구현되지 않음 - TODO")]
    public void ReadHwpFromUrl_ShouldSucceed()
    {
        // Arrange
        var url = "http://ocwork.haansoft.com/sample/sample.hwp";
        
        // Act
        // TODO: HWPReader.FromUrl() 메서드가 라이브러리에 구현되면 활성화
        // var hwpFile = HWPReader.FromUrl(url);
        HWPFile? hwpFile = null;
        
        // Assert
        Assert.IsNotNull(hwpFile);
        Assert.IsTrue(hwpFile.BodyText.SectionList.Count > 0, $"{url} 읽기 성공 !!");
        Console.WriteLine($"{url} 읽기 성공 !!");
    }
}
