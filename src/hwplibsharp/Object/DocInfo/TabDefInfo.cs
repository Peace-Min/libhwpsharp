using HwpLib.Object.DocInfo.TabDef;

namespace HwpLib.Object.DocInfo;

/// <summary>
/// 탭 정의에 대한 레코드
/// </summary>
public class TabDefInfo
{
    private readonly TabDefProperty _property;
    private readonly List<TabInfo> _tabInfoList;

    public TabDefInfo()
    {
        _property = new TabDefProperty();
        _tabInfoList = new List<TabInfo>();
    }

    public TabDefProperty Property => _property;

    public TabInfo AddNewTabInfo()
    {
        var tabInfo = new TabInfo();
        _tabInfoList.Add(tabInfo);
        return tabInfo;
    }

    public IReadOnlyList<TabInfo> TabInfoList => _tabInfoList;

    public TabDefInfo Clone()
    {
        var cloned = new TabDefInfo();
        cloned._property.Copy(_property);

        cloned._tabInfoList.Clear();
        foreach (var tabInfo in _tabInfoList)
        {
            cloned._tabInfoList.Add(tabInfo.Clone());
        }

        return cloned;
    }
}
