using ApiCURD.Data;
using ApiCURD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCURD.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class TeacherController : Controller
    {
        private readonly TeacherAPIDbContext dbContext;

        public TeacherController(TeacherAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeacher()
        {

            return Ok(await dbContext.Teachers.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTeacher([FromRoute] Guid id)
        {
            var teacher = await dbContext.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();

            }
            return Ok(teacher);
        }
        

        [HttpPost]
        public async Task<IActionResult> AddTeacher(AddTeacherRequest addTeacherRequest )
        {
            var teacher = new Teacher()
            {
                Id = Guid.NewGuid(),
                Name = addTeacherRequest.Name,
                Email = addTeacherRequest.Email,
                Phone = addTeacherRequest.Phone,

            };

            await dbContext.Teachers.AddAsync(teacher);
            await dbContext.SaveChangesAsync();

            return Ok(teacher);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTeacher([FromRoute]Guid id, UpdateTeacherRequest updateTeacherRequest)
        {
           var teacher = await dbContext.Teachers.FindAsync(id);

            if(teacher != null)
            {
                teacher.Name = updateTeacherRequest.Name;
                teacher.Email = updateTeacherRequest.Email;
                teacher.Phone = updateTeacherRequest.Phone;

                await dbContext.SaveChangesAsync();
                return Ok(teacher);
            }
            return NoContent();
         
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var teacher = await dbContext.Teachers.FindAsync(id);

            if(teacher != null)
            {
                dbContext.Remove(teacher);
                await dbContext.SaveChangesAsync();
                return Ok(teacher);
            }
            return NotFound();
        }
    
    }
}
