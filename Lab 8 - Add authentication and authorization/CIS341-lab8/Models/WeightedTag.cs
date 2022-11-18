using System.ComponentModel.DataAnnotations;

namespace CIS341_lab8.Models;

public class WeightedTagModel
{
    [Key] public string TagName { get; set; }
    public int Weight { get; set; } // 1 - 6 ( used to generate h tags )
}