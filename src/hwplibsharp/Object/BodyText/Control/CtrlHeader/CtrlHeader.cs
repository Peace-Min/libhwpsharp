namespace HwpLib.Object.BodyText.Control.CtrlHeader;

/// <summary>
/// 컨트롤 헤더 객체들을 위한 부모 클래스
/// </summary>
public abstract class CtrlHeader
{
    /// <summary>
    /// 컨트롤 아이디
    /// </summary>
    protected uint ctrlId;

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="ctrlId">컨트롤 아이디</param>
    public CtrlHeader(uint ctrlId)
    {
        this.ctrlId = ctrlId;
    }

    /// <summary>
    /// 컨트롤 아이디를 반환한다.
    /// </summary>
    public uint CtrlId => ctrlId;

    public abstract void Copy(CtrlHeader from);
}
