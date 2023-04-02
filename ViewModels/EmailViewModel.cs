using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels;
public class EmailViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string SenderName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [MaxLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
    public string SenderEmail { get; set; } = null!;

    [Required(ErrorMessage = "Subject is required")]
    [MaxLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
    public string MessageSubject { get; set; } = null!;

    [Required(ErrorMessage = "Message is required")]
    [MaxLength(500, ErrorMessage = "Message cannot exceed 500 characters")]
    public string MessageBody { get; set; } = null!;
}