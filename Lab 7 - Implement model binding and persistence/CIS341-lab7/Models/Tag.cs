using System.ComponentModel.DataAnnotations;

namespace CIS341_lab7.Models;

public class TagModel
{
    [Key] public string TagName { get; set; }
    public List<MyFavoriteItemModel> MaybeMyFavoriteItems { get; set; }
}