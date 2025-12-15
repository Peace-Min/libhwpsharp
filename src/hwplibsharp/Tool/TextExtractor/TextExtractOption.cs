namespace HwpLib.Tool.TextExtractor
{
    /// <summary>
    /// 텍스트 추출 옵션
    /// </summary>
    public class TextExtractOption
    {
        private TextExtractMethod _method;
        private bool _withControlChar;
        private bool _appendEndingLF;
        private bool _insertParaHead;

        public TextExtractOption()
        {
            _method = TextExtractMethod.InsertControlTextBetweenParagraphText;
            _withControlChar = false;
            _appendEndingLF = true;
            _insertParaHead = true;
        }

        public TextExtractOption(TextExtractMethod method)
        {
            _method = method;
            _withControlChar = false;
            _appendEndingLF = true;
            _insertParaHead = true;
        }

        public TextExtractOption(TextExtractMethod method, bool appendEndingLF)
        {
            _method = method;
            _withControlChar = false;
            _appendEndingLF = appendEndingLF;
            _insertParaHead = true;
        }

        public TextExtractOption(TextExtractOption that)
        {
            _method = that._method;
            _withControlChar = that._withControlChar;
            _appendEndingLF = that._appendEndingLF;
            _insertParaHead = that._insertParaHead;
        }

        public TextExtractMethod GetMethod() => _method;
        public void SetMethod(TextExtractMethod method) => _method = method;

        public bool IsWithControlChar() => _withControlChar;
        public void SetWithControlChar(bool withControlChar) => _withControlChar = withControlChar;

        public bool IsAppendEndingLF() => _appendEndingLF;
        public void SetAppendEndingLF(bool appendEndingLF) => _appendEndingLF = appendEndingLF;

        public bool IsInsertParaHead() => _insertParaHead;
        public void SetInsertParaHead(bool insertParaHead) => _insertParaHead = insertParaHead;
    }
}
