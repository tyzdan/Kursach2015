using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmokeControl.Models
{
    public class Solution
    {
        public Solution()
        {
            Parts = new List<Part>();
        }

        [PrimaryKey, AutoIncrement]
        [IgnoreDataMember]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [IgnoreDataMember]
        public bool Synchronized { get; set; }

        [Ignore]
        [IgnoreDataMember]
        public string DateString
        {
            get
            {
                return Date.ToString("dd.mm.yyyy");
            }
        }

        [Ignore]
        public List<Part> Parts { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var i in Parts)
            {
                sb.Append(i.Liquid).Append(' ');
            }

            return sb.ToString();
        }

        [Ignore]
        [IgnoreDataMember]
        public double Strength 
        {
            get
            {
                return Parts.Average(i=>i.Strength);
            }
        }

        [Ignore]
        [IgnoreDataMember]
        public double Volume 
        {
            get
            {
                return Parts.Sum(i => i.Volume);
            }
        }
    }
}
