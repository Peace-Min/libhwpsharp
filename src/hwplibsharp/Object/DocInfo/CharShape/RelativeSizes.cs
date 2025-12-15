namespace HwpLib.Object.DocInfo.CharShape;

/// <summary>
/// 언어별 상대 크기
/// </summary>
public class RelativeSizes
{
    private readonly short[] _array;

    public RelativeSizes()
    {
        _array = new short[7];
    }

    public short[] Array => _array;

    public void SetArray(short[] array)
    {
        if (array.Length != 7)
            throw new ArgumentException("array length must be 7");
        System.Array.Copy(array, _array, 7);
    }

    public short Hangul
    {
        get => _array[0];
        set => _array[0] = value;
    }

    public short Latin
    {
        get => _array[1];
        set => _array[1] = value;
    }

    public short Hanja
    {
        get => _array[2];
        set => _array[2] = value;
    }

    public short Japanese
    {
        get => _array[3];
        set => _array[3] = value;
    }

    public short Other
    {
        get => _array[4];
        set => _array[4] = value;
    }

    public short Symbol
    {
        get => _array[5];
        set => _array[5] = value;
    }

    public short User
    {
        get => _array[6];
        set => _array[6] = value;
    }

    public void SetForAll(short relativeSize)
    {
        for (int i = 0; i < 7; i++)
            _array[i] = relativeSize;
    }

    public void Copy(RelativeSizes from)
    {
        System.Array.Copy(from._array, _array, from._array.Length);
    }
}
