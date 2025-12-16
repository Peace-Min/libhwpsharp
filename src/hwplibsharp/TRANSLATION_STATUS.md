# hwplib Java → C# 번역 현황

이 문서는 [neolord0/hwplib](https://github.com/neolord0/hwplib) Java 라이브러리를 C# (.NET 8.0)으로 변환하는 프로젝트의 현재 상태를 정리합니다.

## 🎉 빌드 상태

| 항목 | 상태 |
|------|------|
| 빌드 | ✅ **성공** |
| 오류 | 0개 |
| 경고 | 0개 |
| 출력 | `hwplibsharp.dll` (net8.0) |

## 📋 프로젝트 개요

| 항목 | 값 |
|------|-----|
| 대상 프레임워크 | .NET 8.0 (`net8.0`) |
| 프로젝트 이름 | hwplibsharp |
| 버전 | 1.1.7 (원본 Java 라이브러리와 동일) |
| 원본 라이브러리 | [neolord0/hwplib](https://github.com/neolord0/hwplib) (Java) |
| 원 저작자 | neolord0 |
| .NET 포팅 | rkttu (AI 기반 번역) |
| 주요 의존성 | OpenMcdf 3.1.0 |

---

## 📁 폴더 구조 및 번역 현황

### 1. CompoundFile/ (Compound File 처리)

Java 원본에서는 **Apache POI**를 사용하지만, C#에서는 **OpenMcdf 3.x**를 사용합니다.

| Java 클래스 | C# 클래스 | 상태 | 비고 |
|-------------|-----------|------|------|
| `POIFSFileSystem` | `CompoundFileSystem` | ✅ 완료 | OpenMcdf `RootStorage` 래핑 |
| `DirectoryEntry` | `IDirectoryEntry` | ✅ 완료 | 인터페이스로 정의 |
| `DirectoryNode` | `StorageWrapper` | ✅ 완료 | OpenMcdf `Storage` 래핑 |
| `DocumentEntry` | `IStreamEntry` | ✅ 완료 | 인터페이스로 정의 |
| `DocumentNode` | `StreamWrapper` | ✅ 완료 | OpenMcdf `CfbStream` 래핑 |
| `DocumentInputStream` | (제거됨) | ✅ 완료 | `MemoryStream` 직접 사용 |
| `CompoundFileReader` | `CompoundFileReader` | ✅ 완료 | 스토리지 탐색 |
| `CompoundFileWriter` | `CompoundFileWriter` | ✅ 완료 | 스토리지 쓰기 |
| `StreamReader` | `StreamReader` | ✅ 완료 | 바이너리 데이터 읽기 |
| `StreamWriter` | `StreamWriter` | ✅ 완료 | 바이너리 데이터 쓰기 |
| `RecordHeader` (내부) | `RecordHeader` | ✅ 완료 | StreamReader.cs 내 정의 |

#### OpenMcdf 3.x API 변경사항

OpenMcdf 3.x에서는 이전 버전(2.x)과 API가 크게 다릅니다:

```text
OpenMcdf 2.x              →  OpenMcdf 3.x
─────────────────────────────────────────
CompoundFile              →  RootStorage
CFStorage                 →  Storage
CFStream                  →  CfbStream
VisitEntries()            →  EnumerateEntries()
TryGetStorage/TryGetStream →  OpenStorage/OpenStream
```

---

### 2. Binary/ (바이너리 유틸리티)

| Java 클래스 | C# 클래스 | 상태 | 비고 |
|-------------|-----------|------|------|
| `Compressor` | `Compressor` | ✅ 완료 | `DeflateStream` 사용 |
| `Obfuscation` | `Obfuscation` | ✅ 완료 | 배포용 문서 복호화 (AES/ECB) |

#### 압축 처리 차이점

- **Java**: `java.util.zip.Inflater(true)` (raw deflate)
- **C#**: `System.IO.Compression.DeflateStream` (raw deflate)

#### 암호화 처리 차이점

- **Java**: `javax.crypto.Cipher` with `AES/ECB/PKCS5Padding`
- **C#**: `System.Security.Cryptography.Aes` with `CipherMode.ECB`, `PaddingMode.PKCS7`
  - PKCS5와 PKCS7은 16바이트 블록에서 동일하게 동작

---

### 3. Util/ (유틸리티 클래스)

#### 3.1 Util/Binary/ (비트 연산 유틸리티)

| Java 클래스 | C# 클래스 | 상태 | 비고 |
|-------------|-----------|------|------|
| `BitFlag` | `BitFlag` | ✅ 완료 | `uint` 오버로드 추가 |

#### 주요 변경사항

- Java의 `long`/`int` 외에 C#의 `uint` 타입 오버로드 추가
- 비트 마스크 연산에서 `unchecked` 컨텍스트 사용

#### 3.2 Util/ (일반 유틸리티)

| Java 클래스 | C# 클래스 | 상태 | 비고 |
|-------------|-----------|------|------|
| `ArrayUtil` | `ArrayUtil` | ✅ 완료 | 배열 empty/null 검사 |
| `StringUtil` | `StringUtil` | ✅ 완료 | 문자열 처리 유틸리티 |

---

### 4. Object/ (HWP 문서 객체 모델)

#### 4.1 최상위 객체

| Java 클래스 | C# 클래스 | 상태 | 비고 |
|-------------|-----------|------|------|
| `HWPFile` | `HWPFile` | ✅ 완료 | |
| `Scripts` | `Scripts` | ✅ 완료 | |
| `RecordHeader` | `RecordHeader` | ✅ 완료 | |

#### 4.2 Object/FileHeader/

| Java 클래스 | C# 클래스 | 상태 |
|-------------|-----------|------|
| `FileHeader` | `FileHeader` | ✅ 완료 |
| `FileVersion` | `FileVersion` | ✅ 완료 |

#### 4.3 Object/Etc/

| Java 클래스 | C# 클래스 | 상태 |
|-------------|-----------|------|
| `HWPTag` | `HWPTag` | ✅ 완료 |
| `UnknownRecord` | `UnknownRecord` | ✅ 완료 |
| `Color4Byte` | `Color4Byte` | ✅ 완료 |
| `HWPString` | `HWPString` | ✅ 완료 |

#### 4.4 Object/DocInfo/ (⚠️ 네이밍 변경)

**네임스페이스/클래스 이름 충돌 해결**을 위해 일부 클래스명에 `Info` 접미사를 추가했습니다.

| Java 클래스 | C# 클래스 | 상태 | 변경 이유 |
|-------------|-----------|------|-----------|
| `DocInfo` | `DocInfo` | ✅ 완료 | |
| `BinData` | `BinDataInfo` | ✅ 완료 | `BinData` 네임스페이스와 충돌 |
| `BorderFill` | `BorderFillInfo` | ✅ 완료 | `BorderFill` 네임스페이스와 충돌 |
| `CharShape` | `CharShapeInfo` | ✅ 완료 | `CharShape` 네임스페이스와 충돌 |
| `CompatibleDocument` | `CompatibleDocumentInfo` | ✅ 완료 | 네임스페이스 충돌 |
| `DocumentProperties` | `DocumentPropertiesInfo` | ✅ 완료 | 네임스페이스 충돌 |
| `FaceName` | `FaceNameInfo` | ✅ 완료 | `FaceName` 네임스페이스와 충돌 |
| `Numbering` | `NumberingInfo` | ✅ 완료 | `Numbering` 네임스페이스와 충돌 |
| `ParaShape` | `ParaShapeInfo` | ✅ 완료 | `ParaShape` 네임스페이스와 충돌 |
| `Style` | `StyleInfo` | ✅ 완료 | `Style` 네임스페이스와 충돌 |
| `TabDef` | `TabDefInfo` | ✅ 완료 | `TabDef` 네임스페이스와 충돌 |
| `Bullet` | `Bullet` | ✅ 완료 | |
| `IDMappings` | `IDMappings` | ✅ 완료 | |
| `LayoutCompatibility` | `LayoutCompatibility` | ✅ 완료 | |
| `MemoShape` | `MemoShape` | ✅ 완료 | |

---

## ⚠️ 주요 차이점 요약

### 1. Apache POI → OpenMcdf 3.x

Java에서 사용하는 Apache POI의 OLE2/Compound File 처리 기능을 OpenMcdf 3.x로 대체했습니다.

```csharp
// Java (Apache POI)
POIFSFileSystem fs = new POIFSFileSystem(new FileInputStream(file));
DirectoryEntry root = fs.getRoot();
DocumentEntry entry = (DocumentEntry) root.getEntry("DocInfo");
DocumentInputStream dis = new DocumentInputStream(entry);

// C# (OpenMcdf 3.x)
using var fs = new CompoundFileSystem(filePath);
var root = fs.Root;
var entry = (StreamWrapper)root.GetStream("DocInfo");
var data = entry.GetData();
```

### 2. 네임스페이스/클래스 이름 충돌 해결

C#에서는 네임스페이스와 클래스 이름이 같으면 충돌이 발생합니다. 예를 들어:

```text
Object/DocInfo/BinData/          (네임스페이스)
Object/DocInfo/BinData.cs        (클래스) → BinDataInfo.cs로 변경
```

### 3. 컬렉션 타입 변경

| Java | C# |
|------|-----|
| `ArrayList<T>` | `List<T>` |
| `HashSet<T>` | `HashSet<T>` |
| `HashMap<K,V>` | `Dictionary<K,V>` |

### 4. 프로퍼티 스타일

Java의 getter/setter 메서드를 C# 프로퍼티로 변환:

```java
// Java
private String name;
public String getName() { return name; }
public void setName(String name) { this.name = name; }
```

```csharp
// C#
public string Name { get; set; }
```

### 5. Nullable 참조 타입

C# 프로젝트에서 `<Nullable>enable</Nullable>` 설정으로 null 안전성 강화:

```csharp
public RecordHeader? CurrentRecordHeader { get; }  // nullable
public FileVersion FileVersion { get; }            // non-nullable
```

---

## 🚧 미구현 항목

### Reader 클래스

| Java 패키지 | 상태 | 설명 |
|-------------|------|------|
| `reader/HWPReader` | ✅ 완료 | HWP 파일 읽기 진입점 |
| `reader/docinfo/*` | ✅ 완료 | DocInfo 스트림 파서 |
| `reader/bodytext/*` | ✅ 완료 | BodyText 스트림 파서 |
| `reader/bindata/*` | ✅ 완료 | BinData 스토리지 파서 |

### Writer 클래스

| Java 패키지 | 상태 | 설명 |
|-------------|------|------|
| `writer/HWPWriter` | ✅ 완료 | HWP 파일 쓰기 진입점 |
| `writer/docinfo/*` | ✅ 완료 | DocInfo 스트림 작성기 |
| `writer/bodytext/*` | ✅ 완료 | BodyText 스트림 작성기 |

### Object 모델

| Java 패키지 | 상태 | 설명 |
|-------------|------|------|
| `object/bodytext/*` | ✅ 완료 | 본문 텍스트 객체 |
| `object/bindata/*` | ✅ 완료 | 바이너리 데이터 객체 |
| `object/docinfo/borderfill/*` | ✅ 완료 | BorderFill 세부 클래스 |
| `object/docinfo/charshape/*` | ✅ 완료 | CharShape 세부 클래스 |
| `object/docinfo/parashape/*` | ✅ 완료 | ParaShape 세부 클래스 |
| `object/docinfo/numbering/*` | ✅ 완료 | Numbering 세부 클래스 |
| `object/docinfo/facename/*` | ✅ 완료 | FaceName 세부 클래스 |
| `object/docinfo/tabdef/*` | ✅ 완료 | TabDef 세부 클래스 |

### Tool 클래스

| Java 패키지 | 상태 | 설명 |
|-------------|------|------|
| `tool/textextractor/*` | ✅ 완료 | 텍스트 추출기 |
| `tool/paragraphadder/*` | ✅ 완료 | 문단 추가 도구 |
| `tool/objectfinder/*` | ✅ 완료 | 객체 검색 도구 |
| `tool/paragraphmaker/*` | ✅ 완료 | 문단 생성 도구 |

---

## 📊 진행률

| 카테고리 | 예상 파일 수 | 완료 | 진행률 |
|----------|-------------|------|--------|
| CompoundFile | ~10 | 10 | 100% |
| Binary/Util | ~5 | 5 | 100% |
| Object (전체) | ~200+ | 200+ | 100% |
| Reader | ~50+ | 50+ | 100% |
| Writer | ~50+ | 50+ | 100% |
| Tool | ~50+ | 50+ | 100% |

**전체 진행률: 100% ✅**

---

## 🔄 Java → C# API 변환 패턴 가이드

> ⚠️ **중요**: 이 섹션은 추후 에이전트가 코드를 수정하거나 추가할 때 반드시 참고해야 하는 패턴입니다.

### 1. Getter/Setter → Property 변환

Java의 getter/setter 메서드는 C#의 프로퍼티로 변환됩니다.

```java
// Java
private int value;
public int getValue() { return value; }
public void setValue(int value) { this.value = value; }
```

```csharp
// C#
public int Value { get; set; }
```

#### 주요 변환 예시

| Java 메서드 | C# 프로퍼티 | 비고 |
|-------------|-------------|------|
| `getXxx()` / `setXxx(v)` | `Xxx` | 기본 패턴 |
| `isXxx()` | `Xxx` (bool) | 불리언 getter |
| `hasXxx()` | `HasXxx` (bool) | 존재 여부 확인 |

### 2. Protected 멤버 접근

Java에서 protected 필드에 직접 접근하는 경우, C#에서는 public 메서드를 통해 접근해야 합니다.

```java
// Java - Control 클래스 내부에서
target.Header.copy(source.Header);  // Header는 protected
```

```csharp
// C# - public 메서드 사용
target.GetHeader()?.Copy(source.GetHeader());
```

#### Control 클래스 계층별 Header 접근 방식

| 클래스 | Java | C# | 반환 타입 |
|--------|------|-----|----------|
| `Control` (기본) | `Header` (protected) | `GetHeader()` | `CtrlHeader?` |
| `ControlAutoNumber` | `Header` | `GetHeader()` | `CtrlHeaderAutoNumber?` |
| `ControlEndnote` | `Header` | `Header` (public new) | `CtrlHeaderEndnote` |
| `ControlFooter` | `Header` | `Header` (public new) | `CtrlHeaderFooter` |
| `ControlFootnote` | `Header` | `Header` (public new) | `CtrlHeaderFootnote` |
| `ControlHeader` | `Header` | `Header` (public new) | `CtrlHeaderHeader` |

### 3. Nullable 조건부 할당 패턴

`?.` 연산자로 Value 속성에 할당할 때 nullable 타입 오류가 발생합니다.

```csharp
// ❌ 잘못된 패턴 (CS0266 오류)
target.BorderColor?.Value = source.BorderColor?.Value;

// ✅ 올바른 패턴
if (source.BorderColor != null && target.BorderColor != null)
    target.BorderColor.Value = source.BorderColor.Value;
```

#### 적용 대상 클래스

- `Color4Byte.Value` (uint)
- `LineInfoProperty.Value` (uint)
- `FillType.Value` (uint)
- 기타 `.Value` 프로퍼티를 가진 래퍼 클래스

### 4. IReadOnlyList 컬렉션 처리

C#에서 `IReadOnlyList<T>`는 `Add()` 메서드를 지원하지 않습니다.

```java
// Java
target.getList().add(item);
```

```csharp
// ❌ 잘못된 패턴
target.List.Add(item);  // IReadOnlyList에 Add() 없음

// ✅ 올바른 패턴 - AddNewXxx() 메서드 사용
var newItem = target.AddNewItem();
newItem.Copy(sourceItem);

// 또는 ToList()로 변환 후 사용
var list = target.List.ToList();
```

#### AddNew 메서드 패턴

| 클래스 | 메서드 | 설명 |
|--------|--------|------|
| `BodyText` | `AddNewMemo()` | 새 메모 추가 |
| `GradientFill` | `AddNewColor()` | 새 색상 추가 |
| `GradientFill` | `AddChangePoint(int)` | 변경점 추가 |
| `Section` | `AddNewParagraph()` | 새 문단 추가 |
| `Row` | `AddNewCell()` | 새 셀 추가 |

### 5. 클래스/네임스페이스 이름 충돌 해결

C#에서는 네임스페이스와 클래스 이름이 같으면 충돌이 발생합니다.

#### DocInfo 클래스 이름 변경

| Java 클래스 | C# 클래스 | 이유 |
|-------------|-----------|------|
| `BinData` | `BinDataInfo` | `BinData/` 네임스페이스 충돌 |
| `BorderFill` | `BorderFillInfo` | `BorderFill/` 네임스페이스 충돌 |
| `CharShape` | `CharShapeInfo` | `CharShape/` 네임스페이스 충돌 |
| `FaceName` | `FaceNameInfo` | `FaceName/` 네임스페이스 충돌 |
| `Numbering` | `NumberingInfo` | `Numbering/` 네임스페이스 충돌 |
| `ParaShape` | `ParaShapeInfo` | `ParaShape/` 네임스페이스 충돌 |
| `Style` | `StyleInfo` | `Style/` 네임스페이스 충돌 |
| `TabDef` | `TabDefInfo` | `TabDef/` 네임스페이스 충돌 |

#### 타입 별칭 사용

```csharp
// 네임스페이스 충돌 해결을 위한 using alias
using ForParagraphListField = HwpLib.Tool.ObjectFinder.ForField.ForParagraphList;
```

### 6. 특수 타입 변환

#### 6.1 FaceNameIds/Ratios/CharSpaces/RelativeSizes/CharOffsets

Java의 인덱스 기반 접근을 C# 프로퍼티로 변환:

```java
// Java
source.getFaceNameIds().getHangul()
target.getFaceNameIds().setHangul(value)
```

```csharp
// C#
source.FaceNameIds.Hangul
target.FaceNameIds.Hangul = value
```

| Java 메서드 | C# 프로퍼티 |
|-------------|-------------|
| `getHangul()` / `setHangul()` | `Hangul` |
| `getLatin()` / `setLatin()` | `Latin` |
| `getHanja()` / `setHanja()` | `Hanja` |
| `getJapanese()` / `setJapanese()` | `Japanese` |
| `getOther()` / `setOther()` | `Other` |
| `getSymbol()` / `setSymbol()` | `Symbol` |
| `getUser()` / `setUser()` | `User` |

#### 6.2 ShapeComponentPicture

| Java 메서드 | C# 프로퍼티 |
|-------------|-------------|
| `getLeftTop()` | `LeftTop` |
| `getRightTop()` | `RightTop` |
| `getLeftBottom()` | `LeftBottom` |
| `getRightBottom()` | `RightBottom` |
| `getLeftAfterCutting()` / `setLeftAfterCutting()` | `LeftAfterCutting` |
| `getInnerMargin()` | `InnerMargin` |
| `getBorderTransparency()` / `setBorderTransparency()` | `BorderTransparency` |
| `getPictureEffect()` | `PictureEffect` |
| `getImageWidth()` / `setImageWidth()` | `ImageWidth` |
| `getImageHeight()` / `setImageHeight()` | `ImageHeight` |

#### 6.3 FillInfo/FillType

| Java 메서드 | C# 프로퍼티 |
|-------------|-------------|
| `getFillType()` | `Type` |
| `fillType.hasPatternFill()` | `Type.HasPatternFill` |
| `fillType.hasGradientFill()` | `Type.HasGradientFill` |
| `fillType.hasImageFill()` | `Type.HasImageFill` |

#### 6.4 PatternFill/GradientFill

| Java 메서드 | C# 프로퍼티 |
|-------------|-------------|
| `getBackColor()` | `BackColor` |
| `getGradientType()` / `setGradientType()` | `GradientType` |
| `getStartAngle()` / `setStartAngle()` | `StartAngle` |
| `getBlurringDegree()` / `setBlurringDegree()` | `BlurringDegree` |
| `getBlurringCenter()` / `setBlurringCenter()` | `BlurringCenter` |

#### 6.5 HWPString

| Java 메서드 | C# 프로퍼티 |
|-------------|-------------|
| `getBytes()` | `Bytes` |
| `setBytes(byte[])` | `Bytes = value` |

#### 6.6 MemoList

| Java 메서드 | C# 프로퍼티 |
|-------------|-------------|
| `getMemoIndex()` | `MemoIndex` |
| `setMemoIndex(long)` | `MemoIndex = value` |

#### 6.7 CtrlHeaderField

| Java 메서드 | C# 프로퍼티 |
|-------------|-------------|
| `getMemoIndex()` | `MemoIndex` |
| `setMemoIndex(int)` | `MemoIndex = value` |

### 7. Enum 이름 변경

| Java Enum | C# Enum | 비고 |
|-----------|---------|------|
| `NumberType` | `ParagraphNumberType` | 네임스페이스 충돌 방지 |

### 8. 타입 변환 주의사항

#### 8.1 부호 있는/없는 정수

| Java | C# | 비고 |
|------|-----|------|
| `int` | `int` 또는 `uint` | 컨텍스트에 따라 다름 |
| `long` | `long` 또는 `uint` | 컨텍스트에 따라 다름 |

#### 8.2 CharShapeID 특수 값

```csharp
// -1을 CharShapeID로 사용할 때
CharShapeID = uint.MaxValue;  // unchecked((uint)-1) 대신
```

---

## 📝 다음 단계 권장사항

1. **DocInfo 세부 클래스 완성** - BorderFill, CharShape, ParaShape 등의 하위 클래스
2. **Reader 구현** - `HWPReader` 및 DocInfo 파서
3. **BodyText 객체 모델** - 문단, 컨트롤 등
4. **Writer 구현** - 파일 저장 기능
5. **Tool 유틸리티** - 텍스트 추출, 검색 등

---

## 🔧 빌드 및 테스트

```bash
cd src/dotnet/hwplibsharp
dotnet build
dotnet test  # (테스트 프로젝트 추가 시)
```

---

마지막 업데이트: 2025-12-15
