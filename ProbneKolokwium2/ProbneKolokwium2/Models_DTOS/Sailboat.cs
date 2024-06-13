using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;

namespace ProbneKolokwium2.Models_DTOS;
[Table("boat")]
[PrimaryKey(nameof(IdSailboat))]
public class Sailboat
{
    [Key] 
    public int IdSailboat { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    [MaxLength(100)] 
    public string Description { get; set; }
    [Required]
    public int IdBoatStandard { get; set; }
    
    [ForeignKey(nameof(IdBoatStandard))] public BoatStandard IdBoatStandardNavigation { get; set; }
    
    [Required]
    public Double Price { get; set; }

    [Required] 
    public List<SailboatReservation> Sailboat_SailboatReservations { get; set; } = new List<SailboatReservation>();
}