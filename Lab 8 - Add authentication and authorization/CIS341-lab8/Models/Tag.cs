using System.ComponentModel.DataAnnotations;

namespace CIS341_lab8.Models;

public class TagModel
{
    [Key] public string TagName { get; set; }
    public List<MyFavoriteItemModel> MaybeMyFavoriteItems { get; set; }
}