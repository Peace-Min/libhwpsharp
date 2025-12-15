using HwpLib.Object;
using HwpLib.Object.BinData;
using HwpLib.Reader;
using HwpLib.Writer;
using System.Drawing;

namespace HwpLibSharp.Test;

/// <summary>
/// 이미지 변경 테스트
/// </summary>
[TestClass]
public class ChangingImageTest
{
    [TestMethod]
    public void ChangeImage_ToGrayscale_ShouldSucceed()
    {
        // Arrange
        var filePath = TestHelper.GetSamplePath("changing-image.hwp");
        var hwpFile = HWPReader.FromFile(filePath);
        
        Assert.IsNotNull(hwpFile);
        
        // Act
        ProcessAllImages(hwpFile);
        
        var writePath = TestHelper.GetResultPath("result-changing-image.hwp");
        HWPWriter.ToFile(hwpFile, writePath);
        
        // Assert
        Assert.IsTrue(File.Exists(writePath), "이미지 변경 성공");
    }

    private static void ProcessAllImages(HWPFile hwpFile)
    {
        foreach (var ebd in hwpFile.BinData.EmbeddedBinaryDataList)
        {
            using var img = LoadImage(ebd.Data);
            if (img != null)
            {
                MakeGray(img);
                
                var changedFileBinary = MakeFileBinary(ebd.Name, img);
                if (changedFileBinary != null)
                {
                    ebd.Data = changedFileBinary;
                }
            }
        }
    }

    private static Bitmap? LoadImage(byte[] data)
    {
        try
        {
            using var ms = new MemoryStream(data);
            return new Bitmap(ms);
        }
        catch
        {
            return null;
        }
    }

    private static void MakeGray(Bitmap img)
    {
        for (int x = 0; x < img.Width; ++x)
        {
            for (int y = 0; y < img.Height; ++y)
            {
                var pixel = img.GetPixel(x, y);
                int r = pixel.R;
                int g = pixel.G;
                int b = pixel.B;

                // Normalize and gamma correct:
                float rr = (float)Math.Pow(r / 255.0, 2.2);
                float gg = (float)Math.Pow(g / 255.0, 2.2);
                float bb = (float)Math.Pow(b / 255.0, 2.2);

                // Calculate luminance:
                float lum = (float)(0.2126 * rr + 0.7152 * gg + 0.0722 * bb);

                // Gamma compand and rescale to byte range:
                int grayLevel = (int)(255.0 * Math.Pow(lum, 1.0 / 2.2));
                img.SetPixel(x, y, Color.FromArgb(grayLevel, grayLevel, grayLevel));
            }
        }
    }

    private static byte[]? MakeFileBinary(string name, Bitmap img)
    {
        var ext = GetExtension(name);
        if (ext == null) return null;
        
        using var ms = new MemoryStream();
        var format = ext.ToLowerInvariant() switch
        {
            "jpg" or "jpeg" => System.Drawing.Imaging.ImageFormat.Jpeg,
            "png" => System.Drawing.Imaging.ImageFormat.Png,
            "bmp" => System.Drawing.Imaging.ImageFormat.Bmp,
            "gif" => System.Drawing.Imaging.ImageFormat.Gif,
            _ => System.Drawing.Imaging.ImageFormat.Png
        };
        img.Save(ms, format);
        return ms.ToArray();
    }

    private static string? GetExtension(string name)
    {
        int pos = name.LastIndexOf('.');
        if (pos != -1)
        {
            return name.Substring(pos + 1);
        }
        return null;
    }
}
