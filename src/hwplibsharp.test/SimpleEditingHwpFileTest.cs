using HwpLib.Reader;
using HwpLib.Writer;

namespace HwpLibSharp.Test;

/// <summary>
/// 기본 HWP 파일 편집 테스트
/// </summary>
[TestClass]
public class SimpleEditingHwpFileTest
{
    [TestMethod]
    public void SimpleEdit_ShouldSucceed()
    {
        // Arrange
        var filePath = TestHelper.GetBasicSamplePath("blank.hwp");
        var hwpFile = HWPReader.FromFile(filePath);

        Assert.IsNotNull(hwpFile);

        // Act - 단순히 읽고 쓰기
        var writePath = TestHelper.GetResultPath("result-simple-edit.hwp");
        HWPWriter.ToFile(hwpFile, writePath);

        // Assert
        Assert.IsTrue(File.Exists(writePath));

        // 다시 읽어서 검증
        var reloadedFile = HWPReader.FromFile(writePath);
        Assert.IsNotNull(reloadedFile);
    }
}
