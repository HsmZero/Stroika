using Stroika.Models.FamilyMember;
using Stroika.Models.Students;

namespace Stroika.Services.Interface
{
    public interface IStudentService
    {
        Task<StudentModel> AddNewStudent(StudentModel newStudentModel);
        Task<StudentFamilyMemberModel> AddNewwStudnetFamilyMember(int studentId, StudentFamilyMemberModel familyMemberModel);
        Task<List<StudentFamilyMemberModel>> GetAllStudentFamilyMembers(int studentId);
        Task<List<StudentModel>> GetAllStudents();
        Task<StudentModel> GetStudentById(int studentId);
        Task<StudentModel> GetStudentWithNationalityById(int studentId);
        Task<StudentModel> UpdateStudentNationality(int studentId, int nationalityId);
        Task<bool> DeleteStudent(int studentId);
        Task<StudentModel> UpdateStudent(int studentId, StudentModel studentModel);
        Task<StudentModel> AddNewStudentWithFamliyMemebers(StudentWithFamliyModel newStudentModel);
        Task<StudentModel> UpdateStudentWithFamliyMemebers(StudentWithFamliyModel newStudentModel);
        Task<StudentWithFamliyModel> GetStudentFullDataById(int studentId);
    }
}