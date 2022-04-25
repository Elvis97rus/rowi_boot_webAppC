using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using WebApp_c.Models;

namespace WebApp_c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly VisitsContext _context;

        public DoctorsController(VisitsContext context) => _context = context;

        // Get: /api/doctors
        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            return _context.Doctors;
        }

        // Get: /api/doctors/n
        [HttpGet("{id}")]
        public ActionResult<Doctor> GetDoctorItem(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null) return NotFound();
            return doctor;
        }

        // Post: /api/doctors

        [HttpPost]
        public ActionResult<Doctor> PostDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return CreatedAtAction("GetDoctor", new Doctor { Id = doctor.Id }, doctor);
        }

        // Put: /api/doctors/n
        [HttpPut("{id}")]
        public ActionResult PutDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            _context.Entry(doctor).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // Delete: /api/doctors/n
        [HttpDelete("{id}")]
        public ActionResult<Doctor> DeleteDoctor(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null) return NotFound();
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();

            return doctor;
        }
    }
}