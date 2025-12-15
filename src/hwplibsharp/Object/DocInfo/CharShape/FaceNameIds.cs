namespace HwpLib.Object.DocInfo.CharShape;

/// <summary>
/// 언어별 참조된 글꼴 ID(FaceID)
/// </summary>
public class FaceNameIds
{
    private readonly int[] _array;

    public FaceNameIds()
    {
        _array = new int[7];
    }

    public int[] Array => _array;

    public void SetArray(int[] array)
    {
        if (array.Length != 7)
            throw new ArgumentException("array length must be 7");
        System.Array.Copy(array, _array, 7);
    }

    public int Hangul
    {
        get => _array[0];
        set => _array[0] = value;
    }

    public int Latin
    {
        get => _array[1];
        set => _array[1] = value;
    }

    public int Hanja
    {
        get => _array[2];
        set => _array[2] = value;
    }

    public int Japanese
    {
        get => _array[3];
        set => _array[3] = value;
    }

    public int Other
    {
        get => _array[4];
        set => _array[4] = value;
    }

    public int Symbol
    {
        get => _array[5];
        set => _array[5] = value;
    }

    public int User
    {
        get => _array[6];
        set => _array[6] = value;
    }

    public void SetForAll(int faceNameId)
    {
        for (int i = 0; i < 7; i++)
            _array[i] = faceNameId;
    }

    public void Copy(FaceNameIds from)
    {
        System.Array.Copy(from._array, _array, from._array.Length);
    }
}
