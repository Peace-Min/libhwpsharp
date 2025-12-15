using HwpLib.Object.BodyText;
using HwpLib.Object.BodyText.Control;
using HwpLib.Tool.ParagraphAdder.DocInfo;

namespace HwpLib.Tool.ParagraphAdder.Control
{
    /// <summary>
    /// 글자 겹침 컨트롤을 복사하는 클래스
    /// </summary>
    public class OverlappingLetterCopier
    {
        public static void Copy(ControlOverlappingLetter source, ControlOverlappingLetter target, DocInfoAdder? docInfoAdder)
        {
            CopyHeader(source.GetHeader(), target.GetHeader(), docInfoAdder);
            CtrlDataCopier.Copy(source, target, docInfoAdder);
        }

        private static void CopyHeader(HwpLib.Object.BodyText.Control.CtrlHeader.CtrlHeaderOverlappingLetter? source,
                                       HwpLib.Object.BodyText.Control.CtrlHeader.CtrlHeaderOverlappingLetter? target,
                                       DocInfoAdder? docInfoAdder)
        {
            if (source == null || target == null) return;

            target.BorderType = source.BorderType;
            target.ExpendInsideLetter = source.ExpendInsideLetter;
            target.InternalFontSize = source.InternalFontSize;

            foreach (var s in source.OverlappingLetterList)
            {
                target.AddOverlappingLetter(s.Clone());
            }

            foreach (var charShapeId in source.CharShapeIdList)
            {
                target.AddCharShapeId(docInfoAdder == null ? charShapeId : (uint)docInfoAdder.ForCharShapeInfo().ProcessById((int)charShapeId));
            }
        }
    }
}
