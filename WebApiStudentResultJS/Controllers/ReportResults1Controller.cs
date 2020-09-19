using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiStudentResultJS.Models;

namespace WebApiStudentResultJS.Controllers
{
    public class ReportResults1Controller : ApiController
    {
        private SchoolManagementEntities db = new SchoolManagementEntities();

        // GET: api/ReportResults1
        public IQueryable<ReportResult> GetReportResults()
        {
            return db.ReportResults;
        }

        // GET: api/ReportResults1/5
        [ResponseType(typeof(ReportResult))]
        public async Task<IHttpActionResult> GetReportResult(int id)
        {
            ReportResult reportResult = await db.ReportResults.FindAsync(id);
            if (reportResult == null)
            {
                return NotFound();
            }

            return Ok(reportResult);
        }

        // PUT: api/ReportResults1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReportResult(int id, ReportResult reportResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reportResult.ReportResultId)
            {
                return BadRequest();
            }

            db.Entry(reportResult).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportResultExists(id))
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

        // POST: api/ReportResults1
        [ResponseType(typeof(ReportResult))]
        public async Task<IHttpActionResult> PostReportResult(ReportResult reportResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReportResults.Add(reportResult);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReportResultExists(reportResult.ReportResultId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reportResult.ReportResultId }, reportResult);
        }

        // DELETE: api/ReportResults1/5
        [ResponseType(typeof(ReportResult))]
        public async Task<IHttpActionResult> DeleteReportResult(int id)
        {
            ReportResult reportResult = await db.ReportResults.FindAsync(id);
            if (reportResult == null)
            {
                return NotFound();
            }

            db.ReportResults.Remove(reportResult);
            await db.SaveChangesAsync();

            return Ok(reportResult);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReportResultExists(int id)
        {
            return db.ReportResults.Count(e => e.ReportResultId == id) > 0;
        }
    }
}