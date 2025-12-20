using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader.Header;
using HwpLib.Object.BodyText.Control.HeaderFooter;
using HwpLib.Object.Etc;
using System;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// ������ ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public class ForControlFooter
    {
        /// <summary>
        /// ������ ��Ʈ��
        /// </summary>
        private ControlFooter? _foot;

        /// <summary>
        /// ��Ʈ�� ����
        /// </summary>
        private CompoundStreamReader? _sr;

        /// <summary>
        /// ������
        /// </summary>
        public ForControlFooter()
        {
        }

        /// <summary>
        /// ������ ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="foot">������ ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public void Read(ControlFooter foot, CompoundStreamReader sr)
        {
            _foot = foot;
            _sr = sr;

            CtrlHeader();
            ListHeader();
            ParagraphList();
        }

        /// <summary>
        /// ������ ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        private void CtrlHeader()
        {
            _foot!.Header.ApplyPage = HeaderFooterApplyPageExtensions.FromValue((byte)_sr!.ReadUInt4());

            if (_sr.CurrentRecordHeader!.Size > (_sr.CurrentPosition - _sr.CurrentPositionAfterHeader))
            {
                _foot.Header.CreateIndex = _sr.ReadSInt4();
            }
        }

        /// <summary>
        /// ������ ��Ʈ���� ���� ����Ʈ ��� ���ڵ带 �д´�.
        /// </summary>
        private void ListHeader()
        {
            _sr!.ReadRecordHeader();
            if (_sr.CurrentRecordHeader?.TagId == HWPTag.ListHeader)
            {
                ListHeaderForHeaderFooter lh = _foot!.ListHeader;
                lh.ParaCount = _sr.ReadSInt4();
                lh.Property.Value = _sr.ReadUInt4();
                lh.TextWidth = _sr.ReadUInt4();
                lh.TextHeight = _sr.ReadUInt4();
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
            ForParagraphList.Read(_foot!.ParagraphList, _sr!);
        }
    }

}