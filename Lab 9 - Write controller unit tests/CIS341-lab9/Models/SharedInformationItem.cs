using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using CIS341_lab9.Data.Entities;

namespace CIS341_lab9.Models;

public class SharedInformationItemModel
{
    public List<String> Tags { get; set; }
    public SharedInformationItem SharedInformationItem { get; set; }
}

public class SharedInformationItemCreateUpdateModel : SharedInformationItem
{
    [Display(Name = "Tags (comma separated)")]
    public String Tags { get; set; }

    // break polymorphism on purpose >:)
    // https://stackoverflow.com/a/20557095
    public SharedInformationItem DeriveBase()
    {
        return JsonConvert.DeserializeObject<SharedInformationItem>(JsonConvert.SerializeObject(this));
    }

    public static SharedInformationItemCreateUpdateModel FromBase(SharedInformationItem sharedInformationItem)
    {
        List<String> Tags = new List<String>();
        foreach (TaggedInformationItem tag in sharedInformationItem.InformationItemTaggedInformationItems)
        {
            Tags.Add(tag.TagName);
        }

        return new SharedInformationItemCreateUpdateModel
        {
            Id = sharedInformationItem.Id, Title = sharedInformationItem.Title, Details = sharedInformationItem.Details,
            Tags = String.Join(",", Tags)
        };
    }
}