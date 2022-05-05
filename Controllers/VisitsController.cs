using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using WebApp_c.Models;

namespace WebApp_c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private readonly VisitsContext _context;

        public VisitsController(VisitsContext context) => _context = context;

        // Get: /api/visits
        [HttpGet]
        public ActionResult<IEnumerable<Visit>> GetVisits()
        {
            return _context.Visits;
        }


        // Get: /api/visits/n
        [HttpGet("{id}")]
        public ActionResult<Visit> GetVisitItem(int id)
        {
            var visit = _context.Visits.Find(id);
            if (visit == null) return NotFound();
            return visit;
        }

        // Post: /api/visits
        /*
        _payload_ 
        {
            "visitDate": "1994-03-06T03:00:00",
            "visitPlace": "Country 2, City n1, some street 45",
            "diagnosis": "COVID_92",
            "recommendation": "",
            "medicine": 1,
            "doctor": 1,
            "patient": 1
        }
        */

        [HttpPost]
        public ActionResult<Visit> PostVisit(Visit visit)
        {
            _context.Visits.Add(visit);
            _context.SaveChanges();
            return CreatedAtAction("GetVisit", new Visit { Id = visit.Id }, visit);
        }

        // Put: /api/visits/n
        [HttpPut("{id}")]
        public ActionResult<string> PutVisit(int id, Visit visit)
        {
            if (id != visit.Id)
            {
                return BadRequest();
            }

            _context.Entry(visit).State = EntityState.Modified;
            _context.SaveChanges();

            return "success";
        }

        // Delete: /api/visits/n
        [HttpDelete("{id}")]
        public ActionResult<Visit> DeleteVisit(int id)
        {
            var visit = _context.Visits.Find(id);
            if (visit == null) return NotFound();
            _context.Visits.Remove(visit);
            _context.SaveChanges();

            return visit;
        }
    }
}