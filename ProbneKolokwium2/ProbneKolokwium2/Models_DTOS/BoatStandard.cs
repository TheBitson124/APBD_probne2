using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ProbneKolokwium2.Models_DTOS;
[Table("boatstandard")]
[PrimaryKey(nameof(IdBoatStandard))]
public class BoatStandard
{
    [Key] 
    public int IdBoatStandard { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int level { get; set; }
}