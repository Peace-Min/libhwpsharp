namespace HwpLib.Tool.TextExtractor
{
    /// <summary>
    /// 텍스트 추출 리스너 인터페이스
    /// </summary>
    public interface ITextExtractorListener
    {
        void ParagraphText(string text);
    }
}
