using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
using Virtualizer.Models;
using Virtualizer.DAL;

namespace Virtualizer.Controllers
{  
    
    [Authorize]
    public class CmController : Controller
    {
        
        private QuestionContentContext questions = new QuestionContentContext();

        public Virtualizer.Models.UserAnswerToQuestion UserAnswerToQuestion
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        

        // GET: Cm
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.Name;
            int a = 0;
            // questions to answer
            Random rnd = new Random();
            var sendingAnswer = questions.UserAnswersToQuestions;
            var qlist = questions.QuestionsForUsers.ToList();
            var selectedQuestion = qlist[rnd.Next(qlist.Count)];
            
            if (selectedQuestion == null)
            {
                return HttpNotFound();
            }

            // questions to create
            bool questionExists = questions.QuestionsForUsers.Any(o => o.UserForQuestion.Equals(userId) && o.QuestionDate.Equals(DateTime.Today));
            if (questionExists==false) 
            {
                a = 1;

            }
            else
            {
                a = 0;

            }

                ViewBag.LinkableId = a;
          

            return View(selectedQuestion);
     
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Index(QuestionForUsers QuestionsData, string reportName)
        {
            string userId;

            if (User.Identity.IsAuthenticated)
                userId = User.Identity.Name;

            else
                userId = "No user identity available.";
                        
            if (ModelState.IsValid)
            {

                bool answerExists = questions.UserAnswersToQuestions.Any(o => o.UserForAnswerID.Equals(userId) && o.QuestionID.Equals(QuestionsData.QuestionID));
                var sendAnswer = new UserAnswerToQuestion();

                if (answerExists)
                {
                    sendAnswer = questions.UserAnswersToQuestions
                                                        .Where(o => o.UserForAnswerID.Equals(userId) && o.QuestionID.Equals(QuestionsData.QuestionID))
                                                        .Single();
                    sendAnswer.UserAnswer = reportName;
                    sendAnswer.AnswerDate = DateTime.Now;
                    questions.Entry(sendAnswer).State = EntityState.Modified;

                   
                }
                   else
                    {
                        
                        sendAnswer.UserForAnswerID = userId; 
                        sendAnswer.QuestionID = QuestionsData.QuestionID;
                        sendAnswer.UserAnswer = reportName;
                        sendAnswer.AnswerDate = DateTime.Now;
                        questions.UserAnswersToQuestions.Add(sendAnswer); 
                }
                questions.SaveChanges();
                return RedirectToAction("Index");
               
            }

            return View(QuestionsData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult QuestionAnswered(QuestionForUsers QuestionsData, string chosenType, string minVal, string maxVal)
        {
            string userId;

            if (User.Identity.IsAuthenticated)
                userId = User.Identity.Name;

            else
                userId = "No user identity available.";

            if (ModelState.IsValid)
            {

                bool HaveYouAQuestion = questions.QuestionsForUsers.Any(o => o.QuestionDate.Equals(DateTime.Now) && o.UserForQuestion.Equals(userId));
                bool IsQuestionUnique = questions.QuestionsForUsers.Any(o => o.Question.Equals(QuestionsData.Question));
                var PossibleQ = new PossibleAnswerForQuestion();
                var PossibleQ2 = new PossibleAnswerForQuestion();
                var newQuestion = new QuestionForUsers();

                newQuestion = questions.QuestionsForUsers
                                                       .Where(o => o.QuestionID.Equals(QuestionsData.QuestionID))
                                                       .Single();

                if (HaveYouAQuestion)
                {
                    return RedirectToAction("Index");

                }
                else if (IsQuestionUnique)
                {
                    return RedirectToAction("Index");

                }
                else if (chosenType == null)
                {
                    ViewBag.Message = "Fill all fields, Please";
                    return View("Question", QuestionsData);
                }
                else if (chosenType == "Arrey" && (minVal==null||maxVal==null))
                {
                    ViewBag.Message = "Fill all fields, Please";
                    return View("Question", QuestionsData);

                }
                else if (chosenType == "Arrey" && (minVal != null || maxVal != null))  
                {
                    int i = Convert.ToInt32(minVal);
                    int r = Convert.ToInt32(maxVal);

                    if (i > r) 
                    {
                        string p = "";
                        p = maxVal;
                        maxVal = minVal;
                        minVal = p;   
                    }
                    
                    PossibleQ.QuestionID = QuestionsData.QuestionID;
                    PossibleQ.AnswerType = ((AnswerType)Enum.Parse(typeof(AnswerType), chosenType, true));
                    PossibleQ.Answer = minVal;
                    PossibleQ.AnswerDate = DateTime.Today;
                    questions.PossibleAnswerForQuestions.Add(PossibleQ);
                    questions.SaveChanges();
                   
                    PossibleQ2.QuestionID = QuestionsData.QuestionID;
                    PossibleQ2.AnswerType = ((AnswerType)Enum.Parse(typeof(AnswerType), chosenType, true));
                    PossibleQ2.Answer = maxVal;
                    PossibleQ2.AnswerDate = DateTime.Today;
                    questions.PossibleAnswerForQuestions.Add(PossibleQ2);
                    questions.SaveChanges();

                    newQuestion.Comment = QuestionsData.Comment;
                    questions.Entry(newQuestion).State = EntityState.Modified;
                    questions.SaveChanges();

                } else
                {

                    PossibleQ.QuestionID = QuestionsData.QuestionID;
                    PossibleQ.AnswerType = ((AnswerType)Enum.Parse(typeof(AnswerType), chosenType, true));
                    PossibleQ.Answer = "";
                    PossibleQ.AnswerDate = DateTime.Today;

                    newQuestion.Comment = QuestionsData.Comment;

                    questions.PossibleAnswerForQuestions.Add(PossibleQ);
                    questions.Entry(newQuestion).State = EntityState.Modified;
                    questions.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");            
        }



        [Authorize]
         public ActionResult Question(QuestionForUsers QuestionsData, string questionName)
        {
            string userId;

            if (User.Identity.IsAuthenticated)
                userId = User.Identity.Name;

            else
                userId = "No user identity available.";

            if (ModelState.IsValid)
            {

                bool questionExists = questions.QuestionsForUsers.Any(o => o.QuestionDate.Equals(DateTime.Now) && o.UserForQuestion.Equals(userId));
                bool IsQuestionUnique = questions.QuestionsForUsers.Any(o => o.Question.Equals(questionName));
                var newQuestion = new QuestionForUsers();
                 var transitQuestion = new QuestionForUsers();

                if (questionExists)
                {
                 
                    return View("Index");

                }
                else if (IsQuestionUnique)
                {
                   ViewBag.QuestionError = "Question already exists";
                    return RedirectToAction("Index");

                }else
                {
                   
                    newQuestion.UserForQuestion = userId;
                    newQuestion.Question = questionName;
                    newQuestion.QuestionDate = DateTime.Today;
                    questions.QuestionsForUsers.Add(newQuestion);
                    questions.SaveChanges();

                    transitQuestion = questions.QuestionsForUsers
                                                       .Where(o => o.Question.Equals(questionName))
                                                       .Single();

                    return View("Question", transitQuestion);

                }
            }
            return RedirectToAction("Index");
        }

        // GET: List
        [Authorize]
        public ActionResult List()
        {
            string userId = User.Identity.Name;
            var ListOfQuestionsAnswered = questions.UserAnswersToQuestions.Where(x => x.UserForAnswerID == userId).ToList();



            return View(ListOfQuestionsAnswered);


        }
  
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAnswerToQuestion answer = questions.UserAnswersToQuestions.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Cm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAnswerToQuestion answer = questions.UserAnswersToQuestions.Find(id);
            questions.UserAnswersToQuestions.Remove(answer);
            questions.SaveChanges();
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                questions.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
