using System.ComponentModel.DataAnnotations;
using CIS341_lab5.Data.Entities;

namespace CIS341_lab5.Models;

public class TagModel
{
    [Key] public string TagName { get; set; }
    public List<SharedInformationItem> SharedInformationItems { get; set; }
}