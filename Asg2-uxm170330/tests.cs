using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asg2_uxm170330
{
    class tests
    {
        static Database db;
        static RecordsAPI api = RecordsAPI.Instance;
        public static void run() {
            //testDatabase();
            insertRecord();
        }

        public static void testDatabase() {
            //db = Database.Instance;
        }


        public static void insertRecord() {
            Record record = new Record("John","", "Cole", "734 wedd", "Dallas", "Dallas", "TX", 1234, "Male", "34343243", "jcole@gmail.com",
                true, new DateTime());
            Result result = api.addRecord(record);
            Record record1 = new Record("John", "", "Cole", "734 wedd", "Dallas", "Dallas", "TX", 1234, "Male", "334343243", "jcole@gmail.com",
    true, new DateTime());
            Result result1 = api.addRecord(record1);
            Console.WriteLine(result.message);
            Console.WriteLine(result1.message);

            Record record2 = new Record("John", "", "Cole", "734 sdsfsds wedd", "Dallas", "Dallas", "TX", 1234, "Male", "334343243", "jcole@gmail.com",
true, new DateTime());
            Result result2 = api.modifyRecord(record2);
            Console.WriteLine(result2.message);
            Result result3 = api.deleteRecord(record);
        }
    }
}
