using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAPIAPP.Models
{
    public class Student
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int RollNo { get; set; }
        public int Malayalam { get; set; }
        public int Hindi { get; set; }
        public int English { get; set; }
        public int Total { get; set; }
        public int Average { get; set; }

        public DateTime CreatedTime { get; set; }
       
      




    }
}