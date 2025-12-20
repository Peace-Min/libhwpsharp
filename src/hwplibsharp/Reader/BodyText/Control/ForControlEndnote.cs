using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader;
using HwpLib.Object.BodyText.Control.FootnoteEndnote;
using HwpLib.Object.BodyText.Control.SectionDefine;
using HwpLib.Object.Etc;
using System;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// ���� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public class ForControlEndnote
    {
        /// <summary>
        /// ���� ��Ʈ��
        /// </summary>
        private ControlEndnote? _en;

        /// <summary>
        /// ��Ʈ�� ����
        /// </summary>
        private CompoundStreamReader? _sr;

        /// <summary>
        /// ������
        /// </summary>
        public ForControlEndnote()
        {
        }

        /// <summary>
        /// ���� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="en">���� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public void Read(ControlEndnote en, CompoundStreamReader sr)
        {
            _en = en;
            _sr = sr;

            CtrlHeader();
            ListHeader();
            ParagraphList();
        }

        /// <summary>
        /// ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        private void CtrlHeader()
        {
            CtrlHeaderEndnote h = _en!.Header;
            h.Number = _sr!.ReadUInt4();
            h.BeforeDecorationLetter.Bytes = _sr.ReadWChar();
            h.AfterDecorationLetter.Bytes = _sr.ReadWChar();
            h.NumberShape = NumberShapeExtensions.FromValue((short)_sr.ReadUInt4());

            if (_sr.IsEndOfRecord()) return;

            h.InstanceId = _sr.ReadUInt4();
        }

        /// <summary>
        /// ���� ��Ʈ���� ���� ����Ʈ ��� ���ڵ带 �д´�.
        /// </summary>
        private void ListHeader()
        {
            _sr!.ReadRecordHeader();
            if (_sr.CurrentRecordHeader?.TagId == HWPTag.ListHeader)
            {
                ListHeaderForFootnoteEndnote lh = _en!.ListHeader;
                lh.ParaCount = _sr.ReadSInt4();
                lh.Property.Value = _sr.ReadUInt4();
                _sr.SkipToEndRecord();
            }
            else
            {
                throw new InvalidOperationException("List header must be located.");
            }
        }

        /// <summary>
        /// ���� ����Ʈ�� �д´�.
        /// </summary>
        private void ParagraphList()
        {
            ForParagraphList.Read(_en!.ParagraphList, _sr!);
        }
    }

}