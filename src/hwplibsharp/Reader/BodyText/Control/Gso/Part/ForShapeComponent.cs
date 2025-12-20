using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponent;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponent.LineInfo;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponent.RenderingInfo;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponent.ShadowInfo;
using HwpLib.Reader.DocInfo.BorderFill;


namespace HwpLib.Reader.BodyText.Control.Gso.Part
{

    /// <summary>
    /// �׸��� ��ü�� ��ü ���� �Ӽ� ���ڵ带 �б� ���� ��ü
    /// </summary>
    public static class ForShapeComponent
    {
        /// <summary>
        /// �׸��� ��ü�� ��ü ���� �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="gsoControl">�׸��� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(GsoControl gsoControl, CompoundStreamReader sr)
        {
            if (gsoControl.GsoType == GsoControlType.Container)
            {
                ShapeComponentForContainer((ShapeComponentContainer)gsoControl.ShapeComponent, sr);
            }
            else
            {
                ShapeComponentForNormal((ShapeComponentNormal)gsoControl.ShapeComponent, sr);
            }
        }

        /// <summary>
        /// �Ϲ� ��Ʈ���� ���� ��ü ���� �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="scn">��ü ���� �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentForNormal(ShapeComponentNormal scn, CompoundStreamReader sr)
        {
            CommonPart(scn, sr);

            if (sr.IsEndOfRecord()) return;

            LineInfo(scn, sr);

            if (sr.IsEndOfRecord()) return;

            FillInfo(scn, sr);

            if (sr.IsEndOfRecord()) return;

            ShadowInfo(scn, sr);

            if (sr.IsEndOfRecord()) return;

            scn.Instid = sr.ReadUInt4();
            sr.Skip(1);
            if (scn.ShadowInfo != null)
            {
                scn.ShadowInfo.Transparent = sr.ReadUInt1();
            }
            else
            {
                sr.Skip(1);
            }
        }

        /// <summary>
        /// ��ü ���� �Ӽ� ���ڵ��� ���� �κ��� �д´�.
        /// </summary>
        /// <param name="sc">��ü ���� �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void CommonPart(ShapeComponent sc, CompoundStreamReader sr)
        {
            sc.OffsetX = sr.ReadSInt4();
            sc.OffsetY = sr.ReadSInt4();
            sc.GroupingCount = sr.ReadUInt2();
            sc.LocalFileVersion = sr.ReadUInt2();
            sc.WidthAtCreate = sr.ReadSInt4();
            sc.HeightAtCreate = sr.ReadSInt4();
            sc.WidthAtCurrent = sr.ReadSInt4();
            sc.HeightAtCurrent = sr.ReadSInt4();
            sc.Property.Value = sr.ReadUInt4();
            sc.RotateAngle = sr.ReadUInt2();
            sc.RotateXCenter = sr.ReadSInt4();
            sc.RotateYCenter = sr.ReadSInt4();

            RenderingInfo(sc.RenderingInfo, sr);
        }

        /// <summary>
        /// ��ü ���� �Ӽ� ���ڵ��� rendering ������ �д´�.
        /// </summary>
        /// <param name="ri">rendering ������ ��Ÿ���� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void RenderingInfo(RenderingInfo ri, CompoundStreamReader sr)
        {
            int scaleRotateMatrixCount = sr.ReadUInt2();
            Matrix(ri.TranslationMatrix, sr);
            for (int index = 0; index < scaleRotateMatrixCount; index++)
            {
                var srmp = ri.AddNewScaleRotateMatrixPair();
                Matrix(srmp.ScaleMatrix, sr);
                Matrix(srmp.RotateMatrix, sr);
            }
        }

        /// <summary>
        /// ��ȯ ����� �д´�.
        /// </summary>
        /// <param name="m">��ȯ ��� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void Matrix(Matrix m, CompoundStreamReader sr)
        {
            for (int index = 0; index < 6; index++)
            {
                m.SetValue(index, sr.ReadDouble());
            }
        }

        /// <summary>
        /// �Ϲ� ��Ʈ���� ���� ��ü ���� �Ӽ� ���ڵ��� line ������ �д´�.
        /// </summary>
        /// <param name="scn">�Ϲ� ��Ʈ���� ���� ��ü ���� �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void LineInfo(ShapeComponentNormal scn, CompoundStreamReader sr)
        {
            scn.CreateLineInfo();
            var li = scn.LineInfo!;
            li.Color.Value = sr.ReadUInt4();
            li.Thickness = sr.ReadSInt4();
            li.Property.Value = sr.ReadUInt4();
            li.OutlineStyle = OutlineStyleExtensions.FromValue((byte)sr.ReadUInt1());
        }

        /// <summary>
        /// �Ϲ� ��Ʈ���� ���� ��ü ���� �Ӽ� ���ڵ��� ��� ������ �д´�.
        /// </summary>
        /// <param name="scn">�Ϲ� ��Ʈ���� ���� ��ü ���� �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void FillInfo(ShapeComponentNormal scn, CompoundStreamReader sr)
        {
            scn.CreateFillInfo();
            ForFillInfo.Read(scn.FillInfo!, sr);
        }

        /// <summary>
        /// �Ϲ� ��Ʈ���� ���� ��ü ���� �Ӽ� ���ڵ��� �׸��� ������ �д´�.
        /// </summary>
        /// <param name="scn">�Ϲ� ��Ʈ���� ���� ��ü ���� �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShadowInfo(ShapeComponentNormal scn, CompoundStreamReader sr)
        {
            scn.CreateShadowInfo();
            var si = scn.ShadowInfo!;
            si.Type = ShadowTypeExtensions.FromValue((byte)sr.ReadUInt4());
            si.Color.Value = sr.ReadUInt4();
            si.OffsetX = sr.ReadSInt4();
            si.OffsetY = sr.ReadSInt4();
        }

        /// <summary>
        /// ���� ��Ʈ���� ���� ��ü ���� �Ӽ� ���ڵ带 �д´�.
        /// </summary>
        /// <param name="scc">��ü ���� �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShapeComponentForContainer(ShapeComponentContainer scc, CompoundStreamReader sr)
        {
            CommonPart(scc, sr);
            ChildInfo(scc, sr);

            if (sr.IsEndOfRecord()) return;

            scc.Instid = sr.ReadUInt4();
        }

        /// <summary>
        /// �����ϰ� �ִ� ��Ʈ�ѿ� ���� ���� �κ��� �д´�.
        /// </summary>
        /// <param name="scc">���� ��Ʈ���� ��ü ���� �Ӽ� ���ڵ�</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ChildInfo(ShapeComponentContainer scc, CompoundStreamReader sr)
        {
            int count = sr.ReadUInt2();
            for (int index = 0; index < count; index++)
            {
                uint childId = sr.ReadUInt4();
                scc.AddChildControlId(childId);
            }
        }
    }

}