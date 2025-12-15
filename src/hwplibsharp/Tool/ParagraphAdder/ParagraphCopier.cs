using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Paragraph;
using HwpLib.Object.BodyText.Paragraph.CharShape;
using HwpLib.Object.BodyText.Paragraph.Header;
using HwpLib.Object.BodyText.Paragraph.LineSeg;
using HwpLib.Object.BodyText.Paragraph.RangeTag;
using HwpLib.Tool.ParagraphAdder.Control;
using HwpLib.Tool.ParagraphAdder.DocInfo;
using System;
using Paragraph = HwpLib.Object.BodyText.Paragraph.Paragraph;

namespace HwpLib.Tool.ParagraphAdder
{
    /// <summary>
    /// Paragraph 객체를 복사하는 기능을 포함하는 클래스
    /// </summary>
    public class ParagraphCopier
    {
        private DocInfoAdder? _docInfoAdder;
        private Paragraph? _source;
        private Paragraph? _target;
        private bool _includingSectionInfo;
        private bool _excludedSectionDefine;

        public ParagraphCopier(DocInfoAdder? docInfoAdder)
        {
            _docInfoAdder = docInfoAdder;
        }

        /// <summary>
        /// 문단 리스트를 복사한다.
        /// </summary>
        public static void ListCopy(ParagraphList source, ParagraphList target, DocInfoAdder? docInfoAdder)
        {
            var copier = new ParagraphCopier(docInfoAdder);
            foreach (var p in source)
            {
                try
                {
                    copier.Copy(p, target.AddNewParagraph());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        /// <summary>
        /// 문단을 복사한다.
        /// </summary>
        public void Copy(Paragraph source, Paragraph target)
        {
            _source = source;
            _target = target;
            _includingSectionInfo = false;

            CopyHeader();
            CopyText();
            CopyCharShapeInfo();
            CopyLineSeg();
            CopyRangeTag();
            CopyControlList();
            CopyMemoList();
        }

        /// <summary>
        /// 구역 정보를 포함하여 문단을 복사한다.
        /// </summary>
        public void CopyIncludingSectionInfo(Paragraph source, Paragraph target)
        {
            _source = source;
            _target = target;
            _includingSectionInfo = true;

            CopyHeader();
            CopyText();
            CopyCharShapeInfo();
            CopyLineSeg();
            CopyRangeTag();
            CopyControlList();
            CopyMemoList();
        }

        private void CopyHeader()
        {
            if (_source?.Header == null || _target == null) return;

            var sourceH = _source.Header;
            var targetH = _target.Header;

            if (sourceH == null || targetH == null) return;

            targetH.LastInList = sourceH.LastInList;
            targetH.CharacterCount = sourceH.CharacterCount;
            targetH.ControlMask.Value = sourceH.ControlMask.Value;
            targetH.ParaShapeId = _docInfoAdder == null ? sourceH.ParaShapeId : _docInfoAdder.ForParaShapeInfo().ProcessById(sourceH.ParaShapeId);
            targetH.StyleId = (short)(_docInfoAdder == null ? sourceH.StyleId : _docInfoAdder.ForStyle().ProcessById(sourceH.StyleId));
            targetH.DivideSort.Value = sourceH.DivideSort.Value;
            targetH.CharShapeCount = sourceH.CharShapeCount;
            targetH.RangeTagCount = sourceH.RangeTagCount;
            targetH.LineAlignCount = sourceH.LineAlignCount;
            targetH.InstanceID = 0;
            targetH.IsMergedByTrack = sourceH.IsMergedByTrack;
        }

        private void CopyText()
        {
            if (_source?.Text == null || _target == null) return;

            _target.CreateText();
            _excludedSectionDefine = ParaTextCopier.Copy(_source.Text!, _target.Text!, _includingSectionInfo);
        }

        private void CopyCharShapeInfo()
        {
            if (_source?.CharShape == null || _target == null) return;

            _target.CreateCharShape();

            foreach (var cpsp in _source.CharShape!.PositionShapeIdPairList)
            {
                if (_excludedSectionDefine && cpsp.Position > 0)
                {
                    _target.CharShape!.AddParaCharShape(
                        cpsp.Position - 8,
                        _docInfoAdder == null ? cpsp.ShapeId : _docInfoAdder.ForCharShapeInfo().ProcessById((int)cpsp.ShapeId));
                }
                else
                {
                    _target.CharShape!.AddParaCharShape(
                        cpsp.Position,
                        _docInfoAdder == null ? cpsp.ShapeId : _docInfoAdder.ForCharShapeInfo().ProcessById((int)cpsp.ShapeId));
                }
            }
        }

        private void CopyLineSeg()
        {
            if (_source?.LineSeg == null || _target == null) return;

            _target.CreateLineSeg();
            foreach (var lsi in _source.LineSeg!.LineSegItemList)
            {
                _target.LineSeg!.AddLineSegItem(lsi.Clone());
            }
        }

        private void CopyRangeTag()
        {
            if (_source?.RangeTag == null || _target == null) return;

            _target.CreateRangeTag();
            foreach (var rti in _source.RangeTag!.RangeTagItemList)
            {
                _target.RangeTag!.AddRangeTagItem(rti.Clone());
            }
        }

        private void CopyControlList()
        {
            if (_source?.ControlList == null || _target == null) return;

            foreach (var c in _source.ControlList!)
            {
                switch (c.Type)
                {
                    case ControlType.Table:
                        TableCopier.Copy((ControlTable)c, (ControlTable)_target.AddNewControl(ControlType.Table), _docInfoAdder);
                        break;
                    case ControlType.Gso:
                        GsoCopier.Copy((GsoControl)c, (GsoControl)_target.AddNewGsoControl(((GsoControl)c).GsoType), _docInfoAdder);
                        break;
                    case ControlType.Equation:
                        EquationCopier.Copy((ControlEquation)c, (ControlEquation)_target.AddNewControl(ControlType.Equation), _docInfoAdder);
                        break;
                    case ControlType.SectionDefine:
                        if (_includingSectionInfo)
                        {
                            SectionDefineCopier.Copy((ControlSectionDefine)c, (ControlSectionDefine)_target.AddNewControl(ControlType.SectionDefine), _docInfoAdder);
                        }
                        break;
                    case ControlType.ColumnDefine:
                        ETCControlCopier.CopyColumnDefine((ControlColumnDefine)c, (ControlColumnDefine)_target.AddNewControl(ControlType.ColumnDefine), _docInfoAdder);
                        break;
                    case ControlType.Header:
                        if (_includingSectionInfo)
                        {
                            ETCControlCopier.CopyHeader((ControlHeader)c, (ControlHeader)_target.AddNewControl(ControlType.Header), _docInfoAdder);
                        }
                        break;
                    case ControlType.Footer:
                        if (_includingSectionInfo)
                        {
                            ETCControlCopier.CopyFooter((ControlFooter)c, (ControlFooter)_target.AddNewControl(ControlType.Footer), _docInfoAdder);
                        }
                        break;
                    case ControlType.Footnote:
                        ETCControlCopier.CopyFootnote((ControlFootnote)c, (ControlFootnote)_target.AddNewControl(ControlType.Footnote), _docInfoAdder);
                        break;
                    case ControlType.Endnote:
                        ETCControlCopier.CopyEndnote((ControlEndnote)c, (ControlEndnote)_target.AddNewControl(ControlType.Endnote), _docInfoAdder);
                        break;
                    case ControlType.AutoNumber:
                        ETCControlCopier.CopyAutoNumber((ControlAutoNumber)c, (ControlAutoNumber)_target.AddNewControl(ControlType.AutoNumber), _docInfoAdder);
                        break;
                    case ControlType.NewNumber:
                        ETCControlCopier.CopyNewNumber((ControlNewNumber)c, (ControlNewNumber)_target.AddNewControl(ControlType.NewNumber), _docInfoAdder);
                        break;
                    case ControlType.PageHide:
                        ETCControlCopier.CopyPageHide((ControlPageHide)c, (ControlPageHide)_target.AddNewControl(ControlType.PageHide), _docInfoAdder);
                        break;
                    case ControlType.PageOddEvenAdjust:
                        ETCControlCopier.CopyPageOddEvenAdjust((ControlPageOddEvenAdjust)c, (ControlPageOddEvenAdjust)_target.AddNewControl(ControlType.PageOddEvenAdjust), _docInfoAdder);
                        break;
                    case ControlType.PageNumberPosition:
                        ETCControlCopier.CopyPageNumberPosition((ControlPageNumberPosition)c, (ControlPageNumberPosition)_target.AddNewControl(ControlType.PageNumberPosition), _docInfoAdder);
                        break;
                    case ControlType.IndexMark:
                        ETCControlCopier.CopyIndexMark((ControlIndexMark)c, (ControlIndexMark)_target.AddNewControl(ControlType.IndexMark), _docInfoAdder);
                        break;
                    case ControlType.Bookmark:
                        ETCControlCopier.CopyBookmark((ControlBookmark)c, (ControlBookmark)_target.AddNewControl(ControlType.Bookmark), _docInfoAdder);
                        break;
                    case ControlType.OverlappingLetter:
                        OverlappingLetterCopier.Copy((ControlOverlappingLetter)c, (ControlOverlappingLetter)_target.AddNewControl(ControlType.OverlappingLetter), _docInfoAdder);
                        break;
                    case ControlType.AdditionalText:
                        AdditionalTextCopier.Copy((ControlAdditionalText)c, (ControlAdditionalText)_target.AddNewControl(ControlType.AdditionalText), _docInfoAdder);
                        break;
                    case ControlType.HiddenComment:
                        ETCControlCopier.CopyHiddenComment((ControlHiddenComment)c, (ControlHiddenComment)_target.AddNewControl(ControlType.HiddenComment), _docInfoAdder);
                        break;
                    case ControlType.Form:
                        ETCControlCopier.CopyForm((ControlForm)c, (ControlForm)_target.AddNewControl(ControlType.Form), _docInfoAdder);
                        break;
                    default:
                        // Field 컨트롤 처리
                        if (c.Type.IsField())
                        {
                            ETCControlCopier.CopyField((ControlField)c, (ControlField)_target.AddNewControl(((ControlField)c).GetHeader()?.CtrlId ?? 0), _docInfoAdder);
                        }
                        break;
                }
            }
        }

        private void CopyMemoList()
        {
            CopyMemoList(_source, _target, _docInfoAdder);
        }

        public static void CopyMemoList(Paragraph? source, Paragraph? target, DocInfoAdder? docInfoAdder)
        {
            // 메모 리스트 복사 - 추후 구현
        }
    }
}
