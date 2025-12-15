using System.Reflection;

namespace HwpLib;

/// <summary>
/// 라이브러리 정보를 제공하는 클래스
/// </summary>
public static class About
{
    /// <summary>
    /// 라이브러리 버전을 반환한다.
    /// </summary>
    public static string Version => 
        Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.1.7";

    /// <summary>
    /// 원본 라이브러리 저작자
    /// </summary>
    public static string OriginalAuthor => "neolord";

    /// <summary>
    /// .NET 포팅 작업자
    /// </summary>
    public static string PortedBy => "rkttu";

    /// <summary>
    /// 라이브러리 정보를 콘솔에 출력한다.
    /// </summary>
    public static void PrintInfo()
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("HwpLibSharp");
        Console.WriteLine("Copyright for neolord all rights reserved.");
        Console.WriteLine(".NET port by rkttu with AI-assisted translation.");
        Console.WriteLine("=============================================");
        Console.WriteLine($"\tversion = {Version}");
        Console.WriteLine($"\ttarget framework = .NET 10.0");
        Console.WriteLine($"\toriginal author = {OriginalAuthor}");
        Console.WriteLine($"\tported by = {PortedBy}");
    }
}
