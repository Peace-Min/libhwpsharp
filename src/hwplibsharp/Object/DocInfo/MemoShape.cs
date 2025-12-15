using HwpLib.Object.DocInfo.BorderFill;
using HwpLib.Object.Etc;

namespace HwpLib.Object.DocInfo;

/// <summary>
/// 메모 모양
/// </summary>
public class MemoShape
{
    private uint _width;
    private BorderType _lineType;
    private BorderThickness _lineWidth;
    private readonly Color4Byte _lineColor;
    private readonly Color4Byte _fillColor;
    private readonly Color4Byte _activeColor;
    private uint _unknown;

    public MemoShape()
    {
        _lineType = BorderType.None;
        _lineWidth = BorderThickness.MM0_1;
        _lineColor = new Color4Byte();
        _fillColor = new Color4Byte();
        _activeColor = new Color4Byte();
        _unknown = 0;
    }

    public uint Width
    {
        get => _width;
        set => _width = value;
    }

    public BorderType LineType
    {
        get => _lineType;
        set => _lineType = value;
    }

    public BorderThickness LineWidth
    {
        get => _lineWidth;
        set => _lineWidth = value;
    }

    public Color4Byte LineColor => _lineColor;
    public Color4Byte FillColor => _fillColor;
    public Color4Byte ActiveColor => _activeColor;

    public uint Unknown
    {
        get => _unknown;
        set => _unknown = value;
    }

    public MemoShape Clone()
    {
        var cloned = new MemoShape();
        cloned._width = _width;
        cloned._lineType = _lineType;
        cloned._lineWidth = _lineWidth;
        cloned._lineColor.Copy(_lineColor);
        cloned._fillColor.Copy(_fillColor);
        cloned._activeColor.Copy(_activeColor);
        cloned._unknown = _unknown;
        return cloned;
    }
}
