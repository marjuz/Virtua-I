using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Virtualizer.Models;

namespace Virtualizer.Controllers
{
    public class AdminController : Controller
    {
        private AdminForUsersEntities listOfUsers = new AdminForUsersEntities();

        // GET: Admin
         [Authorize(Roles = "canEdit")]
        public ActionResult Index()
        {
            return View();
        }
         // GET: List
        [Authorize(Roles = "canEdit")]
        public ActionResult List()
        {
            var db = listOfUsers.AspNetUsers.ToList();

            return View(db);
        }

                // GET: Admin/Details/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = listOfUsers.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: Admin/Create
         [Authorize(Roles = "canEdit")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "canEdit")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                listOfUsers.AspNetUsers.Add(aspNetUser);
                listOfUsers.SaveChanges();
                return RedirectToAction("List");
            }

            return View(aspNetUser);
        }

        // GET: Admin/Edit/5
         [Authorize(Roles = "canEdit")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = listOfUsers.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "canEdit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                listOfUsers.Entry(aspNetUser).State = EntityState.Modified;
                listOfUsers.SaveChanges();
                return RedirectToAction("List");
            }
            return View(aspNetUser);
        }

        // GET: Admin/Delete/5
         [Authorize(Roles = "canEdit")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = listOfUsers.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: Admin/Delete/5
         [Authorize(Roles = "canEdit")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = listOfUsers.AspNetUsers.Find(id);
            listOfUsers.AspNetUsers.Remove(aspNetUser);
            listOfUsers.SaveChanges();
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                listOfUsers.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
