using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiStudentResultJS.Models;

namespace WebApiStudentResultJS.Controllers
{
    public class ExamTypesController : ApiController
    {
        private SchoolManagementEntities db = new SchoolManagementEntities();

        // GET: api/ExamTypes
        public IQueryable<ExamType> GetExamTypes()
        {
            return db.ExamTypes;
        }

        // GET: api/ExamTypes/5
        [ResponseType(typeof(ExamType))]
        public IHttpActionResult GetExamType(int id)
        {
            ExamType examType = db.ExamTypes.Find(id);
            if (examType == null)
            {
                return NotFound();
            }

            return Ok(examType);
        }

        // PUT: api/ExamTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExamType(int id, ExamType examType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != examType.ExamTypeId)
            {
                return BadRequest();
            }

            db.Entry(examType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ExamTypes
        [ResponseType(typeof(ExamType))]
        public IHttpActionResult PostExamType(ExamType examType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExamTypes.Add(examType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = examType.ExamTypeId }, examType);
        }

        // DELETE: api/ExamTypes/5
        [ResponseType(typeof(ExamType))]
        public IHttpActionResult DeleteExamType(int id)
        {
            ExamType examType = db.ExamTypes.Find(id);
            if (examType == null)
            {
                return NotFound();
            }

            db.ExamTypes.Remove(examType);
            db.SaveChanges();

            return Ok(examType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExamTypeExists(int id)
        {
            return db.ExamTypes.Count(e => e.ExamTypeId == id) > 0;
        }
    }
}