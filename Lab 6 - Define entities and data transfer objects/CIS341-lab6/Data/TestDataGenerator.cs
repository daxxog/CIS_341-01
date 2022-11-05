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

            string[] tags1 = { "bacon", "meatloaf", "pork", "corned beef" };
            foreach (string tag in tags1)
            {
                TaggedInformationItem taggedInformationItem = new TaggedInformationItem
                {
                    TagName = tag,
                    InformationItemSharedInformationItem = sharedItem1,
                };
                context.Add(taggedInformationItem);
            }

            Favorite favorite = new Favorite
            {
                User = user2,
                InformationItemSharedInformationItem = sharedItem1,
            };
            context.Add(favorite);

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

            SharedInformationItem sharedItem3 = new SharedInformationItem
            {
                Id = 3,
                Title = "Shared information item #3",
                Details =
                    "Bacon ipsum dolor amet turducken chicken shoulder, pork chop rump landjaeger flank t-bone sausage ham. Kevin salami landjaeger shank meatloaf pork loin ham hock sausage flank kielbasa. Ham hock picanha tenderloin, burgdoggen drumstick biltong turkey short ribs andouille hamburger jerky spare ribs. Meatloaf strip steak pastrami fatback tongue jowl pork chop filet mignon leberkas alcatra. Brisket corned beef jowl shankle. Andouille picanha bacon filet mignon, fatback frankfurter beef pancetta meatball meatloaf jerky.",
            };
            context.Add(sharedItem3);

            string[] tags3 = { "pork", "chicken", "meatloaf", "hamburger" };
            foreach (string tag in tags3)
            {
                TaggedInformationItem taggedInformationItem = new TaggedInformationItem
                {
                    TagName = tag,
                    InformationItemSharedInformationItem = sharedItem3,
                };
                context.Add(taggedInformationItem);
            }

            SharedInformationItem sharedItem4 = new SharedInformationItem
            {
                Id = 4,
                Title = "Shared information item #4",
                Details =
                    "Bacon ipsum dolor amet tri-tip beef leberkas, ham hock capicola landjaeger bacon doner. Spare ribs shoulder ribeye ham hock salami tenderloin cupim landjaeger pork belly short ribs bacon. T-bone pork chop frankfurter, andouille corned beef turducken ham ham hock jerky chicken kevin. Shankle capicola biltong chislic. Tail chicken spare ribs, bacon cupim capicola corned beef chuck boudin shoulder ribeye.",
            };
            context.Add(sharedItem4);

            SharedInformationItem sharedItem5 = new SharedInformationItem
            {
                Id = 5,
                Title = "Shared information item #5",
                Details =
                    "Bacon ipsum dolor amet strip steak pork chop buffalo shankle kielbasa venison ham ribeye ham hock tongue capicola. Flank pork belly burgdoggen sirloin jerky. Spare ribs kevin turducken prosciutto capicola t-bone venison. Pork loin fatback tongue strip steak boudin, ribeye biltong leberkas.",
            };
            context.Add(sharedItem5);

            SharedInformationItem sharedItem6 = new SharedInformationItem
            {
                Id = 6,
                Title = "Shared information item #6",
                Details =
                    "Bacon ipsum dolor amet prosciutto spare ribs sirloin pork belly porchetta bresaola pancetta biltong. Turducken tongue drumstick burgdoggen pancetta, short ribs swine biltong strip steak salami sausage tenderloin. Ground round chuck ribeye, sausage shoulder venison filet mignon porchetta frankfurter andouille kielbasa chislic short loin pig. Leberkas shoulder fatback ball tip, venison jowl turkey bresaola. Shankle pork belly turkey beef fatback.",
            };
            context.Add(sharedItem6);

            string[] tags456 = { "pork", "chicken", "meatloaf", "turducken" };
            foreach (string tag in tags456)
            {
                SharedInformationItem[] sharedItems = { sharedItem4, sharedItem5, sharedItem6 };
                foreach (SharedInformationItem sharedItem in sharedItems)
                {
                    TaggedInformationItem taggedInformationItem = new TaggedInformationItem
                    {
                        TagName = tag,
                        InformationItemSharedInformationItem = sharedItem,
                    };
                    context.Add(taggedInformationItem);
                }
            }

            SharedInformationItem sharedItem7 = new SharedInformationItem
            {
                Id = 7,
                Title = "Shared information item #7",
                Details =
                    "Bacon ipsum dolor amet pork hamburger strip steak, pastrami turkey porchetta leberkas capicola bacon. Pig pastrami landjaeger corned beef ball tip cupim, chislic kielbasa cow. Beef ribs cow buffalo turducken, burgdoggen frankfurter short ribs rump kevin ribeye capicola turkey. Burgdoggen beef kielbasa chicken, fatback strip steak filet mignon pork chop turkey andouille porchetta kevin. Ham beef turducken flank, frankfurter jowl pork chop cow drumstick boudin beef ribs tenderloin shankle. Brisket landjaeger cupim beef ribs doner leberkas ribeye drumstick sausage pancetta fatback venison.",
            };
            context.Add(sharedItem7);

            string[] tags7 = { "pork", "corned beef", "turducken", "bacon" };
            foreach (string tag in tags7)
            {
                TaggedInformationItem taggedInformationItem = new TaggedInformationItem
                {
                    TagName = tag,
                    InformationItemSharedInformationItem = sharedItem7,
                };
                context.Add(taggedInformationItem);
            }

            context.SaveChanges();
        }
    }
}