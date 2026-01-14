using HwpLib.Reader;
using HwpLib.Tool.ObjectFinder;
using HwpLib.Writer;

namespace HwpLibSharp.Test;

/// <summary>
/// 누름틀 필드 컨트롤의 텍스트를 설정하는 테스트
/// </summary>
[TestClass]
public class SettingClickHereFieldTextTest
{
    [TestMethod]
    public void SetClickHereFieldText_ShouldSucceed()
    {
        // Arrange
        var filePath = TestHelper.GetBasicSamplePath("필드-누름틀.hwp");
        var hwpFile = HWPReader.FromFile(filePath);

        Assert.IsNotNull(hwpFile);

        // Act
        {
            var textList = new List<string> { "필드1 값1" };
            var sfr = FieldFinder.SetClickHereText(hwpFile, "필드1", textList);
            Console.WriteLine($"필드1 설정결과 = {sfr}");
        }
        {
            var textList = new List<string> { "필드2 값1" };
            var sfr = FieldFinder.SetClickHereText(hwpFile, "필드2", textList);
            Console.WriteLine($"필드2 설정결과 = {sfr}");
        }

        var writePath = TestHelper.GetBasicSamplePath("result-setting-필드-누름틀.hwp");
        HWPWriter.ToFile(hwpFile, writePath);

        // Assert
        Assert.IsTrue(File.Exists(writePath), "누름틀 필드 텍스트 설정 성공");
    }
}
