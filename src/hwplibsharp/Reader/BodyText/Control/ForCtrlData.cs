using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Bookmark;


namespace HwpLib.Reader.BodyText.Control
{

    /// <summary>
    /// ���� ����Ÿ ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForCtrlData
    {
        /// <summary>
        /// ���� ������ ��ü�� �д´�.
        /// </summary>
        /// <param name="sr">��Ʈ�� ����</param>
        /// <returns>���� ���� ������ ��ü</returns>
        public static CtrlData Read(CompoundStreamReader sr)
        {
            var ctrlData = new CtrlData();
            ForParameterSet.Read(ctrlData.ParameterSet, sr);
            return ctrlData;
        }
    }

}