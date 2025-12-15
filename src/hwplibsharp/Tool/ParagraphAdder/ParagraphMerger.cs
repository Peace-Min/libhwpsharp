using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Paragraph;
using HwpLib.Object.BodyText.Paragraph.CharShape;
using HwpLib.Object.BodyText.Paragraph.Text;
using HwpLib.Tool.ParagraphAdder.Control;
using HwpLib.Tool.ParagraphAdder.DocInfo;
using System;

namespace HwpLib.Tool.ParagraphAdder
{
    /// <summary>
    /// 문단을 병합하는 기능을 포함하는 클래스
    /// </summary>
    public class ParagraphMerger
    {
        private DocInfoAdder? _docInfoAdder;
        private Paragraph? _source;
        private Paragraph? _target;
        private int _targetCharPosition;
        private int _sourceCharPosition;
        private int _sourceCharShapeIndex;
        private int _sourceControlIndex;

        public ParagraphMerger()
        {
            _docInfoAdder = null;
        }

        public ParagraphMerger(DocInfoAdder docInfoAdder)
        {
            _docInfoAdder = docInfoAdder;
        }

        /// <summary>
        /// source 문단을 target 문단에 병합한다.
        /// </summary>
        public void Merge(Paragraph source, Paragraph target)
        {
            _source = source;
            _target = target;

            RemoveLastParaBreakCharFromTarget();
            MoveTextAndCharShapeInfoAndControl();

            DeleteLineSeg();
            DeleteRangeTag();
            CopyMemoList();
        }

        private void RemoveLastParaBreakCharFromTarget()
        {
            if (_target?.Text == null) return;

            var charList = _target.Text!.CharList;
            if (charList.Count > 0)
            {
                var lastChar = charList[charList.Count - 1];
                if (lastChar.Type == HWPCharType.ControlChar && lastChar.Code == 13)
                {
                    _target.Text!.RemoveCharAt(charList.Count - 1);
                }
            }
        }

        private void MoveTextAndCharShapeInfoAndControl()
        {
            if (_target?.Text == null)
            {
                _target?.CreateText();
            }

            if (_source?.Text == null || _target?.Text == null) return;

            _targetCharPosition = _target.Text!.GetCharSize();
            _sourceCharPosition = 0;
            _sourceCharShapeIndex = 0;
            _sourceControlIndex = 0;

            foreach (var hwpChar in _source.Text!.CharList)
            {
                switch (hwpChar.Type)
                {
                    case HWPCharType.Normal:
                        MoveCharAndCharShapeInfo(hwpChar);
                        break;
                    case HWPCharType.ControlChar:
                        MoveCharAndCharShapeInfo(hwpChar);
                        break;
                    case HWPCharType.ControlInline:
                        MoveCharAndCharShapeInfo(hwpChar);
                        break;
                    case HWPCharType.ControlExtend:
                        MoveExtendChar((HWPCharControlExtend)hwpChar);
                        break;
                }

                _sourceCharPosition += hwpChar.CharSize;
            }
        }

        private void MoveCharAndCharShapeInfo(HWPChar hwpChar)
        {
            _target?.Text?.AddChar(hwpChar);
            MoveCharSpace();
            _targetCharPosition += hwpChar.CharSize;
        }

        private void MoveCharSpace()
        {
            if (_source?.CharShape == null || _target?.CharShape == null) return;

            var pairList = _source.CharShape!.PositionShapeIdPairList;
            if (_sourceCharShapeIndex < pairList.Count)
            {
                var cpsip = pairList[_sourceCharShapeIndex];
                if (cpsip.Position <= _sourceCharPosition)
                {
                    _target.CharShape!.AddParaCharShape(
                        _targetCharPosition,
                        _docInfoAdder == null ? cpsip.ShapeId : _docInfoAdder.ForCharShapeInfo().ProcessById((int)cpsip.ShapeId));
                    _sourceCharShapeIndex++;
                }
            }
        }

        private void MoveExtendChar(HWPCharControlExtend hwpChar)
        {
            if (CanMoveExtendChar(hwpChar))
            {
                _target?.Text?.AddChar(hwpChar);
                MoveCharSpace();
                _targetCharPosition += hwpChar.CharSize;
                
                var controlList = _source?.ControlList;
                if (controlList != null && _sourceControlIndex < controlList.Count)
                {
                    MoveControl(controlList[_sourceControlIndex]);
                }
            }
            _sourceControlIndex++;
        }

        private bool CanMoveExtendChar(HWPCharControlExtend hwpChar)
        {
            int code = hwpChar.Code;
            return code == 3       // 필드 시작
                || code == 11      // 그리기 개체/표/수식/양식 개체
                || code == 15      // 숨은 설명
                || code == 16      // 머리말/꼬리말
                || code == 17      // 각주/미주
                || code == 18      // 자동번호
                || code == 21      // 페이지 컨트롤
                || code == 22      // 책갈피/찾아보기 표식
                || code == 23;     // 덧말/글자 겹침
        }

