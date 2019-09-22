using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asg2_uxm170330
{
    public class Record : ICloneable
    {

        public string fName { get; set; }
        public string mName { get; set; }
        public string lName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool proof { get; set; }
        public DateTime time { get; set; }
        public DateTime firstTime { get; set; }
        public DateTime saveTime { get; set; }
        public int backSpaceCount { get; set; } = 0;

        public Record(string[] fields) {

            fName = fields[0];
            mName = fields[1];
            lName = fields[2];
            this.address1 = fields[3];
            this.address2 = fields[4];
            this.city = fields[5];
            this.state = fields[6];
            this.zip = Int32.Parse(fields[7]);
            this.gender = fields[8];
            this.phone = fields[9];
            this.email = fields[10];
            this.proof = Boolean.Parse(fields[11]);
            this.time = DateTime.Parse(fields[12]);
            this.firstTime = DateTime.Parse(fields[13]);
            this.saveTime = DateTime.Parse(fields[14]);
            this.backSpaceCount = Int32.Parse(fields[15]);

        }
        public Record(string fname, string mname, string lname, string address1, string address2, string city,
            string state, int zip, string gender, string phone, string email, bool proof, DateTime time, DateTime ftime,
            DateTime stime, int counts) {

            fName = fname;
            mName = mname;
            lName = lname;
            this.address1 = address1;
            this.address2 = address2;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.gender = gender;
            this.phone = phone;
            this.email = email;
            this.proof = proof;
            this.time = time;
            this.firstTime = ftime;
            this.saveTime = stime;
            this.backSpaceCount = counts;
            
        }

        public string getSerialized() {
            var record = this;
            string[] fields = new string[16];
            fields[0] = record.fName;
            fields[1] = record.mName;
            fields[2] = record.lName;
            fields[3] = record.address1;
            fields[4] = record.address2;
            fields[5] = record.city;
            fields[6] = record.state;
            fields[7] = record.zip.ToString();
            fields[8] = record.gender;
            fields[9] = record.phone;
            fields[10] = record.email;
            fields[11] = record.proof.ToString();
            fields[12] = record.time.ToString();
            fields[13] = record.firstTime.ToString();
            fields[14] = record.saveTime.ToString();
            fields[15] = record.backSpaceCount.ToString();

            string serializedRecord = String.Join("\t", fields);

            return serializedRecord;
        }

    public virtual object Clone() {
            var result = this.MemberwiseClone();
            return result;
        }

    }
}
