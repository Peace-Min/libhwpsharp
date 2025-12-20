using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.CtrlHeader;
using HwpLib.Util.Binary;


namespace HwpLib.Reader.BodyText.Control.Gso.Part
{

    /// <summary>
    /// �׸��� ��ü�� ��Ʈ�� ��� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForCtrlHeaderGso
    {
        /// <summary>
        /// �׸��� ��ü�� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="header">�׸��� ��ü�� ��Ʈ�� ��� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(CtrlHeaderGso header, CompoundStreamReader sr)
        {
            header.Property.Value = sr.ReadUInt4();
            header.YOffset = sr.ReadUInt4();
            header.XOffset = sr.ReadUInt4();
            header.Width = sr.ReadUInt4();
            header.Height = sr.ReadUInt4();
            header.ZOrder = sr.ReadSInt4();
            header.OutterMarginLeft = sr.ReadUInt2();
            header.OutterMarginRight = sr.ReadUInt2();
            header.OutterMarginTop = sr.ReadUInt2();
            header.OutterMarginBottom = sr.ReadUInt2();
            header.InstanceId = sr.ReadUInt4();

            if (sr.IsEndOfRecord()) return;

            int temp = sr.ReadSInt4();
            header.PreventPageDivide = BitFlag.Get(temp, 0);

            if (sr.IsEndOfRecord()) return;

            header.Explanation.Bytes = sr.ReadHWPString();

            if (sr.IsEndOfRecord()) return;

            int length = (int)(sr.CurrentRecordHeader!.Size - (sr.CurrentPosition - sr.CurrentPositionAfterHeader));
            if (length > 0)
            {
                header.Unknown = sr.ReadBytes(length);
            }
        }
    }

}