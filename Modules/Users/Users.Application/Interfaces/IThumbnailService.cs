namespace User.Application.Interfaces;

public interface IThumbnailService
{
    byte[] GetThumbnailImage(byte[] originalImage, int width, int height);
}