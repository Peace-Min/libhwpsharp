namespace HwpLibSharp.Test;

/// <summary>
/// 테스트용 헬퍼 클래스
/// </summary>
public static class TestHelper
{
    /// <summary>
    /// 프로젝트 루트 경로를 가져옵니다.
    /// </summary>
    public static string GetProjectRoot()
    {
        // 테스트 실행 디렉토리에서 프로젝트 루트로 이동
        var currentDir = Directory.GetCurrentDirectory();
        var dir = new DirectoryInfo(currentDir);

        // sample_hwp 폴더를 찾을 때까지 상위로 이동
        while (dir != null && !Directory.Exists(Path.Combine(dir.FullName, "sample_hwp")))
        {
            dir = dir.Parent;
        }

        return dir?.FullName ?? throw new DirectoryNotFoundException("프로젝트 루트를 찾을 수 없습니다.");
    }

    /// <summary>
    /// 샘플 HWP 파일의 전체 경로를 반환합니다.
    /// </summary>
    public static string GetSamplePath(string filename)
    {
        return Path.Combine(GetProjectRoot(), "sample_hwp", filename);
    }

    /// <summary>
    /// 기본 샘플 HWP 파일의 전체 경로를 반환합니다.
    /// </summary>
    public static string GetBasicSamplePath(string filename)
    {
        return Path.Combine(GetProjectRoot(), "sample_hwp", "basic", filename);
    }

    /// <summary>
    /// 결과 파일의 전체 경로를 반환합니다.
    /// </summary>
    public static string GetResultPath(string filename)
    {
        var resultDir = Path.Combine(GetProjectRoot(), "sample_hwp", "result");
        if (!Directory.Exists(resultDir))
        {
            Directory.CreateDirectory(resultDir);
        }
        return Path.Combine(resultDir, filename);
    }

    /// <summary>
    /// 이미지 파일의 전체 경로를 반환합니다.
    /// </summary>
    public static string GetImagePath(string filename)
    {
        return Path.Combine(GetProjectRoot(), "sample_hwp", "image", filename);
    }

    /// <summary>
    /// mm를 HWP 좌표로 변환합니다.
    /// </summary>
    public static long MmToHwp(double mm)
    {
        return (long)(mm * 72000.0 / 254.0 + 0.5);
    }

    /// <summary>
    /// 포인트를 기본 크기로 변환합니다.
    /// </summary>
    public static int PtToBaseSize(int pt)
    {
        return pt * 100;
    }

    /// <summary>
    /// 포인트를 라인 높이로 변환합니다.
    /// </summary>
    public static int PtToLineHeight(double pt)
    {
        return (int)(pt * 100.0);
    }

    /// <summary>
    /// 기본 샘플 파일 목록
    /// </summary>
    public static readonly string[] BasicSampleFiles = new[]
    {
        "blank.hwp",
        "etc.hwp",
        "ole.hwp",
        "각주미주.hwp",
        "구버전(5.0.2.2) Picture 컨트롤.hwp",
        "그림.hwp",
        "글상자.hwp",
        "글상자-압축.hwp",
        "글자겹침.hwp",
        "다각형.hwp",
        "덧말.hwp",
        "머리글꼬리글.hwp",
        "묶음.hwp",
        "문단번호 1-10 수준.hwp",
        "바탕쪽.hwp",
        "새번호지정.hwp",
        "선-사각형-타원.hwp",
        "수식.hwp",
        "숨은설명.hwp",
        "이미지추가.hwp",
        "차트.hwp",
        "책갈피.hwp",
        "페이지숨김.hwp",
        "표.hwp",
        "필드.hwp",
        "필드-누름틀.hwp",
        "호-곡선.hwp"
    };
}
