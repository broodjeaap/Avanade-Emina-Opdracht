using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emina.Models
{
    public class Enquete
    {
        public Enquete()
        {
            Questions = new List<Question>();
        }

        public int EnqueteID { get; set; }

        [Required(ErrorMessage = "Enquete name is required.")]
        [Display(Name = "Enquete Name")]
        [MaxLength(50, ErrorMessage = "Enquete name can not be longer than 50 letters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enquete description is required.")]
        [Display(Name = "Enquete Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date name is required.")]
        [Display(Name = "Date when the enquete becomes available")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Date when the enquete becomes unavailable")]
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}