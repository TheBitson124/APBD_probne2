using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ProbneKolokwium2.Models_DTOS;
[Table("client")]
[PrimaryKey(nameof(IdClient))]
public class Client
{
    [Key]
    public int IdClient { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateTime Birthday { get; set; }
    [Required]
    [MaxLength(100)]
    public string Pesel { get; set; }
    [Required]
    [MaxLength(100)]
    public string email { get; set; }
    [Required]
    public int IdClientCategory { get; set; }
    [ForeignKey(nameof(IdClientCategory))] public ClientCategory ClientCategoryNavigation { get; set; }
    public List<Reservation> ReservationsNavigation { get; set; }

}