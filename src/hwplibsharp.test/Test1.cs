using HwpLib;

namespace HwpLibSharp.Test;

/// <summary>
/// 기본 라이브러리 정보 테스트
/// </summary>
[TestClass]
public sealed class AboutTest
{
    [TestMethod]
    public void About_Version_ShouldNotBeEmpty()
    {
        // Act
        var version = About.Version;
        
        // Assert
        Assert.IsNotNull(version);
        Console.WriteLine($"HwpLib Version: {version}");
    }

    [TestMethod]
    public void About_PrintInfo_ShouldNotThrow()
    {
        // Act & Assert - should not throw
        About.PrintInfo();
    }
}
