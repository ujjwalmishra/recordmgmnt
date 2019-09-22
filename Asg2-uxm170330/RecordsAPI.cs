using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asg2_uxm170330
{
    class RecordsAPI
    {
        private RecordsAPI()
        {
            dbConn = Database.Instance;
        }

        public static RecordsAPI Instance { get { return Nested.instance; } }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly RecordsAPI instance = new RecordsAPI();
        }

        private static Database dbConn;

        //API methods

        public List<Record> getRecords()
        {
            List<Record> records;
           
            records = dbConn.getRecords();
          
            return records;
        }

        public bool keyExists(string key)
        {
            return dbConn.keyExists(key);
        }
        public Result addRecord(Record record)
        {
            Result result = new Result();
            try
            {
                result  = dbConn.insertRecord(record);
            }
            catch (Exception e) {
                result.success = false;
                result.message = e.Message;
            }
            return result;
        }

        public Result modifyRecord(Record record, string oldKey)
        {
            Result result = new Result();
            try
            {
                result = dbConn.modifyRecord(record, oldKey);
            }
            catch (Exception e)
            {
                result.success = false;
                result.message = e.Message;
            }
            return result;
        }

        public Result deleteRecord(Record record)
        {
            Result result = new Result();
            try
            {
                result = dbConn.deleteRecord(record);
            }
            catch (Exception e)
            {
                result.success = false;
                result.message = e.Message;
            }
            return result;
        }

    }
}
