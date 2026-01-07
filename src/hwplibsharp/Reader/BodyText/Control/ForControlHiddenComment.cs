// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/ForControlHiddenComment.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.Etc;
using System;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// ���� ���� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public class ForControlHiddenComment
    {
        /// <summary>
        /// ���� ���� ��Ʈ��
        /// </summary>
        private ControlHiddenComment? _tcmt;

        /// <summary>
        /// ��Ʈ�� ����
        /// </summary>
        private CompoundStreamReader? _sr;

        /// <summary>
        /// ������
        /// </summary>
        public ForControlHiddenComment()
        {
        }

        /// <summary>
        /// ���� ���� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="tcmt">���� ���� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public void Read(ControlHiddenComment tcmt, CompoundStreamReader sr)
        {
            _tcmt = tcmt;
            _sr = sr;

            ListHeader();
            ParagraphList();
        }

        /// <summary>
        /// ���� ���� ��Ʈ���� ���� ����Ʈ ��� ���ڵ带 �д´�.
        /// </summary>
        private void ListHeader()
        {
            _sr!.ReadRecordHeader();
            if (_sr.CurrentRecordHeader?.TagId == HWPTag.ListHeader)
            {
                _tcmt!.ListHeader.ParaCount = _sr.ReadSInt4();
                _tcmt.ListHeader.Property.Value = _sr.ReadUInt4();
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
            ForParagraphList.Read(_tcmt!.ParagraphList, _sr!);
        }
    }

}