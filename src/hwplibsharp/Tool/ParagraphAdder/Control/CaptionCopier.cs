using HwpLib.Object.BodyText.Control.Gso.Caption;
using HwpLib.Tool.ParagraphAdder.DocInfo;

namespace HwpLib.Tool.ParagraphAdder.Control
{
    /// <summary>
    /// 캡션을 복사하는 클래스
    /// </summary>
    public class CaptionCopier
    {
        public static void Copy(Caption source, Caption target, DocInfoAdder? docInfoAdder)
        {
            var sourceLH = source.ListHeader;
            var targetLH = target.ListHeader;
            targetLH?.Copy(sourceLH);

            ParagraphCopier.ListCopy(source.ParagraphList, target.ParagraphList, docInfoAdder);
        }
    }
}
