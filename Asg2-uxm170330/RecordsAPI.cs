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
        }

        public static RecordsAPI Instance { get { return Nested.instance; } }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly RecordsAPI instance = new RecordsAPI();
        }

        private static Database dbConn = Database.Instance;

        //API methods
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

    }
}
