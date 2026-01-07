// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/ForControlHeader.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader.Header;
using HwpLib.Object.BodyText.Control.HeaderFooter;
using HwpLib.Object.Etc;
using System;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// �Ӹ��� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public class ForControlHeader
    {
        /// <summary>
        /// �Ӹ��� ��Ʈ��
        /// </summary>
        private ControlHeader? _head;

        /// <summary>
        /// ��Ʈ�� ����
        /// </summary>
        private CompoundStreamReader? _sr;

        /// <summary>
        /// ������
        /// </summary>
        public ForControlHeader()
        {
        }

        /// <summary>
        /// �Ӹ��� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="head">�Ӹ��� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public void Read(ControlHeader head, CompoundStreamReader sr)
        {
            _head = head;
            _sr = sr;

            CtrlHeader();
            ListHeader();
            ParagraphList();
        }

        /// <summary>
        /// �Ӹ��� ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        private void CtrlHeader()
        {
            _head!.Header.ApplyPage = HeaderFooterApplyPageExtensions.FromValue((byte)_sr!.ReadUInt4());

            if (_sr.IsEndOfRecord()) return;

            _head.Header.CreateIndex = _sr.ReadSInt4();
        }

        /// <summary>
        /// �Ӹ��� ��Ʈ���� ���� ����Ʈ ��� ���ڵ带 �д´�.
        /// </summary>
        private void ListHeader()
        {
            _sr!.ReadRecordHeader();
            if (_sr.CurrentRecordHeader?.TagId == HWPTag.ListHeader)
            {
                ListHeaderForHeaderFooter lh = _head!.ListHeader;
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
            ForParagraphList.Read(_head!.ParagraphList, _sr!);
        }
    }

}