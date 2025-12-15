using HwpLib.Object;
using HwpLib.Object.Etc;

namespace HwpLib.Tool.ParagraphAdder.DocInfo
{
    /// <summary>
    /// DocInfo를 복사하는 기능을 포함하는 클래스
    /// </summary>
    public class DocInfoAdder
    {
        private HWPFile? _sourceHWPFile;
        private HWPFile? _targetHWPFile;

        private BinDataAdder _binDataAdder;
        private BorderFillInfoAdder _borderFillAdder;
        private BulletAdder _bulletAdder;
        private CharShapeInfoAdder _charShapeAdder;
        private FaceNameInfoAdder _faceNameAdder;
        private NumberingAdder _numberingAdder;
        private ParaShapeInfoAdder _paraShapeAdder;
        private StyleAdder _styleAdder;
        private TabDefInfoAdder _tabDefAdder;

        public DocInfoAdder(HWPFile? sourceHWPFile, HWPFile? targetHWPFile)
        {
            _sourceHWPFile = sourceHWPFile;
            _targetHWPFile = targetHWPFile;

            _binDataAdder = new BinDataAdder(this);
            _borderFillAdder = new BorderFillInfoAdder(this);
            _bulletAdder = new BulletAdder(this);
            _charShapeAdder = new CharShapeInfoAdder(this);
            _faceNameAdder = new FaceNameInfoAdder(this);
            _numberingAdder = new NumberingAdder(this);
            _paraShapeAdder = new ParaShapeInfoAdder(this);
            _styleAdder = new StyleAdder(this);
            _tabDefAdder = new TabDefInfoAdder(this);
        }

        public HWPFile? GetSourceHWPFile() => _sourceHWPFile;
        public HWPFile? GetTargetHWPFile() => _targetHWPFile;

        public BinDataAdder ForBinData() => _binDataAdder;
        public BorderFillInfoAdder ForBorderFillInfo() => _borderFillAdder;
        public BulletAdder ForBullet() => _bulletAdder;
        public CharShapeInfoAdder ForCharShapeInfo() => _charShapeAdder;
        public FaceNameInfoAdder ForFaceNameInfo() => _faceNameAdder;
        public NumberingAdder ForNumbering() => _numberingAdder;
        public ParaShapeInfoAdder ForParaShapeInfo() => _paraShapeAdder;
        public StyleAdder ForStyle() => _styleAdder;
        public TabDefInfoAdder ForTabDefInfo() => _tabDefAdder;
    }
}
