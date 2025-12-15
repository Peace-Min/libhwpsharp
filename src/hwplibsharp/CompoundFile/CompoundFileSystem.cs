using OpenMcdf;

namespace HwpLib.CompoundFile;

/// <summary>
/// OpenMcdf 3.x의 RootStorage를 래핑하는 클래스
/// POI의 POIFSFileSystem에 대응
/// </summary>
public class CompoundFileSystem : IDisposable
{
    private readonly RootStorage _rootStorage;
    private readonly StorageWrapper _root;
    private bool _disposed;

    /// <summary>
    /// 파일에서 Compound File을 읽어 생성한다.
    /// </summary>
    /// <param name="filePath">파일 경로</param>
    public CompoundFileSystem(string filePath)
    {
        _rootStorage = RootStorage.OpenRead(filePath);
        _root = new StorageWrapper(_rootStorage);
    }

    /// <summary>
    /// 스트림에서 Compound File을 읽어 생성한다.
    /// </summary>
    /// <param name="stream">입력 스트림</param>
    public CompoundFileSystem(Stream stream)
    {
        _rootStorage = RootStorage.Open(stream, StorageModeFlags.LeaveOpen);
        _root = new StorageWrapper(_rootStorage);
    }

    /// <summary>
    /// 바이트 배열에서 Compound File을 읽어 생성한다.
    /// </summary>
    /// <param name="data">바이트 배열</param>
    public CompoundFileSystem(byte[] data)
        : this(new MemoryStream(data))
    {
    }

    /// <summary>
    /// 루트 디렉토리(스토리지)
    /// </summary>
    public StorageWrapper Root => _root;

    /// <summary>
    /// 내부 RootStorage 반환 (내부용)
    /// </summary>
    internal RootStorage InternalRootStorage => _rootStorage;

    /// <summary>
    /// 리소스 해제
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _rootStorage.Dispose();
            }
            _disposed = true;
        }
    }

    ~CompoundFileSystem()
    {
        Dispose(false);
    }
}
