using HwpLib.Object.DocInfo.ParaShape;


namespace HwpLib.Object.DocInfo
{

    /// <summary>
    /// 문단 모양에 대한 레코드
    /// </summary>
    public class ParaShapeInfo
    {
        private readonly ParaShapeProperty1 _property1;
        private int _leftMargin;
        private int _rightMargin;
        private int _indent;
        private int _topParaSpace;
        private int _bottomParaSpace;
        private int _lineSpace;
        private int _tabDefId;
        private int _paraHeadId;
        private int _borderFillId;
        private short _leftBorderSpace;
        private short _rightBorderSpace;
        private short _topBorderSpace;
        private short _bottomBorderSpace;
        private readonly ParaShapeProperty2 _property2;
        private readonly ParaShapeProperty3 _property3;
        private uint _lineSpace2;
        private uint _paraLevel;

        public ParaShapeInfo()
        {
            _property1 = new ParaShapeProperty1();
            _property2 = new ParaShapeProperty2();
            _property3 = new ParaShapeProperty3();
        }

        public ParaShapeProperty1 Property1 => _property1;

        public int LeftMargin
        {
            get => _leftMargin;
            set => _leftMargin = value;
        }

        public int RightMargin
        {
            get => _rightMargin;
            set => _rightMargin = value;
        }

        public int Indent
        {
            get => _indent;
            set => _indent = value;
        }

        public int TopParaSpace
        {
            get => _topParaSpace;
            set => _topParaSpace = value;
        }

        public int BottomParaSpace
        {
            get => _bottomParaSpace;
            set => _bottomParaSpace = value;
        }

        /// <summary>
        /// 줄 간격. 한글 2007 이하 버전(5.0.2.5 버전 미만)에서 사용
        /// </summary>
        public int LineSpace
        {
            get => _lineSpace;
            set => _lineSpace = value;
        }

        public int TabDefId
        {
            get => _tabDefId;
            set => _tabDefId = value;
        }

        public int ParaHeadId
        {
            get => _paraHeadId;
            set => _paraHeadId = value;
        }

        public int BorderFillId
        {
            get => _borderFillId;
            set => _borderFillId = value;
        }

        public short LeftBorderSpace
        {
            get => _leftBorderSpace;
            set => _leftBorderSpace = value;
        }

        public short RightBorderSpace
        {
            get => _rightBorderSpace;
            set => _rightBorderSpace = value;
        }

        public short TopBorderSpace
        {
            get => _topBorderSpace;
            set => _topBorderSpace = value;
        }

        public short BottomBorderSpace
        {
            get => _bottomBorderSpace;
            set => _bottomBorderSpace = value;
        }

        /// <summary>
        /// 속성 2 (5.0.1.7 버전 이상)
        /// </summary>
        public ParaShapeProperty2 Property2 => _property2;

        /// <summary>
        /// 속성 3 (5.0.2.5 버전 이상)
        /// </summary>
        public ParaShapeProperty3 Property3 => _property3;

        /// <summary>
        /// 줄 간격 (5.0.2.5 버전 이상)
        /// </summary>
        public uint LineSpace2
        {
            get => _lineSpace2;
            set => _lineSpace2 = value;
        }

        /// <summary>
        /// 개요 수준 (5.1.0.0 버전 이상)
        /// property1.paraLevel 대신 사용하는 듯.
        /// </summary>
        public uint ParaLevel
        {
            get => _paraLevel;
            set => _paraLevel = value;
        }

        public ParaShapeInfo Clone()
        {
            var cloned = new ParaShapeInfo();
            cloned._property1.Copy(_property1);
            cloned._leftMargin = _leftMargin;
            cloned._rightMargin = _rightMargin;
            cloned._indent = _indent;
            cloned._topParaSpace = _topParaSpace;
            cloned._bottomParaSpace = _bottomParaSpace;
            cloned._lineSpace = _lineSpace;
            cloned._tabDefId = _tabDefId;
            cloned._paraHeadId = _paraHeadId;
            cloned._borderFillId = _borderFillId;
            cloned._leftBorderSpace = _leftBorderSpace;
            cloned._rightBorderSpace = _rightBorderSpace;
            cloned._topBorderSpace = _topBorderSpace;
            cloned._bottomBorderSpace = _bottomBorderSpace;
            cloned._property2.Copy(_property2);
            cloned._property3.Copy(_property3);
            cloned._lineSpace2 = _lineSpace2;
            cloned._paraLevel = _paraLevel;
            return cloned;
        }
    }

}