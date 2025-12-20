using HwpLib.Object.DocInfo.Style;


namespace HwpLib.Object.DocInfo
{

    /// <summary>
    /// 스타일에 대한 레코드
    /// </summary>
    public class StyleInfo
    {
        private string? _hangulName;
        private string? _englishName;
        private readonly StyleProperty _property;
        private short _nextStyleId;
        private short _languageId;
        private int _paraShapeId;
        private int _charShapeId;

        public StyleInfo()
        {
            _property = new StyleProperty();
        }

        public string? HangulName
        {
            get => _hangulName;
            set => _hangulName = value;
        }

        public string? EnglishName
        {
            get => _englishName;
            set => _englishName = value;
        }

        public StyleProperty Property => _property;

        public short NextStyleId
        {
            get => _nextStyleId;
            set => _nextStyleId = value;
        }

        public short LanguageId
        {
            get => _languageId;
            set => _languageId = value;
        }

        public int ParaShapeId
        {
            get => _paraShapeId;
            set => _paraShapeId = value;
        }

        public int CharShapeId
        {
            get => _charShapeId;
            set => _charShapeId = value;
        }

        public StyleInfo Clone()
        {
            var cloned = new StyleInfo();
            cloned._hangulName = _hangulName;
            cloned._englishName = _englishName;
            cloned._property.Copy(_property);
            cloned._nextStyleId = _nextStyleId;
            cloned._languageId = _languageId;
            cloned._paraShapeId = _paraShapeId;
            cloned._charShapeId = _charShapeId;
            return cloned;
        }
    }

}