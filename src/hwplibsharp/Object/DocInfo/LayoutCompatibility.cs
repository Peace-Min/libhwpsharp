namespace HwpLib.Object.DocInfo
{

    /// <summary>
    /// 레이아웃 호환성에 대한 레코드
    /// </summary>
    public class LayoutCompatibility
    {
        private uint _letterLevelFormat;
        private uint _paragraphLevelFormat;
        private uint _sectionLevelFormat;
        private uint _objectLevelFormat;
        private uint _fieldLevelFormat;

        public LayoutCompatibility()
        {
        }

        public uint LetterLevelFormat
        {
            get => _letterLevelFormat;
            set => _letterLevelFormat = value;
        }

        public uint ParagraphLevelFormat
        {
            get => _paragraphLevelFormat;
            set => _paragraphLevelFormat = value;
        }

        public uint SectionLevelFormat
        {
            get => _sectionLevelFormat;
            set => _sectionLevelFormat = value;
        }

        public uint ObjectLevelFormat
        {
            get => _objectLevelFormat;
            set => _objectLevelFormat = value;
        }

        public uint FieldLevelFormat
        {
            get => _fieldLevelFormat;
            set => _fieldLevelFormat = value;
        }

        public LayoutCompatibility Clone()
        {
            var cloned = new LayoutCompatibility();
            cloned._letterLevelFormat = _letterLevelFormat;
            cloned._paragraphLevelFormat = _paragraphLevelFormat;
            cloned._sectionLevelFormat = _sectionLevelFormat;
            cloned._objectLevelFormat = _objectLevelFormat;
            cloned._fieldLevelFormat = _fieldLevelFormat;
            return cloned;
        }
    }

}