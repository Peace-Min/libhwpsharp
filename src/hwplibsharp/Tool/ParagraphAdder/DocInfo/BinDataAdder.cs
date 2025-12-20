using HwpLib.Object.DocInfo;

namespace HwpLib.Tool.ParagraphAdder.DocInfo
{
    /// <summary>
    /// DocInfo에 BinData를 복사하는 기능을 포함하는 클래스
    /// </summary>
    public class BinDataAdder
    {
        private DocInfoAdder _docInfoAdder;

        public BinDataAdder(DocInfoAdder docInfoAdder)
        {
            _docInfoAdder = docInfoAdder;
        }

        /// <summary>
        /// sourceID에 해당하는 BinData객체를 처리하여 target 한글 파일의 BinData ID를 반환한다.
        /// </summary>
        /// <param name="sourceId">source BinData ID</param>
        /// <returns>target 한글 파일의 BinData ID</returns>
        public int ProcessById(int sourceId)
        {
            if (_docInfoAdder.GetSourceHWPFile() == _docInfoAdder.GetTargetHWPFile())
            {
                return sourceId;
            }

            if (sourceId == 0)
            {
                return 0;
            }

            var binDataList = _docInfoAdder.GetSourceHWPFile()?.DocInfo?.BinDataList;
            if (binDataList == null || sourceId - 1 >= binDataList.Count || sourceId - 1 < 0)
            {
                return sourceId;
            }

            var source = binDataList[sourceId - 1];
            return AddAndCopy(source);
        }

        private int AddAndCopy(BinDataInfo source)
        {
            int binDataID = ForEmbeddedBinaryData.AddAndCopy(source.BinDataId, source.ExtensionForEmbedding, _docInfoAdder);
            if (binDataID > 0)
            {
                var target = _docInfoAdder.GetTargetHWPFile()?.DocInfo?.AddNewBinData();
                if (target != null)
                {
                    if (source.Property != null && target.Property != null)
                        target.Property.Value = source.Property.Value;
                    target.AbsolutePathForLink = source.AbsolutePathForLink;
                    target.RelativePathForLink = source.RelativePathForLink;
                    target.BinDataId = binDataID;
                    target.ExtensionForEmbedding = source.ExtensionForEmbedding;

                    return _docInfoAdder.GetTargetHWPFile()?.DocInfo?.BinDataList?.Count ?? -1;
                }
            }
            return -1;
        }
    }
}
