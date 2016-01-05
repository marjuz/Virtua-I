using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Virtualizer.Models
{
    public class QuestionForUsers
    {
        [Key]
        public int QuestionID { get; set; }
        public string UserForQuestion { get; set; }
        public string Question { get; set; }
        public string Comment { get; set; }
        [DataType(DataType.Date)]
        public DateTime QuestionDate { get; set; }

        public virtual ICollection<PossibleAnswerForQuestion> PossibleAnswerForQuestion { get; set; }
        public virtual ICollection<UserAnswerToQuestion> UserAnswerToQuestion { get; set; }
    }
}