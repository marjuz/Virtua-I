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

namespace Virtualizer.Controllers
{

    public class QuestionForUsersController : Controller
    {
        private QuestionContentContext db = new QuestionContentContext();

        // GET: QuestionForUsers
        [Authorize(Roles = "canEdit")]
        public ActionResult Index()
        {
            return View(db.QuestionsForUsers.ToList());
        }

        // GET: QuestionForUsers/Details/5
         [Authorize(Roles = "canEdit")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionForUsers questionForUsers = db.QuestionsForUsers.Find(id);
            if (questionForUsers == null)
            {
                return HttpNotFound();
            }
            return View(questionForUsers);
        }

        // GET: QuestionForUsers/Create
         [Authorize(Roles = "canEdit")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionForUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Create([Bind(Include = "QuestionId,UserForQuestion,Question,Comment,QuestionDate")] QuestionForUsers questionForUsers)
        {
            if (ModelState.IsValid)
            {
                questionForUsers.UserForQuestion = User.Identity.Name;
                questionForUsers.QuestionDate = DateTime.Now;
                db.QuestionsForUsers.Add(questionForUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionForUsers);
        }

        // GET: QuestionForUsers/Edit/5
         [Authorize(Roles = "canEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionForUsers questionForUsers = db.QuestionsForUsers.Find(id);
            if (questionForUsers == null)
            {
                return HttpNotFound();
            }
            return View(questionForUsers);
        }

        // POST: QuestionForUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit([Bind(Include = "QuestionId,UserForQuestion,Question,Comment,QuestionDate")] QuestionForUsers questionForUsers)
        {
            if (ModelState.IsValid)
            {

                db.Entry(questionForUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionForUsers);
        }

        // GET: QuestionForUsers/Delete/5
         [Authorize(Roles = "canEdit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionForUsers questionForUsers = db.QuestionsForUsers.Find(id);
            if (questionForUsers == null)
            {
                return HttpNotFound();
            }
            return View(questionForUsers);
        }

        // POST: QuestionForUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionForUsers questionForUsers = db.QuestionsForUsers.Find(id);
            db.QuestionsForUsers.Remove(questionForUsers);
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
