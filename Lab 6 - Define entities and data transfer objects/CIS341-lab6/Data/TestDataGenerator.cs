using CIS341_lab6.Data.Entities;

namespace CIS341_lab6.Data
{
    public class TestDataGenerator
    {
        private readonly SqliteContext _context;

        public TestDataGenerator(SqliteContext context)
        {
            _context = context;
        }

        public void generate()
        {
            // do entity generation here
        }
    }
}