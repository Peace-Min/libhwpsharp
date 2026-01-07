// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/gso/ForControlTextArt.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach.TextArt;
using HwpLib.Object.DocInfo.FaceName;
using HwpLib.Object.Etc;


namespace HwpLib.Reader.BodyText.Control.Gso
{

    /// <summary>
    /// �۸ʽ� ��Ʈ���� ������ �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForControlTextArt
    {
        /// <summary>
        /// �۸ʽ� ��Ʈ���� ������ �κ��� �д´�.
        /// </summary>
        /// <param name="textArt">�۸ʽ� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadRest(ControlTextArt textArt, CompoundStreamReader sr)
        {
            sr.ReadRecordHeader();

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ShapeComponentTextArt)
            {
                ShapeComponentTextArt(textArt.ShapeComponentTextArt, sr);
            }
        }

        /// <summary>
        /// �۸ʽ� ��ü �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="scta">�۸ʽ� ��ü �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentTextArt(ShapeComponentTextArt scta, CompoundStreamReader sr)
        {
            scta.X1 = sr.ReadSInt4();
            scta.Y1 = sr.ReadSInt4();
            scta.X2 = sr.ReadSInt4();
            scta.Y2 = sr.ReadSInt4();
            scta.X3 = sr.ReadSInt4();
            scta.Y3 = sr.ReadSInt4();
            scta.X4 = sr.ReadSInt4();
            scta.Y4 = sr.ReadSInt4();
            scta.Content.Bytes = sr.ReadHWPString();
            scta.FontName.Bytes = sr.ReadHWPString();
            scta.FontStyle.Bytes = sr.ReadHWPString();
            scta.FontType = FontTypeExtensions.FromValue((byte)sr.ReadSInt4());
            scta.TextArtShape = TextArtShapeExtensions.FromValue((byte)sr.ReadSInt4());
            scta.LineSpace = sr.ReadSInt4();
            scta.CharSpace = sr.ReadSInt4();
            scta.ParaAlignment = TextArtAlignExtensions.FromValue((byte)sr.ReadSInt4());
            scta.ShadowType = sr.ReadSInt4();
            scta.ShadowOffsetX = sr.ReadSInt4();
            scta.ShadowOffsetY = sr.ReadSInt4();
            scta.ShadowColor.Value = sr.ReadUInt4();

            int outlinePointCount = sr.ReadSInt4();
            for (int index = 0; index < outlinePointCount; index++)
            {
                var positionXY = scta.AddNewOutlinePoint();
                positionXY.X = (uint)sr.ReadSInt4();
                positionXY.Y = (uint)sr.ReadSInt4();
            }
        }
    }

}