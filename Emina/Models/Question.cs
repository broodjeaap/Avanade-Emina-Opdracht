using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emina.Models
{
    public class Question
    {
        public Question()
        {
            PossibleAnswers = new List<PossibleAnswer>();
        }

        public int QuestionID { get; set; }
        public int QuestionNumber { get; set; }
        public int EnqueteID { get; set; }
        public virtual Enquete Enquete { get; set; }

        [Required(ErrorMessage = "Question Text is required")]
        [Display(Name = "Question Text")]
        public string Text { get; set; }

        public QuestionType Type { get; set; }
        public virtual Question NextQuestion { get; set; }
        public virtual ICollection<PossibleAnswer> PossibleAnswers { get; set; }
    }
}