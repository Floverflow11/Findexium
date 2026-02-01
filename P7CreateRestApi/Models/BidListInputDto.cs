using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models;

public class BidListInputDto
{
    [Required(ErrorMessage = "Le compte est obligatoire.")]
    public required string Account { get; set; }
    [Required(ErrorMessage = "Le type de l'offre est obligatoire.")]
    public required string BidType { get; set; }
    public double? BidQuantity { get; set; }
}