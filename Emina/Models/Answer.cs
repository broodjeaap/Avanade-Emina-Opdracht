using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Emina.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int EnqueteID { get; set; }
        public virtual Enquete Enquete { get; set; }
        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }
        public int PossibleAnswerID { get; set; }
        public virtual PossibleAnswer PossibleAnswer { get; set; }
    }
}