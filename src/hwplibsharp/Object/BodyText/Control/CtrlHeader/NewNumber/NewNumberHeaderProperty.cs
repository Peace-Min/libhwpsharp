namespace HwpLib.Object.BodyText.Control.CtrlHeader.NewNumber;

using HwpLib.Object.BodyText.Control.CtrlHeader.AutoNumber;
using HwpLib.Util.Binary;

/// <summary>
/// 새 번호 지정 컨트롤의 속성을 나타내는 객체
/// </summary>
public class NewNumberHeaderProperty
{
    /// <summary>
    /// 파일에 저장되는 정수값(unsigned 4 byte)
    /// </summary>
    public uint Value { get; set; }

    /// <summary>
    /// 생성자
    /// </summary>
    public NewNumberHeaderProperty()
    {
    }

    /// <summary>
    /// 번호 종류를 반환하거나 설정한다. (0~3 bit)
    /// </summary>
    public NumberSort NumberSort
    {
        get => NumberSortExtensions.FromValue((byte)BitFlag.Get(Value, 0, 3));
        set => Value = (uint)BitFlag.Set(Value, 0, 3, value.GetValue());
    }

    public void Copy(NewNumberHeaderProperty from)
    {
        Value = from.Value;
    }
}
