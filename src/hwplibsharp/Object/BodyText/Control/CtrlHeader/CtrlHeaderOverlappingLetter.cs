using System.Collections.Generic;

namespace HwpLib.Object.BodyText.Control.CtrlHeader
{

    using HwpLib.Object.Etc;

    /// <summary>
    /// 글자 겹침 컨트롤을 위한 컨트롤 헤더 레코드
    /// </summary>
    public class CtrlHeaderOverlappingLetter : CtrlHeader
    {
        /// <summary>
        /// 겹침 글자 리스트
        /// </summary>
        private List<HWPString> overlappingLetterList;

        /// <summary>
        /// 테두리 내부 글자의 글자모양 id 리스트
        /// </summary>
        private List<uint> charShapeIdList;

        /// <summary>
        /// 생성자
        /// </summary>
        public CtrlHeaderOverlappingLetter()
            : base(ControlType.OverlappingLetter.GetCtrlId())
        {
            overlappingLetterList = new List<HWPString>();
            charShapeIdList = new List<uint>();
        }

        /// <summary>
        /// 겹쳐지는 글자를 리스트에 추가한다.
        /// </summary>
        /// <param name="overlappingLetter">겹쳐지는 글자</param>
        public void AddOverlappingLetter(HWPString overlappingLetter)
        {
            overlappingLetterList.Add(overlappingLetter);
        }

        /// <summary>
        /// 겹침 글자 리스트를 반환한다.
        /// </summary>
        public IReadOnlyList<HWPString> OverlappingLetterList => overlappingLetterList;

        /// <summary>
        /// 테두리 타입
        /// </summary>
        public short BorderType { get; set; }

        /// <summary>
        /// 내부 글자 크기
        /// </summary>
        public byte InternalFontSize { get; set; }

        /// <summary>
        /// 테두리 내부 글자 펼침
        /// </summary>
        public short ExpendInsideLetter { get; set; }

        /// <summary>
        /// 테두리 내부 글자의 글자모양 id를 리스트에 추가한다.
        /// </summary>
        /// <param name="charShapeId">테두리 내부 글자의 글자모양 id</param>
        public void AddCharShapeId(uint charShapeId)
        {
            charShapeIdList.Add(charShapeId);
        }

        /// <summary>
        /// 테두리 내부 글자의 글자모양 id 리스트를 반환한다.
        /// </summary>
        public IReadOnlyList<uint> CharShapeIdList => charShapeIdList;

        public override void Copy(CtrlHeader from)
        {
            CtrlHeaderOverlappingLetter from2 = (CtrlHeaderOverlappingLetter)from;

            overlappingLetterList.Clear();
            foreach (HWPString str in from2.overlappingLetterList)
            {
                overlappingLetterList.Add(str.Clone());
            }

            BorderType = from2.BorderType;
            InternalFontSize = from2.InternalFontSize;
            ExpendInsideLetter = from2.ExpendInsideLetter;

            charShapeIdList.Clear();
            foreach (uint id in from2.charShapeIdList)
            {
                charShapeIdList.Add(id);
            }
        }
    }

}