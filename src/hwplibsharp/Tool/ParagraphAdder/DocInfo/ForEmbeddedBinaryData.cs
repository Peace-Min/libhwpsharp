using HwpLib.Object.BinData;
using System;
using System.Collections.Generic;

namespace HwpLib.Tool.ParagraphAdder.DocInfo
{
    /// <summary>
    /// EmbeddedBinaryData를 복사하는 기능을 포함하는 클래스
    /// </summary>
    public static class ForEmbeddedBinaryData
    {
        public static int AddAndCopy(int sourceId, string? imageFileExt, DocInfoAdder docInfoAdder)
        {
            var source = FindByName(docInfoAdder.GetSourceHWPFile()?.BinData?.EmbeddedBinaryDataList, Name(sourceId, imageFileExt));
            if (source != null)
            {
                var target = docInfoAdder.GetTargetHWPFile()?.BinData?.AddNewEmbeddedBinaryData();
                if (target != null)
                {
                    var list = docInfoAdder.GetTargetHWPFile()?.BinData?.EmbeddedBinaryDataList;
                    int targetID = list?.Count ?? 0;
                    target.Name = Name(targetID, imageFileExt);
                    target.Data = source.Data;
                    target.CompressMethod = source.CompressMethod;
                    return list?.Count ?? -1;
                }
            }
            return -1;
        }

        private static EmbeddedBinaryData? FindByName(IReadOnlyList<EmbeddedBinaryData>? embeddedBinaryDataList, string name)
        {
            if (embeddedBinaryDataList == null) return null;

            foreach (var embeddedBinaryData in embeddedBinaryDataList)
            {
                if (string.Equals(embeddedBinaryData.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return embeddedBinaryData;
                }
            }
            return null;
        }

        private static string Name(int binDataID, string? imageFileExt)
        {
            return $"Bin{binDataID:X4}.{imageFileExt}";
        }
    }
}
