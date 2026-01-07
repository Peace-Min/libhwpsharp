// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/ForControlField.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// �ʵ� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public static class ForControlField
    {
        /// <summary>
        /// �ʵ� ��Ʈ�� ����� �д´�.
        /// </summary>
        /// <param name="f">�ʵ� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadCtrlHeader(ControlField f, CompoundStreamReader sr)
        {
            var h = f.GetHeader();
            if (h == null) return;

            h.Property.Value = sr.ReadUInt4();
            h.EtcProperty = (short)sr.ReadUInt1();
            h.Command.Bytes = sr.ReadHWPString();
            h.InstanceId = sr.ReadUInt4();

            // �߰� 4����Ʈ �б� (�޸� �ε��� �Ǵ� ���� ����)
            if (!sr.IsEndOfRecord())
            {
                if (h.CtrlId == ControlType.FIELD_UNKNOWN.GetCtrlId())
                {
                    h.MemoIndex = sr.ReadSInt4();
                }
                else
                {
                    sr.Skip(4);
                }
            }

            // ���ڵ� ������ ���� ����Ʈ �ǳʶٱ�
            sr.SkipToEndRecord();
        }

        /// <summary>
        /// ��Ʈ�� ID�� �̹� ���� �� �ʵ� ��Ʈ�� ����� �д´�.
        /// </summary>
        /// <param name="f">�ʵ� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadAfterCtrlId(ControlField f, CompoundStreamReader sr)
        {
            var h = f.GetHeader();
            if (h == null) return;

            h.Property.Value = sr.ReadUInt4();
            h.EtcProperty = (short)sr.ReadUInt1();
            h.Command.Bytes = sr.ReadHWPString();
            h.InstanceId = sr.ReadUInt4();

            // �߰� 4����Ʈ �б� (�޸� �ε��� �Ǵ� ���� ����)
            if (!sr.IsEndOfRecord())
            {
                if (h.CtrlId == ControlType.FIELD_UNKNOWN.GetCtrlId())
                {
                    h.MemoIndex = sr.ReadSInt4();
                }
                else
                {
                    sr.Skip(4);
                }
            }

            // ���ڵ� ������ ���� ����Ʈ �ǳʶٱ�
            sr.SkipToEndRecord();
        }
    }

}