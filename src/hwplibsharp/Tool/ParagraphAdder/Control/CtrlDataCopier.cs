using HwpLib.Tool.ParagraphAdder.DocInfo;

namespace HwpLib.Tool.ParagraphAdder.Control
{
    /// <summary>
    /// 컨트롤 데이터를 복사하는 클래스
    /// </summary>
    public class CtrlDataCopier
    {
        public static void Copy(HwpLib.Object.BodyText.Control.Control source, HwpLib.Object.BodyText.Control.Control target, DocInfoAdder? docInfoAdder)
        {
            if (source.GetControlData() != null)
            {
                target.CreateCtrlData();
                ParameterSetCopier.Copy(source.GetControlData()!.ParameterSet, target.GetControlData()!.ParameterSet, docInfoAdder);
            }
            else
            {
                target.DeleteCtrlData();
            }
        }
    }
}
