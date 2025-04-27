using SkiaSharp;
using User.Application.Interfaces;

namespace User.Application.Services;

public class ThumbnailService: IThumbnailService
{
    public byte[] GetThumbnailImage(byte[] originalImage, int width, int height)
    {
        var inputStream = new MemoryStream(originalImage);
        using var codec = SKCodec.Create(inputStream);
        using var bitmap = SKBitmap.Decode(codec);
        
        using var scaledBitmap = bitmap.Resize(new SKSizeI(width, height), SKFilterQuality.Low);
        var scaledImage = scaledBitmap.Encode(codec.EncodedFormat, 50);

        return scaledImage.ToArray();
    }
}