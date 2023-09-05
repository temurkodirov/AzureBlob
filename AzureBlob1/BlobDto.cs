namespace AzureBlob1;

public class BlobDto
{
    public string Uri { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string ContentType { get; set; } = String.Empty;
    public Stream? Content { get; set; }
}
