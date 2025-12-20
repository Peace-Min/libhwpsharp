using HwpLib.Object.DocInfo;
using HwpLib.Object.DocInfo.FaceName;
using System.Collections.Generic;
using System.Linq;

namespace HwpLib.Tool.ParagraphAdder.DocInfo
{
    /// <summary>
    /// DocInfo에 FaceNameInfo을 복사하는 기능을 포함하는 클래스
    /// </summary>
    public class FaceNameInfoAdder
    {
        private DocInfoAdder _docInfoAdder;

        public FaceNameInfoAdder(DocInfoAdder docInfoAdder)
        {
            _docInfoAdder = docInfoAdder;
        }

        public int ProcessByHangulId(int sourceId)
        {
            if (_docInfoAdder.GetSourceHWPFile() == _docInfoAdder.GetTargetHWPFile())
            {
                return sourceId;
            }

            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.HangulFaceNameList?.ToList(), sourceId);
            var targetArray = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.HangulFaceNameList?.ToList();
            return Process(source, targetArray);
        }

        private FaceNameInfo? GetFaceNameInfo(List<FaceNameInfo>? faceNameList, int index)
        {
            if (faceNameList == null) return null;

            int count = faceNameList.Count;
            if (count == 0) return null;
            if (index >= count)
            {
                return faceNameList[count - 1];
            }
            else if (index < 0)
            {
                return faceNameList[0];
            }
            else
            {
                return faceNameList[index];
            }
        }

        private int Process(FaceNameInfo? source, List<FaceNameInfo>? targetArray)
        {
            if (source == null || targetArray == null) return 0;

            int index = Find(source, targetArray);
            if (index == -1)
            {
                index = AddAndCopy(source, targetArray);
            }
            return index;
        }

        private int Find(FaceNameInfo source, List<FaceNameInfo> targetArray)
        {
            int count = targetArray.Count;
            for (int index = 0; index < count; index++)
            {
                var target = targetArray[index];
                if (Equal(source, target))
                {
                    return index;
                }
            }
            return -1;
        }

        private bool Equal(FaceNameInfo? source, FaceNameInfo? target)
        {
            if (source == null || target == null) return source == target;

            return source.Property?.Value == target.Property?.Value
                && source.Name == target.Name
                && source.SubstituteFontType == target.SubstituteFontType
                && EqualNullableString(source.SubstituteFontName, target.SubstituteFontName)
                && EqualFontTypeInfo(source.FontTypeInfo, target.FontTypeInfo)
                && source.BaseFontName == target.BaseFontName;
        }

        private bool EqualNullableString(string? source, string? target)
        {
            if (source == null && target == null)
            {
                return true;
            }
            return source?.Equals(target) == true;
        }

        private bool EqualFontTypeInfo(FontTypeInfo? source, FontTypeInfo? target)
        {
            if (source == null || target == null) return source == target;

            return source.FontType == target.FontType
                && source.SerifType == target.SerifType
                && source.Thickness == target.Thickness
                && source.Ratio == target.Ratio
                && source.Contrast == target.Contrast
                && source.StrokeDeviation == target.StrokeDeviation
                && source.CharacterStrokeType == target.CharacterStrokeType
                && source.CharacterShape == target.CharacterShape
                && source.MiddleLine == target.MiddleLine
                && source.XHeight == target.XHeight;
        }

        private int AddAndCopy(FaceNameInfo source, List<FaceNameInfo> targetArray)
        {
            var target = new HwpLib.Object.DocInfo.FaceNameInfo();
            targetArray.Add(target);

            if (source.Property != null && target.Property != null)
                target.Property.Value = source.Property.Value;
            target.Name = source.Name;
            target.SubstituteFontType = source.SubstituteFontType;
            target.SubstituteFontName = source.SubstituteFontName;
            CopyFontTypeInfo(source.FontTypeInfo, target.FontTypeInfo);
            target.BaseFontName = source.BaseFontName;

            return targetArray.Count - 1;
        }

        private void CopyFontTypeInfo(FontTypeInfo? source, FontTypeInfo? target)
        {
            if (source == null || target == null) return;

            target.FontType = source.FontType;
            target.SerifType = source.SerifType;
            target.Thickness = source.Thickness;
            target.Ratio = source.Ratio;
            target.Contrast = source.Contrast;
            target.StrokeDeviation = source.StrokeDeviation;
            target.CharacterStrokeType = source.CharacterStrokeType;
            target.CharacterShape = source.CharacterShape;
            target.MiddleLine = source.MiddleLine;
            target.XHeight = source.XHeight;
        }

