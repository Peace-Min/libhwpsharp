using HwpLib.Object.DocInfo;
using HwpLib.Object.DocInfo.TabDef;

namespace HwpLib.Tool.ParagraphAdder.DocInfo
{
    /// <summary>
    /// DocInfo에 TabDefInfo을 복사하는 기능을 포함하는 클래스
    /// </summary>
    public class TabDefInfoAdder
    {
        private DocInfoAdder _docInfoAdder;
        private Dictionary<int, int> _idMatchingMap;

        public TabDefInfoAdder(DocInfoAdder docInfoAdder)
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
                // id == index
                TabDefInfo? source;
                try
                {
                    var list = _docInfoAdder.GetSourceHWPFile()?.DocInfo?.TabDefList;
                    if (list == null || sourceId < 0 || sourceId >= list.Count)
                    {
                        return sourceId;
                    }
                    source = list[sourceId];
                }
                catch
                {
                    return sourceId;
                }

                int id = FindFromTarget(source);
                if (id == -1)
                {
                    id = AddAndCopy(source);
                }
                _idMatchingMap[sourceId] = id;
                return id;
            }
        }

        private int FindFromTarget(TabDefInfo source)
        {
            var list = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.TabDefList;
            if (list == null) return -1;

            int count = list.Count;
            for (int index = 0; index < count; index++)
            {
                var target = list[index];
                if (Equal(source, target))
                {
                    return index;
                }
            }
            return -1;
        }

        private bool Equal(TabDefInfo? source, TabDefInfo? target)
        {
            if (source == null || target == null) return source == target;

            return source.Property?.Value == target.Property?.Value
                && EqualTabInfoList(source.TabInfoList, target.TabInfoList);
        }

        private bool EqualTabInfoList(IReadOnlyList<TabInfo>? source, IReadOnlyList<TabInfo>? target)
        {
            if (source == null || target == null) return source == target;
            if (source.Count != target.Count) return false;

            int count = source.Count;
            for (int index = 0; index < count; index++)
            {
                if (!EqualTabInfo(source[index], target[index]))
                {
                    return false;
                }
            }
            return true;
        }

        private bool EqualTabInfo(TabInfo? source, TabInfo? target)
        {
            if (source == null || target == null) return source == target;

            return source.Position == target.Position
                && source.TabSort == target.TabSort
                && source.FillSort == target.FillSort;
        }

        private int AddAndCopy(TabDefInfo source)
        {
            var target = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.AddNewTabDef();
            if (target == null) return -1;

            if (target.Property != null && source.Property != null)
            {
                target.Property.Value = source.Property.Value;
            }

            var sourceTabInfoList = source.TabInfoList;
            if (sourceTabInfoList != null)
            {
                foreach (var sourceTabInfo in sourceTabInfoList)
                {
                    var targetTabInfo = target.AddNewTabInfo();
                    CopyTabInfo(sourceTabInfo, targetTabInfo);
                }
            }

            var list = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.TabDefList;
            return (list?.Count ?? 1) - 1;
        }

        private void CopyTabInfo(TabInfo source, TabInfo target)
        {
            target.Position = source.Position;
            target.TabSort = source.TabSort;
            target.FillSort = source.FillSort;
        }

        public bool EqualById(int sourceId, int targetId)
        {
            try
            {
                var source = _docInfoAdder.GetSourceHWPFile()?.DocInfo?.TabDefList?[sourceId];
                var target = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.TabDefList?[targetId];
                return Equal(source, target);
            }
            catch
            {
                return false;
            }
        }
    }
}
