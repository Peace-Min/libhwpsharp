using HwpLib.Object.DocInfo.BorderFill;
using HwpLib.Object.DocInfo.BorderFill.FillInfo;


namespace HwpLib.Object.DocInfo
{

    /// <summary>
    /// 테두리/배경의 모양을 나타내는 레코드
    /// </summary>
    public class BorderFillInfo
    {
        private readonly BorderFillProperty _property;
        private readonly EachBorder _leftBorder;
        private readonly EachBorder _rightBorder;
        private readonly EachBorder _topBorder;
        private readonly EachBorder _bottomBorder;
        private readonly EachBorder _diagonalBorder;
        private readonly FillInfo _fillInfo;

        public BorderFillInfo()
        {
            _property = new BorderFillProperty();
            _leftBorder = new EachBorder();
            _rightBorder = new EachBorder();
            _topBorder = new EachBorder();
            _bottomBorder = new EachBorder();
            _diagonalBorder = new EachBorder();
            _fillInfo = new FillInfo();
        }

        public BorderFillProperty Property => _property;
        public EachBorder LeftBorder => _leftBorder;
        public EachBorder RightBorder => _rightBorder;
        public EachBorder TopBorder => _topBorder;
        public EachBorder BottomBorder => _bottomBorder;
        public EachBorder DiagonalBorder => _diagonalBorder;
        public FillInfo FillInfo => _fillInfo;

        public BorderFillInfo Clone()
        {
            var cloned = new BorderFillInfo();
            cloned._property.Copy(_property);
            cloned._leftBorder.Copy(_leftBorder);
            cloned._rightBorder.Copy(_rightBorder);
            cloned._topBorder.Copy(_topBorder);
            cloned._bottomBorder.Copy(_bottomBorder);
            cloned._diagonalBorder.Copy(_diagonalBorder);
            cloned._fillInfo.Copy(_fillInfo);
            return cloned;
        }
    }

}