        private void MoveControl(HwpLib.Object.BodyText.Control.Control sourceControl)
        {
            if (_target?.ControlList == null)
            {
                _target?.CreateControlList();
            }

            if (_target == null) return;

            switch (sourceControl.Type)
            {
                case ControlType.Table:
                    TableCopier.Copy((ControlTable)sourceControl, (ControlTable)_target.AddNewControl(ControlType.Table), _docInfoAdder);
                    break;
                case ControlType.Gso:
                    GsoCopier.Copy((GsoControl)sourceControl, _target.AddNewGsoControl(((GsoControl)sourceControl).GsoType), _docInfoAdder);
                    break;
                case ControlType.Equation:
                    EquationCopier.Copy((ControlEquation)sourceControl, (ControlEquation)_target.AddNewControl(ControlType.Equation), _docInfoAdder);
                    break;
                case ControlType.Header:
                    ETCControlCopier.CopyHeader((ControlHeader)sourceControl, (ControlHeader)_target.AddNewControl(ControlType.Header), _docInfoAdder);
                    break;
                case ControlType.Footer:
                    ETCControlCopier.CopyFooter((ControlFooter)sourceControl, (ControlFooter)_target.AddNewControl(ControlType.Footer), _docInfoAdder);
                    break;
                case ControlType.Footnote:
                    ETCControlCopier.CopyFootnote((ControlFootnote)sourceControl, (ControlFootnote)_target.AddNewControl(ControlType.Footnote), _docInfoAdder);
                    break;
                case ControlType.Endnote:
                    ETCControlCopier.CopyEndnote((ControlEndnote)sourceControl, (ControlEndnote)_target.AddNewControl(ControlType.Endnote), _docInfoAdder);
                    break;
                case ControlType.AutoNumber:
                    ETCControlCopier.CopyAutoNumber((ControlAutoNumber)sourceControl, (ControlAutoNumber)_target.AddNewControl(ControlType.AutoNumber), _docInfoAdder);
                    break;
                case ControlType.NewNumber:
                    ETCControlCopier.CopyNewNumber((ControlNewNumber)sourceControl, (ControlNewNumber)_target.AddNewControl(ControlType.NewNumber), _docInfoAdder);
                    break;
                case ControlType.PageHide:
                    ETCControlCopier.CopyPageHide((ControlPageHide)sourceControl, (ControlPageHide)_target.AddNewControl(ControlType.PageHide), _docInfoAdder);
                    break;
                case ControlType.PageOddEvenAdjust:
                    ETCControlCopier.CopyPageOddEvenAdjust((ControlPageOddEvenAdjust)sourceControl, (ControlPageOddEvenAdjust)_target.AddNewControl(ControlType.PageOddEvenAdjust), _docInfoAdder);
                    break;
                case ControlType.PageNumberPosition:
                    ETCControlCopier.CopyPageNumberPosition((ControlPageNumberPosition)sourceControl, (ControlPageNumberPosition)_target.AddNewControl(ControlType.PageNumberPosition), _docInfoAdder);
                    break;
                case ControlType.IndexMark:
                    ETCControlCopier.CopyIndexMark((ControlIndexMark)sourceControl, (ControlIndexMark)_target.AddNewControl(ControlType.IndexMark), _docInfoAdder);
                    break;
                case ControlType.Bookmark:
                    ETCControlCopier.CopyBookmark((ControlBookmark)sourceControl, (ControlBookmark)_target.AddNewControl(ControlType.Bookmark), _docInfoAdder);
                    break;
                case ControlType.OverlappingLetter:
                    OverlappingLetterCopier.Copy((ControlOverlappingLetter)sourceControl, (ControlOverlappingLetter)_target.AddNewControl(ControlType.OverlappingLetter), _docInfoAdder);
                    break;
                case ControlType.AdditionalText:
                    AdditionalTextCopier.Copy((ControlAdditionalText)sourceControl, (ControlAdditionalText)_target.AddNewControl(ControlType.AdditionalText), _docInfoAdder);
                    break;
                case ControlType.HiddenComment:
                    ETCControlCopier.CopyHiddenComment((ControlHiddenComment)sourceControl, (ControlHiddenComment)_target.AddNewControl(ControlType.HiddenComment), _docInfoAdder);
                    break;
                case ControlType.Form:
                    ETCControlCopier.CopyForm((ControlForm)sourceControl, (ControlForm)_target.AddNewControl(ControlType.Form), _docInfoAdder);
                    break;
                default:
                    if (sourceControl.IsField)
                    {
                        ETCControlCopier.CopyField((ControlField)sourceControl, (ControlField)_target.AddNewControl(((ControlField)sourceControl).GetHeader()?.CtrlId ?? 0), _docInfoAdder);
                    }
                    break;
            }
        }

        private void DeleteLineSeg()
        {
            _target?.DeleteLineSeg();
        }

        private void DeleteRangeTag()
        {
            _target?.DeleteRangeTag();
        }

        private void CopyMemoList()
        {
            ParagraphCopier.CopyMemoList(_source, _target, _docInfoAdder);
        }
    }
}
