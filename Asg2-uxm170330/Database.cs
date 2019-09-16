using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asg2_uxm170330
{
    class Database
    {

        private Dictionary<string, Record> dbMap = new Dictionary<string, Record>();
        private Database()
        {
            this.checkDbFile();
            this.loadDatabase();
        }

        public static Database Instance { get { return Nested.instance; } }
        
        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly Database instance = new Database();
        }

        private void checkDbFile()
        {

            if (!File.Exists("CS6326Asg2.txt")) {
                Console.WriteLine("File doesn't exist creating one");
                //Creating file
                try
                {
                    using (StreamWriter sw = File.CreateText("CS6326Asg2.txt")) { }
                    //File.CreateText("CS6326Asg2.txt");
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    Console.WriteLine("--------------Exising program---------------");
                    System.Environment.Exit(1);
                }


            }
        }

        private void loadDatabase()
        {
            using (StreamReader sr = File.OpenText("CS6326Asg2.txt"))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Record record = deserializeRecord(s);
                    Result result = insertRecord(record);
                    Console.WriteLine(result.message);
                }
            }

        }

        private Record deserializeRecord(string recordStr)
        {
            string[] fields = recordStr.Split('\t');
            Record record = new Record(fields);
            return record;
        }

        public Result insertRecord(Record record)
        {
            Result result = new Result();
            try
            {
                string key = String.Concat(record.fName, record.lName, record.phone);
                dbMap.Add(key, record);
                result.success = true;
                result.message = "Record Inserterd Successfully";
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                result.success = false;
                result.message = e.Message;
            }
            return result;
        }

        private Result writeToDatabase()
        {
            Result result = new Result();
            try
            {
                using (StreamWriter sw = File.CreateText("CS6326Asg2.txt"))
                {
                    foreach (var item in dbMap.OrderBy(i => i.Value.time))
                    {
                        string serializeRecord = item.Value.getSerialized();
                        sw.WriteLine(serializeRecord);
                    }
                    result.success = true;
                    result.message = "Database Updated";
                }
            }
            catch(Exception e)
            {
                result.success = false;
                result.message = e.Message;
            }

            return result;

        }

    }


}
