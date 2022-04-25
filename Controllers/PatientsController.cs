using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using WebApp_c.Models;

namespace WebApp_c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly VisitsContext _context;

        public PatientsController(VisitsContext context) => _context = context;

        // Get: /api/patients
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return _context.Patients;
        }


        // Get: /api/patients/n
        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatientItem(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();
            return patient;
        }

        // Post: /api/patients
        [HttpPost]
        public ActionResult<Patient> PostPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return CreatedAtAction("GetPatientItem", new Patient { Id = patient.Id }, patient);
        }

        // Put: /api/patients/n
        [HttpPut("{id}")]
        public ActionResult PutPatient(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // Delete: /api/patients/n
        [HttpDelete("{id}")]
        public ActionResult<Patient> DeletePatient(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();
            _context.Patients.Remove(patient);
            _context.SaveChanges();

            return patient;
        }
    }
}