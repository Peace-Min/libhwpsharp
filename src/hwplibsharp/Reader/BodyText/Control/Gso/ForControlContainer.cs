using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponent;


namespace HwpLib.Reader.BodyText.Control.Gso
{

    /// <summary>
    /// ���� ��Ʈ���� ������ �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForControlContainer
    {
        /// <summary>
        /// ���� ��Ʈ���� ������ �κ��� �д´�.
        /// </summary>
        /// <param name="container">���� ��Ʈ��</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void ReadRest(ControlContainer container, CompoundStreamReader sr)
        {
            var scc = (ShapeComponentContainer)container.ShapeComponent;
            int childCount = scc.ChildControlIdList.Count;
            for (int index = 0; index < childCount; index++)
            {
                var fgc = new ForGsoControl();
                var gc = fgc.ReadInContainer(sr);
                container.AddChildControl(gc);
            }
        }
    }

}