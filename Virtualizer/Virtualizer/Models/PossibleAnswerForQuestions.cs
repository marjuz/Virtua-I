using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Virtualizer.Models
{
    public enum AnswerType
    {
        Open, RadioButton, Closed, Date, List, Arrey
    }


    public class PossibleAnswerForQuestion
    {        
        [Key]
        public int PAnswerID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public AnswerType? AnswerType { get; set; }

        public DateTime AnswerDate { get; set; }

        public virtual QuestionForUsers QuestionForUsers { get; set; }
        
    }
}