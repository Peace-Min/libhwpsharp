// =====================================================================
// Java Original: kr/dogfoot/hwplib/reader/bodytext/ForControlTable.java
// Repository: https://github.com/neolord0/hwplib
// =====================================================================

using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.Table;
using HwpLib.Object.Etc;
using HwpLib.Reader.BodyText.Control.Gso.Part;
using HwpLib.Reader.BodyText.Control.Tbl;


namespace HwpLib.Reader.BodyText.Paragraph
{

    /// <summary>
    /// ǥ ��Ʈ���� �б� ���� ��ü
    /// </summary>
    public class ForControlTable
    {
        /// <summary>
        /// ǥ ��Ʈ��
        /// </summary>
        private ControlTable? _table;

        /// <summary>
        /// ��Ʈ�� ����
        /// </summary>
        private CompoundStreamReader? _sr;

        /// <summary>
        /// ������
        /// </summary>
        public ForControlTable()
        {
        }

        /// <summary>
        /// ǥ ��Ʈ���� �д´�.
        /// </summary>
        /// <param name="table">ǥ ��Ʈ�� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public void Read(ControlTable table, CompoundStreamReader sr)
        {
            _table = table;
            _sr = sr;

            CtrlHeader();
            CtrlData();
            Caption();
            Table();
            Rows();
        }

        /// <summary>
        /// ǥ ��Ʈ���� ��Ʈ�� ��� ���ڵ带 �д´�.
        /// </summary>
        private void CtrlHeader()
        {
            ForCtrlHeaderGso.Read(_table!.Header, _sr!);
        }

        /// <summary>
        /// ��Ʈ�� �����͸� �д´�.
        /// </summary>
        private void CtrlData()
        {
            _sr!.ReadRecordHeader();
            if (_sr.CurrentRecordHeader?.TagId == HWPTag.CtrlData)
            {
                _table!.CreateCtrlData();
                var ctrlData = Control.ForCtrlData.Read(_sr);
                _table.SetCtrlData(ctrlData);
            }
        }

        /// <summary>
        /// ĸ�� ������ �д´�.
        /// </summary>
        private void Caption()
        {
            if (!_sr!.IsImmediatelyAfterReadingHeader)
            {
                _sr.ReadRecordHeader();
            }
            if (_sr.CurrentRecordHeader?.TagId == HWPTag.ListHeader)
            {
                _table!.CreateCaption();
                ForCaption.Read(_table.Caption!, _sr);
            }
        }

        /// <summary>
        /// ǥ ���� ���ڵ带 �д´�.
        /// </summary>
        private void Table()
        {
            if (!_sr!.IsImmediatelyAfterReadingHeader)
            {
                _sr.ReadRecordHeader();
            }
            if (_sr.CurrentRecordHeader?.TagId == HWPTag.Table)
            {
                ForTable.Read(_table!.Table, _sr);
            }
        }

        /// <summary>
        /// ����� �д´�.
        /// </summary>
        private void Rows()
        {
            int rowCount = _table!.Table.RowCount;
            var cellCountOfRowList = _table.Table.CellCountOfRowList;
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var r = _table.AddNewRow();
                Row(r, cellCountOfRowList[rowIndex]);
            }
        }

        /// <summary>
        /// �ϳ��� �� �ȿ� ������ �д´�.
        /// </summary>
        /// <param name="r">��</param>
        /// <param name="cellCount">�࿡ ���Ե� �� ����</param>
        private void Row(Row r, int cellCount)
        {
            for (int cellIndex = 0; cellIndex < cellCount; cellIndex++)
            {
                var c = r.AddNewCell();
                ForCell.Read(c, _sr!);
            }
        }
    }

}