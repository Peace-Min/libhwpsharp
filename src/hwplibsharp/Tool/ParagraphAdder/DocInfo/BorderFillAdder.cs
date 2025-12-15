using HwpLib.Object.DocInfo;
using HwpLib.Object.DocInfo.BorderFill;

namespace HwpLib.Tool.ParagraphAdder.DocInfo
{
    /// <summary>
    /// DocInfo에 BorderFillInfo을 복사하는 기능을 포함하는 클래스
    /// </summary>
    public class BorderFillInfoAdder
    {
        private DocInfoAdder _docInfoAdder;
        private Dictionary<int, int> _idMatchingMap;

        public BorderFillInfoAdder(DocInfoAdder docInfoAdder)
        {
            _docInfoAdder = docInfoAdder;
            _idMatchingMap = new Dictionary<int, int>();
        }

        public int ProcessById(int sourceId)
        {
            if (_docInfoAdder.GetSourceHWPFile() == _docInfoAdder.GetTargetHWPFile())
            {
                return sourceId;
            }

            if (_idMatchingMap.TryGetValue(sourceId, out int cachedId))
            {
                return cachedId;
            }
            else
            {
                BorderFillInfo? source = null;
                try
                {
                    var list = _docInfoAdder.GetSourceHWPFile()?.DocInfo?.BorderFillList;
                    if (list != null && sourceId - 1 >= 0 && sourceId - 1 < list.Count)
                    {
                        source = list[sourceId - 1];
                    }
                }
                catch
                {
                    return sourceId;
                }

                if (source == null) return sourceId;

                int id = FindFromTarget(source);
                if (id == -1)
                {
                    id = AddAndCopy(source);
                }
                _idMatchingMap[sourceId] = id;
                return id;
            }
        }

        private int FindFromTarget(BorderFillInfo source)
        {
            var list = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.BorderFillList;
            if (list == null) return -1;

            int count = list.Count;
            for (int index = 0; index < count; index++)
            {
                var target = list[index];
                if (Equal(source, target))
                {
                    return index + 1;
                }
            }
            return -1;
        }

        private bool Equal(BorderFillInfo? source, BorderFillInfo? target)
        {
            if (source == null || target == null)
            {
                return source == target;
            }

            return source.Property?.Value == target.Property?.Value
                && EqualEachBorder(source.LeftBorder, target.LeftBorder)
                && EqualEachBorder(source.RightBorder, target.RightBorder)
                && EqualEachBorder(source.TopBorder, target.TopBorder)
                && EqualEachBorder(source.BottomBorder, target.BottomBorder)
                && EqualEachBorder(source.DiagonalBorder, target.DiagonalBorder)
                && ForFillInfo.Equal(source.FillInfo, target.FillInfo);
        }

        private bool EqualEachBorder(EachBorder? source, EachBorder? target)
        {
            if (source == null || target == null) return source == target;

            return source.Type == target.Type
                && source.Thickness == target.Thickness
                && source.Color?.Value == target.Color?.Value;
        }

        private int AddAndCopy(BorderFillInfo source)
        {
            var target = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.AddNewBorderFill();
            if (target == null) return -1;

            if (target.Property != null && source.Property != null)
            {
                target.Property.Value = source.Property.Value;
            }
            CopyEachBorder(source.LeftBorder, target.LeftBorder);
            CopyEachBorder(source.RightBorder, target.RightBorder);
            CopyEachBorder(source.TopBorder, target.TopBorder);
            CopyEachBorder(source.BottomBorder, target.BottomBorder);
            CopyEachBorder(source.DiagonalBorder, target.DiagonalBorder);
            ForFillInfo.Copy(source.FillInfo, target.FillInfo, _docInfoAdder);

            return _docInfoAdder.GetTargetHWPFile()?.DocInfo?.BorderFillList?.Count ?? -1;
        }

        private void CopyEachBorder(EachBorder? source, EachBorder? target)
        {
            if (source == null || target == null) return;

            target.Type = source.Type;
            target.Thickness = source.Thickness;
            if (target.Color != null && source.Color != null)
            {
                target.Color.Value = source.Color.Value;
            }
        }

        public bool EqualById(int sourceId, int targetId)
        {
            if (sourceId == 0 || targetId == 0)
            {
                return sourceId == targetId;
            }

            var source = GetBorderFillInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.BorderFillList, sourceId - 1);
            var target = GetBorderFillInfo(_docInfoAdder.GetTargetHWPFile()?.DocInfo?.BorderFillList, targetId - 1);
            return Equal(source, target);
        }

        private BorderFillInfo? GetBorderFillInfo(IReadOnlyList<BorderFillInfo>? borderFillList, int index)
        {
            if (borderFillList == null) return null;

            int count = borderFillList.Count;
            if (count == 0)
            {
                return null;
            }
            if (index >= count)
            {
                return borderFillList[count - 1];
            }
            else if (index < 0)
            {
                return borderFillList[0];
            }
            else
            {
                return borderFillList[index];
            }
        }
    }
}
