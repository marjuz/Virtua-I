using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Virtualizer.Models
{
    public class UserAnswerToQuestion
    {
        [Key]
        public int UserAnswerID { get; set; }
        public string UserForAnswerID { get; set; }
        public int QuestionID { get; set; }
        public string UserAnswer { get; set; }
        // [UIHint("LoudDateTime")]
       
        public DateTime AnswerDate { get; set; }

        public virtual QuestionForUsers QuestionForUsers { get; set; }
    }
}