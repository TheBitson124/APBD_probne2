using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProbneKolokwium2.Models_DTOS;
[Table("sailboatreservation")]
[PrimaryKey(nameof(IdSailboat),nameof(IdReservation))]
public class SailboatReservation
{
    public int IdSailboat { get; set; }
    public int IdReservation { get; set; }
    [ForeignKey(nameof(IdSailboat))] public Sailboat IdSailboatNavigation { get; set; }
    [ForeignKey(nameof(IdReservation))] public Reservation IdReservationNavigation { get; set; }

}