        public int ProcessByLatinId(int sourceId)
        {
            if (_docInfoAdder.GetSourceHWPFile() == _docInfoAdder.GetTargetHWPFile())
            {
                return sourceId;
            }

            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.EnglishFaceNameList?.ToList(), sourceId);
            var targetArray = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.EnglishFaceNameList?.ToList();
            return Process(source, targetArray);
        }

        public int ProcessByHanjaId(int sourceId)
        {
            if (_docInfoAdder.GetSourceHWPFile() == _docInfoAdder.GetTargetHWPFile())
            {
                return sourceId;
            }

            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.HanjaFaceNameList?.ToList(), sourceId);
            var targetArray = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.HanjaFaceNameList?.ToList();
            return Process(source, targetArray);
        }

        public int ProcessByJapaneseId(int sourceId)
        {
            if (_docInfoAdder.GetSourceHWPFile() == _docInfoAdder.GetTargetHWPFile())
            {
                return sourceId;
            }

            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.JapaneseFaceNameList?.ToList(), sourceId);
            var targetArray = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.JapaneseFaceNameList?.ToList();
            return Process(source, targetArray);
        }

        public int ProcessByOtherId(int sourceId)
        {
            if (_docInfoAdder.GetSourceHWPFile() == _docInfoAdder.GetTargetHWPFile())
            {
                return sourceId;
            }

            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.EtcFaceNameList?.ToList(), sourceId);
            var targetArray = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.EtcFaceNameList?.ToList();
            return Process(source, targetArray);
        }

        public int ProcessBySymbolId(int sourceId)
        {
            if (_docInfoAdder.GetSourceHWPFile() == _docInfoAdder.GetTargetHWPFile())
            {
                return sourceId;
            }

            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.SymbolFaceNameList?.ToList(), sourceId);
            var targetArray = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.SymbolFaceNameList?.ToList();
            return Process(source, targetArray);
        }

        public int ProcessByUserId(int sourceId)
        {
            if (_docInfoAdder.GetSourceHWPFile() == _docInfoAdder.GetTargetHWPFile())
            {
                return sourceId;
            }

            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.UserFaceNameList?.ToList(), sourceId);
            var targetArray = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.UserFaceNameList?.ToList();
            return Process(source, targetArray);
        }

        public bool EqualByHangulId(int sourceId, int targetId)
        {
            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.HangulFaceNameList?.ToList(), sourceId);
            var target = GetFaceNameInfo(_docInfoAdder.GetTargetHWPFile()?.DocInfo?.HangulFaceNameList?.ToList(), targetId);
            return Equal(source, target);
        }

        public bool EqualByLatinId(int sourceId, int targetId)
        {
            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.EnglishFaceNameList?.ToList(), sourceId);
            var target = GetFaceNameInfo(_docInfoAdder.GetTargetHWPFile()?.DocInfo?.EnglishFaceNameList?.ToList(), targetId);
            return Equal(source, target);
        }

        public bool EqualByHanjaId(int sourceId, int targetId)
        {
            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.HanjaFaceNameList?.ToList(), sourceId);
            var target = GetFaceNameInfo(_docInfoAdder.GetTargetHWPFile()?.DocInfo?.HanjaFaceNameList?.ToList(), targetId);
            return Equal(source, target);
        }

        public bool EqualByJapaneseId(int sourceId, int targetId)
        {
            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.JapaneseFaceNameList?.ToList(), sourceId);
            var target = GetFaceNameInfo(_docInfoAdder.GetTargetHWPFile()?.DocInfo?.JapaneseFaceNameList?.ToList(), targetId);
            return Equal(source, target);
        }

        public bool EqualByOtherId(int sourceId, int targetId)
        {
            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.EtcFaceNameList?.ToList(), sourceId);
            var target = GetFaceNameInfo(_docInfoAdder.GetTargetHWPFile()?.DocInfo?.EtcFaceNameList?.ToList(), targetId);
            return Equal(source, target);
        }

        public bool EqualBySymbolId(int sourceId, int targetId)
        {
            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.SymbolFaceNameList?.ToList(), sourceId);
            var target = GetFaceNameInfo(_docInfoAdder.GetTargetHWPFile()?.DocInfo?.SymbolFaceNameList?.ToList(), targetId);
            return Equal(source, target);
        }

        public bool EqualByUserId(int sourceId, int targetId)
        {
            var source = GetFaceNameInfo(_docInfoAdder.GetSourceHWPFile()?.DocInfo?.UserFaceNameList?.ToList(), sourceId);
            var target = GetFaceNameInfo(_docInfoAdder.GetTargetHWPFile()?.DocInfo?.UserFaceNameList?.ToList(), targetId);
            return Equal(source, target);
        }
    }
}
