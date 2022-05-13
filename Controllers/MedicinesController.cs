using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using WebApp_c.Models;

namespace WebApp_c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly VisitsContext _context;

        public MedicinesController(VisitsContext context) => _context = context;

        // Get: /api/medicines
        [HttpGet]
        public ActionResult<IEnumerable<Medicine>> GetMedicines()
        {
            return _context.Medicines;
        }


        // Get: /api/medicines/n
        [HttpGet("{id}")]
        public ActionResult<Medicine> GetMedicineItem(int id)
        {
            var medicine = _context.Medicines.Find(id);
            if (medicine == null) return NotFound();
            return medicine;
        }

        // Post: /api/medicines
        /*    _payload_
              {
                "name": "Aspirine",
                "useMethod": "Drink with a small amount of water.",
                "effects": "Relief pain",
                "sideEffects": "Never mentioned"
            }
        */

        [HttpPost]
        public ActionResult<Medicine> PostMedicine(Medicine medicine)
        {

            _context.Medicines.Add(medicine);
            _context.SaveChanges();
            return CreatedAtAction("GetMedicineItem", new Medicine { Id = medicine.Id }, medicine);
        }

        // Put: /api/medicines/n
        [HttpPut("{id}")]
        public ActionResult PutMedicine(int id, Medicine medicine)
        {
            if (id != medicine.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicine).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }


        // Delete: /api/medicines/n
        [HttpDelete("{id}")]
        public ActionResult<Medicine> DeleteMedicine(int id)
        {
            var medicine = _context.Medicines.Find(id);
            if (medicine == null) return NotFound();
            _context.Medicines.Remove(medicine);
            _context.SaveChanges();

            return medicine;
        }
    }
}