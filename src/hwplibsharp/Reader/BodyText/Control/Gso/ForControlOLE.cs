using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach;
using HwpLib.Object.Etc;


namespace HwpLib.Reader.BodyText.Control.Gso
{

    /// <summary>
    /// OLE ��Ʈ���� ������ �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForControlOLE
    {
        /// <summary>
        /// OLE ��Ʈ���� ������ �κ��� �д´�.
        /// </summary>
        /// <param name="ole">OLE ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadRest(ControlOLE ole, CompoundStreamReader sr)
        {
            sr.ReadRecordHeader();

            if (sr.CurrentRecordHeader?.TagId == HWPTag.ShapeComponentOle)
            {
                ShapeComponentOLE(ole.ShapeComponentOLE, sr);
            }
        }

        /// <summary>
        /// OLE ��ü �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="sco">OLE ��ü �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentOLE(ShapeComponentOLE sco, CompoundStreamReader sr)
        {
            sco.Property.Value = sr.ReadUInt4();
            sco.ExtentWidth = sr.ReadSInt4();
            sco.ExtentHeight = sr.ReadSInt4();
            sco.BinDataId = sr.ReadUInt2();
            sco.BorderColor.Value = sr.ReadUInt4();
            sco.BorderThickness = sr.ReadSInt4();
            sco.BorderProperty.Value = sr.ReadUInt4();
            UnknownData(sco, sr);
        }

        /// <summary>
        /// �� �� ���� ������ ����� �д´�.
        /// </summary>
        /// <param name="sco">OLE ��ü �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void UnknownData(ShapeComponentOLE sco, CompoundStreamReader sr)
        {
            int unknownSize = (int)(sr.CurrentRecordHeader!.Size - (sr.CurrentPosition - sr.CurrentPositionAfterHeader));
            if (unknownSize > 0)
            {
                byte[] unknown = sr.ReadBytes(unknownSize);
                sco.Unknown = unknown;
            }
        }
    }

}