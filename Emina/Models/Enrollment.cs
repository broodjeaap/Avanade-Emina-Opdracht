using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Emina.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        public int EnqueteID { get; set; }
        public Enquete Enquete { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}