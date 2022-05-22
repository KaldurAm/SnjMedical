using System.ComponentModel.DataAnnotations;

namespace SnjMedical.ScheduleService.Models;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool Deleted { get; set; } = false;
}