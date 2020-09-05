using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApiStudentResultJS.Models;

namespace WebApiStudentResultJS.Controllers
{
    public class ReportResultsController : Controller
    {
        private SchoolManagementEntities db = new SchoolManagementEntities();

        // GET: ReportResults
        public ActionResult Index()
        {
            return View(db.ReportResults.ToList());
        }

        // GET: ReportResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportResult reportResult = db.ReportResults.Find(id);
            if (reportResult == null)
            {
                return HttpNotFound();
            }
            return View(reportResult);
        }

        // GET: ReportResults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportResultId,StudentId,CourseId,ExamTypeId,Marks")] ReportResult reportResult)
        {
            if (ModelState.IsValid)
            {
                db.ReportResults.Add(reportResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reportResult);
        }

        // GET: ReportResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportResult reportResult = db.ReportResults.Find(id);
            if (reportResult == null)
            {
                return HttpNotFound();
            }
            return View(reportResult);
        }

        // POST: ReportResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportResultId,StudentId,CourseId,ExamTypeId,Marks")] ReportResult reportResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reportResult);
        }

        // GET: ReportResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportResult reportResult = db.ReportResults.Find(id);
            if (reportResult == null)
            {
                return HttpNotFound();
            }
            return View(reportResult);
        }

        // POST: ReportResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportResult reportResult = db.ReportResults.Find(id);
            db.ReportResults.Remove(reportResult);
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
