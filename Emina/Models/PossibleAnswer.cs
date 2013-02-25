using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Emina.Models
{
    public class PossibleAnswer
    {
        public int PossibleAnswerID { get; set; }

        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }
        public string Text { get; set; }

        public int NextQuestionID { get; set; }
        public virtual Question NextQuestion { get; set; }
    }
}