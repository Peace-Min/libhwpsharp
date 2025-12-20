using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach.Picture;
using HwpLib.Object.Etc;
using HwpLib.Reader.BodyText.Control.Gso.Part;
using HwpLib.Reader.DocInfo.BorderFill;


namespace HwpLib.Reader.BodyText.Control.Gso
{

    /// <summary>
    /// �׸� ��Ʈ���� ������ �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForControlPicture
    {
        /// <summary>
        /// �׸� ��Ʈ���� ������ �κ��� �д´�.
        /// </summary>
        /// <param name="picture">�׸� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadRest(ControlPicture picture, CompoundStreamReader sr)
        {
            sr.ReadRecordHeader();

            if (sr.CurrentRecordHeader?.TagId == HWPTag.CtrlData)
            {
                picture.CreateCtrlData();
                var ctrlData = ForCtrlData.Read(sr);
                picture.SetCtrlData(ctrlData);

                if (!sr.IsImmediatelyAfterReadingHeader)
                {
                    sr.ReadRecordHeader();
                }
            }

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ShapeComponentPicture)
            {
                ShapeComponentPicture(picture.ShapeComponentPicture, sr);
            }
        }

        /// <summary>
        /// �׸� ��ü �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="scp">�׸� ��ü �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentPicture(ShapeComponentPicture scp, CompoundStreamReader sr)
        {
            scp.BorderColor.Value = sr.ReadUInt4();
            scp.BorderThickness = sr.ReadSInt4();
            scp.BorderProperty.Value = sr.ReadUInt4();
            scp.LeftTop.X = (uint)sr.ReadSInt4();
            scp.LeftTop.Y = (uint)sr.ReadSInt4();
            scp.RightTop.X = (uint)sr.ReadSInt4();
            scp.RightTop.Y = (uint)sr.ReadSInt4();
            scp.RightBottom.X = (uint)sr.ReadSInt4();
            scp.RightBottom.Y = (uint)sr.ReadSInt4();
            scp.LeftBottom.X = (uint)sr.ReadSInt4();
            scp.LeftBottom.Y = (uint)sr.ReadSInt4();
            scp.LeftAfterCutting = sr.ReadSInt4();
            scp.TopAfterCutting = sr.ReadSInt4();
            scp.RightAfterCutting = sr.ReadSInt4();
            scp.BottomAfterCutting = sr.ReadSInt4();
            InnerMargin(scp.InnerMargin, sr);
            ForFillInfo.ReadPictureInfo(scp.PictureInfo, sr);

            if (sr.IsEndOfRecord()) return;

            scp.BorderTransparency = sr.ReadUInt1();

            if (sr.IsEndOfRecord()) return;

            scp.InstanceId = sr.ReadUInt4();

            if (sr.IsEndOfRecord()) return;

            ForPictureEffect.Read(scp.PictureEffect, sr);

            if (sr.IsEndOfRecord()) return;

            scp.ImageWidth = sr.ReadUInt4();
            scp.ImageHeight = sr.ReadUInt4();

            if (sr.IsEndOfRecord()) return;

            sr.SkipToEndRecord();
        }

        /// <summary>
        /// �׸� ��ü �Ӽ� ���ڵ��� ���� ���� �κ��� �д´�.
        /// </summary>
        /// <param name="im">���� ������ ��Ÿ���� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void InnerMargin(InnerMargin im, CompoundStreamReader sr)
        {
            im.Left = sr.ReadUInt2();
            im.Right = sr.ReadUInt2();
            im.Top = sr.ReadUInt2();
            im.Bottom = sr.ReadUInt2();
        }
    }

}