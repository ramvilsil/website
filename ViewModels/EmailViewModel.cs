namespace Application.ViewModels;
public class EmailViewModel
{
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
    public string MessageSubject { get; set; }
    public string MessageBody { get; set; }
    public DateTime MessageTimestamp { get; set; }
}