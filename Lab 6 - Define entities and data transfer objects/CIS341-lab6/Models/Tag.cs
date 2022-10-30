using System.ComponentModel.DataAnnotations;
using CIS341_lab6.Data.Entities;

namespace CIS341_lab6.Models;

public class TagModel
{
    [Key] public string TagName { get; set; }
    public List<SharedInformationItem> SharedInformationItems { get; set; }
}