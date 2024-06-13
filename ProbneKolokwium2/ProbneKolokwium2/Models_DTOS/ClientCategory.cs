using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProbneKolokwium2.Models_DTOS;
[Table("clientcategory")]
[PrimaryKey(nameof(IdClientCategory))]
public class ClientCategory
{
    [Key]
    public int IdClientCategory { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int DiscountPerc { get; set; }
}