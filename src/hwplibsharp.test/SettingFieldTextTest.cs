using HwpLib.Object.BodyText.Control;
using HwpLib.Reader;
using HwpLib.Tool.ObjectFinder;
using HwpLib.Writer;

namespace HwpLibSharp.Test;

/// <summary>
/// 필드 텍스트 설정 테스트
/// </summary>
[TestClass]
public class SettingFieldTextTest
{
    [TestMethod]
    public void SetFieldText_ShouldSucceed()
    {
        // Arrange
        var filePath = TestHelper.GetSamplePath("setting-fields.hwp");
        var hwpFile = HWPReader.FromFile(filePath);

        Assert.IsNotNull(hwpFile);

        // Act & Assert - 필드1 설정
        {
            var textList = new List<string>
            {
                "필드1 값1\n2줄\n3줄\n",
                "필드1 값2\n2줄\n3줄",
                "필드1 값3",
                "필드1 값4"
            };
            var sfr = FieldFinder.SetFieldText(hwpFile, ControlType.FIELD_CLICKHERE, "필드1", textList);
            Console.WriteLine($"필드1 설정결과 = {sfr}");
        }

        // Act & Assert - 필드2 설정
        {
            var textList = new List<string>
            {
                "필드2 값1",
                "필드2 값2",
                "필드2 값3"
            };
            var sfr = FieldFinder.SetFieldText(hwpFile, ControlType.FIELD_CLICKHERE, "필드2", textList);
            Console.WriteLine($"필드2 설정결과 = {sfr}");
        }

        // 저장
        var writePath = TestHelper.GetResultPath("result-setting-field.hwp");
        HWPWriter.ToFile(hwpFile, writePath);
        Assert.IsTrue(File.Exists(writePath));
    }
}
