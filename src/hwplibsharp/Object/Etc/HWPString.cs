namespace HwpLib.Object.Etc;

using System.Text;

/// <summary>
/// HWP 문자열을 나타내는 클래스
/// </summary>
public class HWPString
{
    private byte[]? bytes;

    public HWPString()
    {
        bytes = null;
    }

    public HWPString(byte[] bytes)
    {
        this.bytes = bytes;
    }

    public byte[]? Bytes
    {
        get => bytes;
        set => bytes = value;
    }

    /// <summary>
    /// UTF-16LE 인코딩으로 문자열을 반환한다.
    /// </summary>
    /// <returns>문자열</returns>
    public string? ToUTF16LEString()
    {
        if (bytes == null)
        {
            return null;
        }
        return Encoding.Unicode.GetString(bytes);
    }

    /// <summary>
    /// UTF-16LE 인코딩으로 문자열을 설정한다.
    /// </summary>
    /// <param name="utf16LE">문자열</param>
    public void FromUTF16LEString(string? utf16LE)
    {
        if (utf16LE != null && utf16LE.Length > 0)
        {
            bytes = Encoding.Unicode.GetBytes(utf16LE);
        }
    }

    public HWPString Clone()
    {
        HWPString cloned = new HWPString();
        cloned.Copy(this);
        return cloned;
    }

    public void Copy(HWPString from)
    {
        bytes = from.bytes;
    }

    public int GetWCharsSize()
    {
        if (bytes != null)
        {
            return 2 + bytes.Length;
        }
        return 2;
    }

    public bool Equals(HWPString other)
    {
        if (bytes == null && other.bytes == null)
        {
            return true;
        }
        if (bytes == null || other.bytes == null)
        {
            return false;
        }
        return bytes.SequenceEqual(other.bytes);
    }
}
