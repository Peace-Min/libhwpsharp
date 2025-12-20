namespace HwpLib.Object.DocInfo.BorderFill
{

    using HwpLib.Object.Etc;

    /// <summary>
    /// 테두리/배경 객체에서 4방향의 각각의 선을 나타내는 객체
    /// </summary>
    public class EachBorder
    {
        /// <summary>
        /// 선 종류
        /// </summary>
        public BorderType Type { get; set; }

        /// <summary>
        /// 두께
        /// </summary>
        public BorderThickness Thickness { get; set; }

        /// <summary>
        /// 색상
        /// </summary>
        private Color4Byte color;

        /// <summary>
        /// 생성자
        /// </summary>
        public EachBorder()
        {
            color = new Color4Byte();
        }

        /// <summary>
        /// 선의 색상 객체를 반환한다.
        /// </summary>
        public Color4Byte Color => color;

        public void Copy(EachBorder from)
        {
            Type = from.Type;
            Thickness = from.Thickness;
            color.Copy(from.color);
        }
    }

}