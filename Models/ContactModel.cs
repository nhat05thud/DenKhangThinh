using System.ComponentModel.DataAnnotations;

namespace DenKhangThinh.Models;

public class ContactModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Service { get; set; }
    public string? ErrorMsg { get; set; }
    public int CultureLcid { get; set; }
    public int PageId { get; set; }
}
