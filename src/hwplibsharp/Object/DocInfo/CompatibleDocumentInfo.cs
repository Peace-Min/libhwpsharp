using HwpLib.Object.DocInfo.CompatibleDocument;


namespace HwpLib.Object.DocInfo
{

    /// <summary>
    /// 호환 문서에 대한 레코드
    /// </summary>
    public class CompatibleDocumentInfo
    {
        private CompatibleDocumentSort _targetProgram;

        public CompatibleDocumentInfo()
        {
        }

        public CompatibleDocumentSort TargetProgram
        {
            get => _targetProgram;
            set => _targetProgram = value;
        }

        public CompatibleDocumentInfo Clone()
        {
            var cloned = new CompatibleDocumentInfo();
            cloned._targetProgram = _targetProgram;
            return cloned;
        }
    }

}