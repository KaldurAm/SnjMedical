using System.ComponentModel.DataAnnotations;

namespace SnjMedical.ScheduleService.Models;

public class Schedule : BaseEntity
{
    [MinLength(36), MaxLength(40)]
    public string UserId { get; set; }
    [MinLength(36), MaxLength(40)]
    public string DoctorId { get; set; }
    public DateTime ScheduleDate { get; set; }
    
}