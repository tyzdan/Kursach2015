using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmokeControl.Models
{
    public class Part
    {
        [PrimaryKey, AutoIncrement]
        [IgnoreDataMember]
        public int Id { get; set; }

        public string Liquid { get; set; }
        
        public double Strength { get; set; }
        
        public double Volume { get; set; }
    }
}
