using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stroika.Models.Students;
using Stroika.Services.Interface;

namespace Stroika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet(Name = "Students")]
        public async Task<List<StudentModel>> Get()
        { 
            var data= await _studentService.GetAllStudents();
            return data;

        }

        [HttpPost]
        public async Task<StudentModel> Post(StudentModel newStudentModel)
        {
            var data = await _studentService.AddNewStudent(newStudentModel);
            return data;

        }
        [HttpDelete("{studentId}")]
        public async Task<bool> Delete(int studentId)
        {
            var data = await _studentService.DeleteStudent(studentId);
            return data;

        }
        [HttpPut("{studentId}")]
        public async Task<StudentModel> Update(int studentId, StudentModel newStudentModel)
        {
            var data = await _studentService.UpdateStudent(studentId, newStudentModel);
            return data;

        }
        [HttpGet("{studentId}/Nationality")]
        public async Task<StudentModel> Nationality(int studentId)
        {
            var data = await _studentService.GetStudentWithNationalityById(studentId);
            return data;

        }
        [HttpPut("{studentId}/Nationality/{nationalityId}")]
        public async Task<StudentModel> UpdateNationality(int studentId, int nationalityId)
        {
            var data = await _studentService.UpdateStudentNationality(studentId, nationalityId);
            return data;

        }

        [HttpGet("GetStudentFullDataById/{studentId}")]
        public async Task<StudentWithFamliyModel> GetStudentFullDataById(int studentId)
        {
            var data = await _studentService.GetStudentFullDataById(studentId);
            return data;

        }
        [HttpPost("AddNewStudentWithFamliy")]
        public async Task<StudentModel> AddNewStudentWithFamliy(StudentWithFamliyModel studentWithFamliyModel)
        {
            var data = await _studentService.AddNewStudentWithFamliyMemebers(studentWithFamliyModel);
            return data;

        }
        [HttpPut("AddNewStudentWithFamliy")]
        public async Task<StudentModel> UpdateStudentWithFamliyMemebers(StudentWithFamliyModel studentWithFamliyModel)
        {
            var data = await _studentService.UpdateStudentWithFamliyMemebers(studentWithFamliyModel);
            return data;

        }
        //        Task<StudentWithFamliyModel> GetStudentFullDataById(int studentId);

    }
}
