using HwpLib.CompoundFile;
using HwpLib.Object.BodyText.Control.Gso.ShapeComponentEach.Picture;
using System;


namespace HwpLib.Reader.BodyText.Control.Gso.Part
{

    /// <summary>
    /// �׸� ��ü �Ӽ� ���ڵ��� �׸� ȿ�� �κ��� �б� ���� ��ü
    /// </summary>
    public static class ForPictureEffect
    {
        /// <summary>
        /// �׸� ��ü �Ӽ� ���ڵ��� �׸� ȿ�� �κ��� �д´�.
        /// </summary>
        /// <param name="pe">�׸� ��ü �Ӽ� ���ڵ��� �׸� ȿ���� ��Ÿ���� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        public static void Read(PictureEffect pe, CompoundStreamReader sr)
        {
            pe.Property.Value = sr.ReadUInt4();
            if (pe.Property.HasShadowEffect)
            {
                pe.CreateShadowEffect();
                ShadowEffect(pe.ShadowEffect!, sr);
            }
            if (pe.Property.HasNeonEffect)
            {
                pe.CreateNeonEffect();
                NeonEffect(pe.NeonEffect!, sr);
            }
            if (pe.Property.HasSoftBorderEffect)
            {
                pe.CreateSoftEdgeEffect();
                SoftEdgeEffect(pe.SoftEdgeEffect!, sr);
            }
            if (pe.Property.HasReflectionEffect)
            {
                pe.CreateReflectionEffect();
                ReflectionEffect(pe.ReflectionEffect!, sr);
            }
        }

        /// <summary>
        /// �׸��� ȿ�� �κ��� �д´�.
        /// </summary>
        /// <param name="se">�׸��� ȿ�� �κ��� ��Ÿ���� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ShadowEffect(ShadowEffect se, CompoundStreamReader sr)
        {
            se.Style = sr.ReadSInt4();
            se.Transparency = sr.ReadFloat();
            se.Cloudy = sr.ReadFloat();
            se.Direction = sr.ReadFloat();
            se.Distance = sr.ReadFloat();
            se.Sort = sr.ReadSInt4();
            se.TiltAngleX = sr.ReadFloat();
            se.TiltAngleY = sr.ReadFloat();
            se.ZoomRateX = sr.ReadFloat();
            se.ZoomRateY = sr.ReadFloat();
            se.RotateWithShape = sr.ReadSInt4();

            ColorProperty(se.Color, sr);
        }

        /// <summary>
        /// ���� �Ӽ� �κ��� �д´�.
        /// </summary>
        /// <param name="cp">���� �Ӽ��� ��Ÿ���� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ColorProperty(ColorWithEffect cp, CompoundStreamReader sr)
        {
            cp.Type = sr.ReadSInt4();
            if (cp.Type == 0)
            {
                byte[] color = sr.ReadBytes(4);
                cp.Color = color;
            }
            else
            {
                throw new InvalidOperationException("not supported color type !!!");
            }
            int colorEffectCount = (int)sr.ReadUInt4();
            for (int index = 0; index < colorEffectCount; index++)
            {
                var ce = cp.AddNewColorEffect();
                ce.Sort = ColorEffectSortExtensions.FromValue(sr.ReadSInt4());
                ce.Value = sr.ReadFloat();
            }
        }

        /// <summary>
        /// �׿� ȿ�� �κ��� �д´�.
        /// </summary>
        /// <param name="ne">�׿� ȿ���� ��Ÿ���� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void NeonEffect(NeonEffect ne, CompoundStreamReader sr)
        {
            ne.Transparency = sr.ReadFloat();
            ne.Radius = sr.ReadFloat();
            ColorProperty(ne.Color, sr);
        }

        /// <summary>
        /// �ε巯�� �����ڸ� ȿ�� �κ��� �д´�.
        /// </summary>
        /// <param name="see">�ε巯�� �����ڸ� ȿ���� ��Ÿ���� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void SoftEdgeEffect(SoftEdgeEffect see, CompoundStreamReader sr)
        {
            see.Radius = sr.ReadFloat();
        }

        /// <summary>
        /// �ݻ� ȿ�� �κ��� �д´�.
        /// </summary>
        /// <param name="re">�ݻ� ȿ���� ��Ÿ���� ��ü</param>
        /// <param name="sr">��Ʈ�� ����</param>
        private static void ReflectionEffect(ReflectionEffect re, CompoundStreamReader sr)
        {
            re.Style = sr.ReadSInt4();
            re.Radius = sr.ReadFloat();
            re.Direction = sr.ReadFloat();
            re.Distance = sr.ReadFloat();
            re.TiltAngleX = sr.ReadFloat();
            re.TiltAngleY = sr.ReadFloat();
            re.ZoomRateX = sr.ReadFloat();
            re.ZoomRateY = sr.ReadFloat();
            re.RotationStyle = sr.ReadSInt4();
            re.StartTransparency = sr.ReadFloat();
            re.StartPosition = sr.ReadFloat();
            re.EndTransparency = sr.ReadFloat();
            re.EndPosition = sr.ReadFloat();
            re.OffsetDirection = sr.ReadFloat();
        }
    }

}