using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.Etc;
using HwpLib.Reader.BodyText.Control.Secd;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// ���� ���� ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public class ForControlSectionDefine
    {
        /// <summary>
        /// ���� ���� ��Ʈ��
        /// </summary>
        private ControlSectionDefine? _secd;

        /// <summary>
        /// ��Ʈ�� ����
        /// </summary>
        private CompoundStreamReader? _sr;

        /// <summary>
        /// ��Ʈ������� ����
        /// </summary>
        private short _ctrlHeaderLevel;

        /// <summary>
        /// ��/���ָ�� ���ڵ� �ε���
        /// </summary>
        private int _endFootnoteShapeIndex;

        /// <summary>
        /// �� �׵θ�/��� ���ڵ� �ε���
        /// </summary>
        private int _pageBorderFillIndex;

        /// <summary>
        /// ������
        /// </summary>
        public ForControlSectionDefine()
        {
            _endFootnoteShapeIndex = 0;
            _pageBorderFillIndex = 0;
        }

        /// <summary>
        /// ���� ���� ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="secd">���� ���� ��Ʈ�� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public void Read(ControlSectionDefine secd, CompoundStreamReader sr)
        {
            _secd = secd;
            _sr = sr;
            _ctrlHeaderLevel = (short)sr.CurrentRecordHeader!.Level;

            CtrlHeader();

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
        /// ���� ���� ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        private void CtrlHeader()
        {
            ForCtrlHeaderSecd.Read(_secd!.Header, _sr!);
        }

        /// <summary>
        /// �̹� ���� ���ڵ� ����� ���� ���ڵ� ������ �д´�.
        /// </summary>
        private void ReadBody()
        {
            var tagId = _sr!.CurrentRecordHeader!.TagId;

            if (tagId == HWPTag.PageDef)
            {
                PageDef();
            }
            else if (tagId == HWPTag.FootnoteShape)
            {
                EndFootnoteShapes();
            }
            else if (tagId == HWPTag.PageBorderFill)
            {
                PageBorderFills();
            }
            else if (tagId == HWPTag.ListHeader)
            {
                BatangPageInfo();
            }
            else if (tagId == HWPTag.CtrlData)
            {
                CtrlData();
            }
            else
            {
                _sr.SkipToEndRecord();
            }
        }

        /// <summary>
        /// ���� ���� ���ڵ带 �д´�.
        /// </summary>
        private void PageDef()
        {
            ForPageDef.Read(_secd!.PageDef, _sr!);
        }

        /// <summary>
        /// ����/���� ��� ���ڵ带 �д´�.
        /// </summary>
        private void EndFootnoteShapes()
        {
            if (_endFootnoteShapeIndex == 0)
            {
                FootNoteShape();
            }
            else if (_endFootnoteShapeIndex == 1)
            {
                EndNoteShape();
            }
            _endFootnoteShapeIndex++;
        }

        /// <summary>
        /// ���� ��� ���ڵ带 �д´�.
        /// </summary>
        private void FootNoteShape()
        {
            ForFootEndNoteShape.Read(_secd!.FootNoteShape, _sr!);
        }

        /// <summary>
        /// ���� ��� ���ڵ带 �д´�.
        /// </summary>
        private void EndNoteShape()
        {
            ForFootEndNoteShape.Read(_secd!.EndNoteShape, _sr!);
        }

        /// <summary>
        /// �� �׵θ�/��� ���ڵ带 �д´�.
        /// </summary>
        private void PageBorderFills()
        {
            if (_pageBorderFillIndex == 0)
            {
                BothPageBorderFill();
            }
            else if (_pageBorderFillIndex == 1)
            {
                EvenPageBorderFill();
            }
            else if (_pageBorderFillIndex == 2)
            {
                OddPageBorderFill();
            }
            _pageBorderFillIndex++;
        }

        /// <summary>
        /// ���� �������� ���� �� �׵θ�/��� ���ڵ带 �д´�.
        /// </summary>
        private void BothPageBorderFill()
        {
            ForPageBorderFill.Read(_secd!.BothPageBorderFill, _sr!);
        }

        /// <summary>
        /// ¦���� �������� ���� �� �׵θ�/��� ���ڵ带 �д´�.
        /// </summary>
        private void EvenPageBorderFill()
        {
            ForPageBorderFill.Read(_secd!.EvenPageBorderFill, _sr!);
        }

        /// <summary>
        /// Ȧ���� �������� ���� �� �׵θ�/��� ���ڵ带 �д´�.
        /// </summary>
        private void OddPageBorderFill()
        {
            ForPageBorderFill.Read(_secd!.OddPageBorderFill, _sr!);
        }

        /// <summary>
        /// ������ ������ �д´�.
        /// </summary>
        private void BatangPageInfo()
        {
            ForBatangPageInfo.Read(_secd!.AddNewBatangPageInfo(), _sr!);
        }

        /// <summary>
        /// ��Ʈ�� �����͸� �д´�.
        /// </summary>
        private void CtrlData()
        {
            _secd!.CreateCtrlData();
            var ctrlData = ForCtrlData.Read(_sr!);
            _secd.SetCtrlData(ctrlData);
        }
    }

}