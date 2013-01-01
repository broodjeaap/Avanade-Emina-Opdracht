using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emina.Models
{
    public enum QuestionType
    {
        Open, 
        MultipleChoice,
        Checkbox,
        Stars,
        Grade,
        Binary,
        FuzzyBinary
    }
}