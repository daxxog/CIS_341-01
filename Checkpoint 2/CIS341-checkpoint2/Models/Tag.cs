using System.ComponentModel.DataAnnotations;

namespace CIS341_checkpoint2.Models;

public class TagModel
{
    [Key] public string TagName { get; set; }
    public List<MyFavoriteItemModel> MaybeMyFavoriteItems { get; set; }
}