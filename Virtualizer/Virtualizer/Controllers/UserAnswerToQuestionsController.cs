using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Virtualizer.DAL;
using Virtualizer.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Security;

namespace Virtualizer.Controllers
{
    [Authorize]
    public class UserAnswerToQuestionsController : Controller
    {
        private QuestionContentContext db = new QuestionContentContext();
        
       
        // GET: UserAnswerToQuestions
         [Authorize(Roles = "canEdit")]
        public ActionResult Index()
        {
            return View(db.UserAnswersToQuestions.ToList());
        }

        // GET: UserAnswerToQuestions/Details/5
         [Authorize(Roles = "canEdit")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAnswerToQuestion userAnswerToQuestion = db.UserAnswersToQuestions.Find(id);
            if (userAnswerToQuestion == null)
            {
                return HttpNotFound();
            }
            return View(userAnswerToQuestion);
        }

        // GET: UserAnswerToQuestions/Create
         [Authorize(Roles = "canEdit")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAnswerToQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Create([Bind(Include = "UserAnswerID,UserForAnswerID,QuestionForUsersID,UserAnswer,AnswerDate")] UserAnswerToQuestion userAnswerToQuestion)
        {
            string userId;
            
            if (User.Identity.IsAuthenticated)
                   userId = User.Identity.Name;
                
                else
                    userId = "No user identity available.";
                        
            if (ModelState.IsValid)
            {
                userAnswerToQuestion.UserForAnswerID = userId;
                db.UserAnswersToQuestions.Add(userAnswerToQuestion);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userAnswerToQuestion);
        }

        

        // GET: UserAnswerToQuestions/Delete/5
         [Authorize(Roles = "canEdit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAnswerToQuestion userAnswerToQuestion = db.UserAnswersToQuestions.Find(id);
            if (userAnswerToQuestion == null)
            {
                return HttpNotFound();
            }
            return View(userAnswerToQuestion);
        }

        // POST: UserAnswerToQuestions/Delete/5
         [Authorize(Roles = "canEdit")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAnswerToQuestion userAnswerToQuestion = db.UserAnswersToQuestions.Find(id);
            db.UserAnswersToQuestions.Remove(userAnswerToQuestion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
