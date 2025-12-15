using HwpLib.Object.DocInfo.BinData;

namespace HwpLib.Object.DocInfo;

/// <summary>
/// 바이너리 데이터를 나타내는 레코드
/// </summary>
public class BinDataInfo
{
    private readonly BinDataProperty _property;
    private string? _absolutePathForLink;
    private string? _relativePathForLink;
    private int _binDataId;
    private string? _extensionForEmbedding;

    public BinDataInfo()
    {
        _property = new BinDataProperty();
    }

    public BinDataProperty Property => _property;

    /// <summary>
    /// Type이 "LINK"일 때, 연결 파일의 절대 경로
    /// </summary>
    public string? AbsolutePathForLink
    {
        get => _absolutePathForLink;
        set => _absolutePathForLink = value;
    }

    /// <summary>
    /// Type이 "LINK"일 때, 연결 파일의 상대 경로
    /// </summary>
    public string? RelativePathForLink
    {
        get => _relativePathForLink;
        set => _relativePathForLink = value;
    }

    /// <summary>
    /// Type이 "EMBEDDING"이거나 "STORAGE"일 때, BINDATASTORAGE에 저장된 바이너리 데이터의 아이디
    /// </summary>
    public int BinDataId
    {
        get => _binDataId;
        set => _binDataId = value;
    }

    /// <summary>
    /// Type이 "EMBEDDING"일 때 extension("." 제외)
    /// </summary>
    public string? ExtensionForEmbedding
    {
        get => _extensionForEmbedding;
        set => _extensionForEmbedding = value;
    }

    public BinDataInfo Clone()
    {
        var cloned = new BinDataInfo();
        cloned._property.Copy(_property);
        cloned._absolutePathForLink = _absolutePathForLink;
        cloned._relativePathForLink = _relativePathForLink;
        cloned._binDataId = _binDataId;
        cloned._extensionForEmbedding = _extensionForEmbedding;
        return cloned;
    }
}
