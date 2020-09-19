using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApiStudentResultJS.Models;

namespace WebApiStudentResultJS.Controllers
{
    public class Results1Controller : Controller
    {
        private SchoolManagementEntities db = new SchoolManagementEntities();

        // GET: Results1
        public async Task<ActionResult> Index()
        {
            var results = db.Results.Include(r => r.ExamType).Include(r => r.Student);
            return View(await results.ToListAsync());
        }

        // GET: Results1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = await db.Results.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Results1/Create
        public ActionResult Create()
        {
            ViewBag.ExamTypeId = new SelectList(db.ExamTypes, "ExamTypeId", "ExamName");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName");
            return View();
        }

        // POST: Results1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ResultId,StudentId,ExamTypeId,CGPA")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ExamTypeId = new SelectList(db.ExamTypes, "ExamTypeId", "ExamName", result.ExamTypeId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", result.StudentId);
            return View(result);
        }

        // GET: Results1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = await db.Results.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamTypeId = new SelectList(db.ExamTypes, "ExamTypeId", "ExamName", result.ExamTypeId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", result.StudentId);
            return View(result);
        }

        // POST: Results1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ResultId,StudentId,ExamTypeId,CGPA")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ExamTypeId = new SelectList(db.ExamTypes, "ExamTypeId", "ExamName", result.ExamTypeId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentName", result.StudentId);
            return View(result);
        }

        // GET: Results1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = await db.Results.FindAsync(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Results1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Result result = await db.Results.FindAsync(id);
            db.Results.Remove(result);
            await db.SaveChangesAsync();
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
