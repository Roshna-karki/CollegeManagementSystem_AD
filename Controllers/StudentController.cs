using Microsoft.AspNetCore.Mvc;
using CollegeManagementSystem.Models;

namespace CollegeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "John Doe", Age = 20, Course = "Computer Science" },
            new Student { Id = 2, Name = "Jane Smith", Age = 21, Course = "Information Technology" },
            new Student { Id = 3, Name = "Ahmed Khan", Age = 19, Course = "Software Engineering" },
            new Student { Id = 4, Name = "Sarah Johnson", Age = 22, Course = "Cybersecurity" }
        };
        public StudentController() { }
        // A) Get All Students
        // GET api/student/getall
        [HttpGet("getall")]
        public ActionResult<List<Student>> GetAllStudents()
        {
            return Ok(students);
        }

        // B) Get Student by ID
        // GET api/student/{id}
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("ID cannot be empty.");
            }

            var student = students.FirstOrDefault(s => s.Id.ToString() == id);
            if (student == null)
            {
                return NotFound("Student not found.");
            }
            return Ok(student);
        }

        // C) Add a New Student
        // POST api/student/add
        [HttpPost("add")]
        public ActionResult<Student> AddStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Student data is required.");
            }

            if (string.IsNullOrWhiteSpace(student.Name))
            {
                return BadRequest("Student name is required.");
            }

            if (student.Age <= 0)
            {
                return BadRequest("Student age must be greater than 0.");
            }

            if (string.IsNullOrWhiteSpace(student.Course))
            {
                return BadRequest("Student course is required.");
            }

            students.Add(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        // D) Update an Existing Student
        // PUT api/student/update
        [HttpPut("update")]
        public ActionResult<Student> UpdateStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Student data is required.");
            }

            if (string.IsNullOrWhiteSpace(student.Name))
            {
                return BadRequest("Student name is required.");
            }

            if (student.Age <= 0)
            {
                return BadRequest("Student age must be greater than 0.");
            }

            if (string.IsNullOrWhiteSpace(student.Course))
            {
                return BadRequest("Student course is required.");
            }

            var existingStudent = students.FirstOrDefault(s => s.Id == student.Id);
            if (existingStudent == null)
            {
                return NotFound("Student not found.");
            }

            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Course = student.Course;

            return Ok(existingStudent);
        }

        // E) Delete a Student
        // DELETE api/student/delete/{id}
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteStudent(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("ID cannot be empty.");
            }

            var student = students.FirstOrDefault(s => s.Id.ToString() == id);
            if (student == null)
            {
                return NotFound("Student not found.");
            }

            students.Remove(student);
            return Ok("Student deleted successfully.");
        }
    }
}
