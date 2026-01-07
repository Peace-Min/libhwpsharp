// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/ForParaText.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;


namespace HwpLib.Reader.BodyText.Paragraph
{

    /// <summary>
    /// ���� �ؽ�Ʈ ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForParaText
    {
        /// <summary>
        /// ���� �ؽ�Ʈ ���ڵ带 �д´�.
        /// </summary>
        /// <param name="p">����</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(Object.BodyText.Paragraph.Paragraph p, CompoundStreamReader sr)
        {
            if (p.Text == null)
            {
                p.CreateText();
            }

            var pt = p.Text!;

            // ���ڵ� ũ�� / 2 = ���� �� (UTF-16LE)
            long charCount = sr.CurrentRecordHeader!.Size / 2;

            for (long i = 0; i < charCount; i++)
            {
                ushort code = sr.ReadUInt2();

                if (code <= 0x0020)
                {
                    // ���� ����
                    switch (code)
                    {
                        case 0x0000: // ��� �� ��
                            break;
                        case 0x0001: // ���� (���� ��Ʈ��)
                        case 0x0002: // ����/�� ���� (Ȯ�� ��Ʈ��)
                        case 0x0003: // �ʵ� ���� (Ȯ�� ��Ʈ��)
                            {
                                var extendChar = pt.AddNewExtendControlChar();
                                extendChar.Code = (short)code;
                                byte[] addition = sr.ReadBytes(12);
                                extendChar.SetAddition(addition);
                                sr.ReadUInt2(); // ���� �ڵ� �б�
                                i += 7; // �߰� 12����Ʈ = 6 ���� + 1 ���� (�ڵ� ��ü) - �̹� 1 ������
                            }
                            break;
                        case 0x0004: // �ʵ� �� (�ζ��� ��Ʈ��)
                            {
                                var inlineChar = pt.AddNewInlineControlChar();
                                inlineChar.Code = (short)code;
                                byte[] addition = sr.ReadBytes(12);
                                inlineChar.SetAddition(addition);
                                sr.ReadUInt2(); // ���� �ڵ� �б�
                                i += 7;
                            }
                            break;
                        case 0x0005: // ����
                        case 0x0006: // ����
                        case 0x0007: // ����
                        case 0x0008: // ���� ���� (���� ��Ʈ��)
                        case 0x0009: // �� (���� ��Ʈ��)
                        case 0x000A: // �� �ٲ� (���� ��Ʈ��)
                            {
                                var controlChar = pt.AddNewCharControlChar();
                                controlChar.Code = (short)code;
                            }
                            break;
                        case 0x000B: // �׸��� ��ü/ǥ (Ȯ�� ��Ʈ��)
                            {
                                var extendChar = pt.AddNewExtendControlChar();
                                extendChar.Code = (short)code;
                                byte[] addition = sr.ReadBytes(12);
                                extendChar.SetAddition(addition);
                                sr.ReadUInt2(); // ���� �ڵ� �б�
                                i += 7;
                            }
                            break;
                        case 0x000C: // ����
                            break;
                        case 0x000D: // ���� �� (���� ��Ʈ��)
                            {
                                var normalChar = pt.AddNewNormalChar();
                                normalChar.Code = (short)code;
                            }
                            break;
                        case 0x000E: // ����
                        case 0x000F: // ������ (���� ��Ʈ��)
                            {
                                var controlChar = pt.AddNewCharControlChar();
                                controlChar.Code = (short)code;
                            }
                            break;
                        case 0x0010: // �Ӹ���/������/����/���� �� (Ȯ�� ��Ʈ��)
                        case 0x0011: // �ڵ� ��ȣ (Ȯ�� ��Ʈ��)
                            {
                                var extendChar = pt.AddNewExtendControlChar();
                                extendChar.Code = (short)code;
                                byte[] addition = sr.ReadBytes(12);
                                extendChar.SetAddition(addition);
                                sr.ReadUInt2(); // ���� �ڵ� �б�
                                i += 7;
                            }
                            break;
                        case 0x0012: // ����
                        case 0x0013: // ����
                        case 0x0014: // ����
                        case 0x0015: // ���� ���� (Ȯ�� ��Ʈ��)
                            {
                                var extendChar = pt.AddNewExtendControlChar();
                                extendChar.Code = (short)code;
                                byte[] addition = sr.ReadBytes(12);
                                extendChar.SetAddition(addition);
                                sr.ReadUInt2(); // ���� �ڵ� �б�
                                i += 7;
                            }
                            break;
                        case 0x0016: // ����
                        case 0x0017: // ����
                        case 0x0018: // ���� ��ħ (�ζ��� ��Ʈ��)
                            {
                                var inlineChar = pt.AddNewInlineControlChar();
                                inlineChar.Code = (short)code;
                                byte[] addition = sr.ReadBytes(12);
                                inlineChar.SetAddition(addition);
                                sr.ReadUInt2(); // ���� �ڵ� �б�
                                i += 7;
                            }
                            break;
                        case 0x0019: // ����
                        case 0x001A: // ����
                        case 0x001B: // ����
                        case 0x001C: // ����
                        case 0x001D: // ����
                        case 0x001E: // ����/���� ���� ��ġ (���� ��Ʈ��)
                        case 0x001F: // ������ (���� ��Ʈ��)
                        case 0x0020: // ���� (�Ϲ� ����)
                            {
                                var normalChar = pt.AddNewNormalChar();
                                normalChar.Code = (short)code;
                            }
                            break;
                        default:
                            {
                                var normalChar = pt.AddNewNormalChar();
                                normalChar.Code = (short)code;
                            }
                            break;
                    }
                }
                else
                {
                    // �Ϲ� ����
                    var normalChar = pt.AddNewNormalChar();
                    normalChar.Code = (short)code;
                }
            }
        }
    }

}