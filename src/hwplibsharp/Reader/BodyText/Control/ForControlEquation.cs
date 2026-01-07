// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/ForControlEquation.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.Etc;
using HwpLib.Reader.BodyText.Control.Eqed;
using HwpLib.Reader.BodyText.Control.Gso.Part;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// ���� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public class ForControlEquation
    {
        /// <summary>
        /// ���� ��Ʈ��
        /// </summary>
        private ControlEquation? _eqed;

        /// <summary>
        /// ��Ʈ�� ����
        /// </summary>
        private CompoundStreamReader? _sr;

        /// <summary>
        /// ��Ʈ�� ��� ���ڵ��� ����
        /// </summary>
        private int _ctrlHeaderLevel;

        /// <summary>
        /// ������
        /// </summary>
        public ForControlEquation()
        {
        }

        /// <summary>
        /// ���� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="eqed">���� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public void Read(ControlEquation eqed, CompoundStreamReader sr)
        {
            _eqed = eqed;
            _sr = sr;
            _ctrlHeaderLevel = sr.CurrentRecordHeader!.Level;

            CtrlHeader();
            Caption();

            while (!sr.IsEndOfStream())
            {
                if (!sr.IsImmediatelyAfterReadingHeader)
                {
                    sr.ReadRecordHeader();
                }

                if (_ctrlHeaderLevel >= sr.CurrentRecordHeader!.Level)
                {
                    break;
                }
                ReadBody();
            }
        }

        /// <summary>
        /// ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        private void CtrlHeader()
        {
            ForCtrlHeaderGso.Read(_eqed!.GetHeader()!, _sr!);
        }

        /// <summary>
        /// ĸ�� ������ �д´�.
        /// </summary>
        private void Caption()
        {
            _sr!.ReadRecordHeader();
            if (_sr.CurrentRecordHeader?.TagId != HWPTag.ListHeader) return;

            _eqed!.CreateCaption();
            ForCaption.Read(_eqed.Caption!, _sr);
        }

        /// <summary>
        /// �̹� ���� ���ڵ� ����� ���� ���ڵ� ������ �д´�.
        /// </summary>
        private void ReadBody()
        {
            if (_sr!.CurrentRecordHeader?.TagId == HWPTag.EqEdit)
            {
                EqEdit();
            }
            else
            {
                _sr.SkipToEndRecord();
            }
        }

        /// <summary>
        /// ���� ���� ���ڵ带 �д´�.
        /// </summary>
        private void EqEdit()
        {
            ForEQEdit.Read(_eqed!.EQEdit, _sr!);
        }
    }

}