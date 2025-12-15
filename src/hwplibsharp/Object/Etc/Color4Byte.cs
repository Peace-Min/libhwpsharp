namespace HwpLib.Object.Etc;

using HwpLib.Util.Binary;

/// <summary>
/// 4byte 색상 객체. windows API의 COLORREF에 대응하는 객체
/// </summary>
public class Color4Byte
{
    /// <summary>
    /// unsigned 4 byte color 값을 저장
    /// </summary>
    private uint value;

    /// <summary>
    /// 생성자
    /// </summary>
    public Color4Byte()
    {
    }

    public Color4Byte(int r, int g, int b, int a)
    {
        R = (byte)r;
        G = (byte)g;
        B = (byte)b;
        A = (byte)a;
    }

    public Color4Byte(int r, int g, int b)
    {
        R = (byte)r;
        G = (byte)g;
        B = (byte)b;
        A = 0;
    }

    /// <summary>
    /// unsigned 4 byte color 값
    /// </summary>
    public uint Value
    {
        get => value;
        set => this.value = value;
    }

    /// <summary>
    /// 색상의 red 값 (0~255)
    /// </summary>
    public byte R
    {
        get => (byte)BitFlag.Get(value, 0, 7);
        set => this.value = (uint)BitFlag.Set(this.value, 0, 7, value);
    }

    /// <summary>
    /// 색상의 green 값 (0~255)
    /// </summary>
    public byte G
    {
        get => (byte)BitFlag.Get(value, 8, 15);
        set => this.value = (uint)BitFlag.Set(this.value, 8, 15, value);
    }

    /// <summary>
    /// 색상의 blue 값 (0~255)
    /// </summary>
    public byte B
    {
        get => (byte)BitFlag.Get(value, 16, 23);
        set => this.value = (uint)BitFlag.Set(this.value, 16, 23, value);
    }

    /// <summary>
    /// 색상의 alpha 값 (0~255)
    /// </summary>
    public byte A
    {
        get => (byte)BitFlag.Get(value, 24, 31);
        set => this.value = (uint)BitFlag.Set(this.value, 24, 31, value);
    }

    public Color4Byte Clone()
    {
        Color4Byte cloned = new Color4Byte();
        cloned.Copy(this);
        return cloned;
    }

    public void Copy(Color4Byte from)
    {
        value = from.value;
    }
}
