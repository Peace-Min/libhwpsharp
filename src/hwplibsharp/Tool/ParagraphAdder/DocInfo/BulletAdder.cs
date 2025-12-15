using HwpLib.Object.DocInfo;
using HwpLib.Object.DocInfo.Numbering;

namespace HwpLib.Tool.ParagraphAdder.DocInfo
{
    /// <summary>
    /// DocInfo에 Bullet을 복사하는 기능을 포함하는 클래스
    /// </summary>
    public class BulletAdder
    {
        private DocInfoAdder _docInfoAdder;
        private Dictionary<int, int> _idMatchingMap;

        public BulletAdder(DocInfoAdder docInfoAdder)
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
                // id == index + 1
                Bullet? source;
                try
                {
                    var list = _docInfoAdder.GetSourceHWPFile()?.DocInfo?.BulletList;
                    if (list == null || sourceId - 1 < 0 || sourceId - 1 >= list.Count)
                    {
                        return sourceId;
                    }
                    source = list[sourceId - 1];
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

        private int FindFromTarget(Bullet source)
        {
            var list = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.BulletList;
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

        private bool Equal(Bullet? source, Bullet? target)
        {
            if (source == null || target == null) return source == target;

            return EqualParagraphHeadInfo(source.ParagraphHeadInfo, target.ParagraphHeadInfo)
                && source.BulletChar?.Equals(target.BulletChar) == true
                && source.CheckBulletChar?.Equals(target.CheckBulletChar) == true
                && (source.ImageBullet == target.ImageBullet) == false;
            // imageBulletInfo.binDataID 비교 불가
        }

        private bool EqualParagraphHeadInfo(ParagraphHeadInfo? source, ParagraphHeadInfo? target)
        {
            if (source == null || target == null) return source == target;

            return source.Property?.Value == target.Property?.Value
                && source.CorrectionValueForWidth == target.CorrectionValueForWidth
                && source.DistanceFromBody == target.DistanceFromBody
                && _docInfoAdder.ForCharShapeInfo().EqualById((int)source.CharShapeID, (int)target.CharShapeID);
        }

        private int AddAndCopy(Bullet source)
        {
            var target = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.AddNewBullet();
            if (target == null) return -1;

            CopyParagraphHeadInfo(source.ParagraphHeadInfo, target.ParagraphHeadInfo);
            target.BulletChar?.Copy(source.BulletChar);
            target.ImageBullet = source.ImageBullet;
            ForFillInfo.CopyPictureInfo(source.ImageBulletInfo, target.ImageBulletInfo, _docInfoAdder);
            target.CheckBulletChar?.Copy(source.CheckBulletChar);

            return _docInfoAdder.GetTargetHWPFile()?.DocInfo?.BulletList?.Count ?? -1;
        }

        private void CopyParagraphHeadInfo(ParagraphHeadInfo? source, ParagraphHeadInfo? target)
        {
            if (source == null || target == null) return;

            if (source.Property != null && target.Property != null)
                target.Property.Value = source.Property.Value;
            target.CorrectionValueForWidth = source.CorrectionValueForWidth;
            target.DistanceFromBody = source.DistanceFromBody;
            target.CharShapeID = (uint)_docInfoAdder.ForCharShapeInfo().ProcessById((int)source.CharShapeID);
        }

        public bool EqualById(int sourceId, int targetId)
        {
            Bullet? source = null;
            Bullet? target = null;

            try
            {
                var list = _docInfoAdder.GetSourceHWPFile()?.DocInfo?.BulletList;
                if (list != null && sourceId - 1 >= 0 && sourceId - 1 < list.Count)
                {
                    source = list[sourceId - 1];
                }
            }
            catch { }

            try
            {
                var list = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.BulletList;
                if (list != null && targetId - 1 >= 0 && targetId - 1 < list.Count)
                {
                    target = list[targetId - 1];
                }
            }
            catch { }

            if (source == null && target == null)
            {
                return sourceId == targetId;
            }
            else if (source == null || target == null)
            {
                return false;
            }
            else
            {
                return Equal(source, target);
            }
        }
    }
}
