namespace HwpLib.Object.BodyText.Control.CtrlHeader.PageOddEvenAdjust;

using HwpLib.Util.Binary;

/// <summary>
/// 홀/짝수 조정(페이지 번호 제어) 컨트롤의 속성을 나타내는 객체
/// </summary>
public class PageOddEvenAdjustHeaderProperty
{
    /// <summary>
    /// 파일에 저장되는 정수값(unsigned 4 byte)
    /// </summary>
    public uint Value { get; set; }

    /// <summary>
    /// 생성자
    /// </summary>
    public PageOddEvenAdjustHeaderProperty()
    {
    }

    /// <summary>
    /// 홀/짝수 구분 (0~1 bit)
    /// </summary>
    public OddEvenPage OddEvenPage
    {
        get => OddEvenPageExtensions.FromValue((byte)BitFlag.Get(Value, 0, 1));
        set => Value = (uint)BitFlag.Set(Value, 0, 1, value.GetValue());
    }

    public void Copy(PageOddEvenAdjustHeaderProperty from)
    {
        Value = from.Value;
    }
}
