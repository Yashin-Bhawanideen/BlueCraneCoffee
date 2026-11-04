using System.ComponentModel.DataAnnotations;

namespace BlueCraneCoffee.Models;

public class ContactMessage
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please tell us your name."), StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "We need an email to reply to."), EmailAddress(ErrorMessage = "That email doesn't look right."), StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Write a short message."), StringLength(1500)]
    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
