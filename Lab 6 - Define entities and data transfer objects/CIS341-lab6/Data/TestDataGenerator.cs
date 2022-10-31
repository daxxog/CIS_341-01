using CIS341_lab6.Data.Entities;

namespace CIS341_lab6.Data
{
    public class TestDataGenerator
    {
        public TestDataGenerator()
        {
        }

        public void generate(SqliteContext context)
        {
            User user1 = new User
            {
                Id = 1,
                Email = "user1@example.com",
                PasswordHash = "<something>",
                ContentManager = true,
            };
            User user2 = new User
            {
                Id = 2,
                Email = "user2@example.com",
                PasswordHash = "<something>",
                ContentManager = false,
            };
            context.Add(user1);
            context.Add(user2);

            UserInformationItem item = new UserInformationItem
            {
                Id = 1,
                User = user2,
                Title = "Example information item #1",
                Details =
                    "Bacon ipsum dolor amet pastrami sausage short loin, cow beef short ribs pork chop burgdoggen leberkas biltong andouille. Brisket turducken venison ham. Ground round shoulder shankle tongue short ribs. Drumstick t-bone pig sausage pastrami beef ribs chuck beef.",
            };
            context.Add(item);

            SharedInformationItem sharedItem1 = new SharedInformationItem
            {
                Id = 1,
                Title = "Shared information item #1",
                Details =
                    "Bacon ipsum dolor amet meatball tenderloin cupim, ham pork meatloaf drumstick. Leberkas bacon jowl beef ribs beef corned beef ham hock fatback kielbasa andouille strip steak turkey ribeye filet mignon. Corned beef ham hock salami, jerky jowl beef cupim landjaeger pork short loin doner. Chicken pancetta porchetta shankle hamburger burgdoggen, prosciutto landjaeger jerky alcatra strip steak bresaola. Pancetta landjaeger cupim beef salami. Short ribs turducken sirloin rump pig biltong short loin hamburger beef ribs porchetta burgdoggen jowl turkey.",
            };
            context.Add(sharedItem1);

            string[] tags1 = { "bacon", "meatloaf", "pork" };
            foreach (string tag in tags1)
            {
                TaggedInformationItem taggedInformationItem = new TaggedInformationItem
                {
                    TagName = tag,
                    InformationItemSharedInformationItem = sharedItem1,
                };
                context.Add(taggedInformationItem);
            }

            SharedInformationItem sharedItem2 = new SharedInformationItem
            {
                Id = 2,
                Title = "Shared information item #2",
                Details =
                    "Bacon ipsum dolor amet corned beef sausage meatloaf strip steak chislic, tenderloin kevin sirloin kielbasa andouille buffalo. Picanha meatball short ribs pork belly pig turducken corned beef frankfurter biltong ribeye. Chuck frankfurter pork loin brisket strip steak. Frankfurter porchetta meatloaf flank andouille short ribs pastrami pork belly corned beef chicken tenderloin buffalo.",
            };
            context.Add(sharedItem2);

            string[] tags2 = { "chicken", "meatloaf" };
            foreach (string tag in tags2)
            {
                TaggedInformationItem taggedInformationItem = new TaggedInformationItem
                {
                    TagName = tag,
                    InformationItemSharedInformationItem = sharedItem2,
                };
                context.Add(taggedInformationItem);
            }

            Favorite favorite = new Favorite
            {
                User = user2,
                InformationItemSharedInformationItem = sharedItem1,
            };
            context.Add(favorite);

            context.SaveChanges();
        }
    }
}