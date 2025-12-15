using HwpLib.Object.DocInfo.DocumentProperties;

namespace HwpLib.Object.DocInfo;

/// <summary>
/// 문서 속성를 나타내는 레코드
/// </summary>
public class DocumentPropertiesInfo
{
    private int _sectionCount;
    private readonly StartNumber _startNumber;
    private readonly CaretPosition _caretPosition;

    public DocumentPropertiesInfo()
    {
        _startNumber = new StartNumber();
        _caretPosition = new CaretPosition();
    }

    public int SectionCount
    {
        get => _sectionCount;
        set => _sectionCount = value;
    }

    public StartNumber StartNumber => _startNumber;
    public CaretPosition CaretPosition => _caretPosition;

    public void Copy(DocumentPropertiesInfo from)
    {
        _sectionCount = from._sectionCount;
        _startNumber.Copy(from._startNumber);
        _caretPosition.Copy(from._caretPosition);
    }
}
