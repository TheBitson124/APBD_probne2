using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProbneKolokwium2.Models_DTOS;

[Table("reservation")]
[PrimaryKey(nameof(IdReservation))]
public class Reservation
{
    [Key]
    public int IdReservation { get; set; }
    [Required]
    public int IdClient { get; set; }
    [Required]
    public DateTime DateFrom { get; set; }
    [Required]
    public DateTime DateTo { get; set; }
    [Required]
    public int IdBoatStandard { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    public int NumOfBoats { get; set; }
    [Required]
    public bool Fullfilled { get; set; }
    
    
    public double Price { get; set; }
    
    public string? CancelReservation { get; set; }
    [ForeignKey(nameof(IdClient))] public Client IdClientNavigation { get; set; }
    [ForeignKey(nameof(IdBoatStandard))] public BoatStandard IdBoatStandardNavigation { get; set; }
    
    public List<SailboatReservation> Reservation_SailboatReservation { get; set; } = new List<SailboatReservation>();
}