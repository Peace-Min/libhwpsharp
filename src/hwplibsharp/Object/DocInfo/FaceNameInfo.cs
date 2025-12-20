using HwpLib.Object.DocInfo.FaceName;


namespace HwpLib.Object.DocInfo
{

    /// <summary>
    /// 글꼴 레코드
    /// </summary>
    public class FaceNameInfo
    {
        private readonly FaceNameProperty _property;
        private string? _name;
        private FontType _substituteFontType;
        private string? _substituteFontName;
        private readonly FontTypeInfo _fontTypeInfo;
        private string? _baseFontName;

        public FaceNameInfo()
        {
            _property = new FaceNameProperty();
            _fontTypeInfo = new FontTypeInfo();
        }

        public FaceNameProperty Property => _property;

        public string? Name
        {
            get => _name;
            set => _name = value;
        }

        public FontType SubstituteFontType
        {
            get => _substituteFontType;
            set => _substituteFontType = value;
        }

        public string? SubstituteFontName
        {
            get => _substituteFontName;
            set => _substituteFontName = value;
        }

        public FontTypeInfo FontTypeInfo => _fontTypeInfo;

        public string? BaseFontName
        {
            get => _baseFontName;
            set => _baseFontName = value;
        }

        public FaceNameInfo Clone()
        {
            var cloned = new FaceNameInfo();
            cloned._property.Copy(_property);
            cloned._name = _name;
            cloned._substituteFontType = _substituteFontType;
            cloned._substituteFontName = _substituteFontName;
            cloned._fontTypeInfo.Copy(_fontTypeInfo);
            cloned._baseFontName = _baseFontName;
            return cloned;
        }
    }

}