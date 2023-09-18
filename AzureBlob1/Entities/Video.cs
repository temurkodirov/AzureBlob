namespace AzureBlob1.Entities;

public class Video
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string VideoPath { get; set; } = string.Empty;
    public string VideoImagePath { get; set; } = string.Empty;
    public double VideoDuration {  get; set; }
}
