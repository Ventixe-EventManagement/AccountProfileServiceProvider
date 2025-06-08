using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ProfileEntity
{
    [Key]
    public string UserId { get; set; } = null!;
    
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(20)]
    public string? PhoneNumber { get; set; }
}
