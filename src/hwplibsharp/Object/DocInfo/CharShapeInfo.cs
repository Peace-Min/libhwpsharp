using HwpLib.Object.DocInfo.CharShape;
using HwpLib.Object.Etc;

namespace HwpLib.Object.DocInfo;

/// <summary>
/// 글자 모양을 나타내는 레코드
/// </summary>
public class CharShapeInfo
{
    private readonly FaceNameIds _faceNameIds;
    private readonly Ratios _ratios;
    private readonly CharSpaces _charSpaces;
    private readonly RelativeSizes _relativeSizes;
    private readonly CharOffsets _charOffsets;
    private int _baseSize;
    private readonly CharShapeProperty _property;
    private sbyte _shadowGap1;
    private sbyte _shadowGap2;
    private readonly Color4Byte _charColor;
    private readonly Color4Byte _underLineColor;
    private readonly Color4Byte _shadeColor;
    private readonly Color4Byte _shadowColor;
    private int _borderFillId;
    private readonly Color4Byte _strikeLineColor;

    public CharShapeInfo()
    {
        _faceNameIds = new FaceNameIds();
        _ratios = new Ratios();
        _charSpaces = new CharSpaces();
        _relativeSizes = new RelativeSizes();
        _charOffsets = new CharOffsets();
        _property = new CharShapeProperty();
        _charColor = new Color4Byte();
        _underLineColor = new Color4Byte();
        _shadeColor = new Color4Byte();
        _shadowColor = new Color4Byte();
        _strikeLineColor = new Color4Byte();
    }

    public FaceNameIds FaceNameIds => _faceNameIds;
    public Ratios Ratios => _ratios;
    public CharSpaces CharSpaces => _charSpaces;
    public RelativeSizes RelativeSizes => _relativeSizes;
    public CharOffsets CharOffsets => _charOffsets;

    public int BaseSize
    {
        get => _baseSize;
        set => _baseSize = value;
    }

    public CharShapeProperty Property => _property;

    public sbyte ShadowGap1
    {
        get => _shadowGap1;
        set => _shadowGap1 = value;
    }

    public sbyte ShadowGap2
    {
        get => _shadowGap2;
        set => _shadowGap2 = value;
    }

    public Color4Byte CharColor => _charColor;
    public Color4Byte UnderLineColor => _underLineColor;
    public Color4Byte ShadeColor => _shadeColor;
    public Color4Byte ShadowColor => _shadowColor;

    public int BorderFillId
    {
        get => _borderFillId;
        set => _borderFillId = value;
    }

    /// <summary>
    /// 취소선 색 (5.0.3.0 이상)
    /// </summary>
    public Color4Byte StrikeLineColor => _strikeLineColor;

    public CharShapeInfo Clone()
    {
        var cloned = new CharShapeInfo();
        cloned._faceNameIds.Copy(_faceNameIds);
        cloned._ratios.Copy(_ratios);
        cloned._charSpaces.Copy(_charSpaces);
        cloned._relativeSizes.Copy(_relativeSizes);
        cloned._charOffsets.Copy(_charOffsets);
        cloned._baseSize = _baseSize;
        cloned._property.Copy(_property);
        cloned._shadowGap1 = _shadowGap1;
        cloned._shadowGap2 = _shadowGap2;
        cloned._charColor.Copy(_charColor);
        cloned._underLineColor.Copy(_underLineColor);
        cloned._shadeColor.Copy(_shadeColor);
        cloned._shadowColor.Copy(_shadowColor);
        cloned._borderFillId = _borderFillId;
        cloned._strikeLineColor.Copy(_strikeLineColor);
        return cloned;
    }
}
