namespace AzureBlob1.Dtos;

public class VideoDto
{
    public string Title { get; set; } = string.Empty;
    public IFormFile? VideoPath { get; set; } 
    public IFormFile? VideoImagePath { get; set; } 
    public double VideoDuration { get; set; }
}
