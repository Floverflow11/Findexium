using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models;

public class CurvePointInputDto
{
    [Required(ErrorMessage = "Ne doit pas être vide.")]
    public required byte CurveId { get; set; }
    public double? Term { get; set; }
    public double? CurvePointValue { get; set; }
}