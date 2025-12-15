using HwpLib.Object.BodyText.Control;
using HwpLib.Object.BodyText.Control.CtrlHeader;

namespace HwpLib.Writer.AutoSetter.Control;

/// <summary>
/// 단 정의 컨트롤을 쓰기 전에 자동 설정하기 위한 객체
/// </summary>
public static class ForControlColumnDefine
{
    /// <summary>
    /// 단 정의 컨트롤을 자동 설정한다.
    /// </summary>
    /// <param name="cd">단 정의 컨트롤</param>
    public static void AutoSet(ControlColumnDefine cd)
    {
        var h = cd.GetHeader()!;

        if (!h.Property.IsSameWidth())
        {
            if (h.ColumnInfoList.Count > 1)
            {
                h.Property.SetColumnCount((short)h.ColumnInfoList.Count);
            }
            else
            {
                h.Property.SetColumnCount(1);
            }
        }
    }
}
