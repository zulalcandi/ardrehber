#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArdRehber.Data;
using ArdRehber.Entities;
using ArdRehber.Dtos;
using ArdRehber.FluentValidation;
using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace ArdRehber.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.Persons.Include(x => x.Department).ToListAsync();//Include(x=>x.Department)    İNCLUDE İŞLEMİ sss  Include(x => x.Department).
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/Persons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Persons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(PersonDto personDto)
        {
            PersonValidator personValidator = new PersonValidator();
            ValidationResult results = personValidator.Validate(personDto);

            if (results.IsValid == false)
            {
                return BadRequest(results.Errors[0].ErrorMessage);
            }

            var kontrolPerson = await _context.Persons.AnyAsync(s => s.Name == personDto.Name && s.SurName == personDto.SurName && s.PhoneNumber == personDto.PhoneNumber
           && s.InternalNumber == personDto.InternalNumber && s.DepartmentId == personDto.DepartmentId);

            if (kontrolPerson == true)
            {
                return BadRequest("Aynı kullanıcıyı tekrar ekleyemezsiniz.");
            }

            var person = new Person()
            {
                Name = personDto.Name,
                SurName = personDto.SurName,
                PhoneNumber = personDto.PhoneNumber,
                InternalNumber = personDto.InternalNumber,
                DepartmentId = personDto.DepartmentId,
                UserTypeId=2
                
            };
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            personDto.Id = person.Id;

            return Ok(personDto);
            //return CreatedAtAction("GetPerson", new { id = person.Id }, person);

            //PersonValidator personValidator=new PersonValidator();
            //ValidationResult results = personValidator.Validate(person);
            //personValidator.Validate(person);   
        }   

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}
