using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asg2_uxm170330
{
    public class Record
    {

        public string fName { get; set; }
        private string mName { get; set; }
        public string lName { get; set; }
        private string address1 { get; set; }
        private string address2 { get; set; }
        private string city { get; set; }
        private string state { get; set; }
        private int zip { get; set; }
        private string gender { get; set; }
        public string phone { get; set; }
        private string email { get; set; }
        private bool proof { get; set; }
        public DateTime time { get; set; }

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

        }
        public Record(string fname, string mname, string lname, string address1, string address2, string city,
            string state, int zip, string gender, string phone, string email, bool proof, DateTime time) {

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
            
        }

        public string getSerialized() {
            var record = this;
            string[] fields = new string[12];
            fields.Append(record.fName);
            fields.Append(record.mName);
            fields.Append(record.lName);
            fields.Append(record.address1);
            fields.Append(record.address2);
            fields.Append(record.city);
            fields.Append(record.state);
            fields.Append(record.zip.ToString());
            fields.Append(record.gender);
            fields.Append(record.phone);
            fields.Append(record.email);
            fields.Append(record.proof.ToString());
            fields.Append(record.time.ToString());

            return String.Join("\t", fields);
        }

    }
}
