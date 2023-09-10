using Microsoft.EntityFrameworkCore;
using Stroika.DAL;
using Stroika.DAL.Entity;
using Stroika.Models.FamilyMember;
using Stroika.Models.Students;
using Stroika.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroika.Services
{
    public class StudentService : IStudentService
    {
        StroikaDBContext _dbContext;
        public StudentService(StroikaDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<StudentModel>> GetAllStudents()
        {
            var data = await _dbContext.Students.AsNoTracking().Select(i => new StudentModel
            {
                Id = i.StudentId,
                DateOfBirth = i.DateOfBirth,
                FirstName = i.FirstName,
                LastName = i.LastName,
                //NationalityId = i.NationalityId,
            }).ToListAsync();
            return data;
        }
        public async Task<StudentModel> GetStudentById(int studentId)
        {
            var student = await _dbContext.Students.Where(i => i.StudentId == studentId).Select(i => new StudentModel
            {
                Id = i.StudentId,
                DateOfBirth = i.DateOfBirth,
                FirstName = i.FirstName,
                LastName = i.LastName,
                //NationalityId = i.NationalityId,
            }).FirstOrDefaultAsync();
            return student;
        }
        public async Task<StudentWithFamliyModel> GetStudentFullDataById(int studentId)
        {
            var student = await _dbContext.Students.Include(i => i.StudentFamilies).FirstOrDefaultAsync(i => i.StudentId == studentId);
            
             var studentModel=   new StudentWithFamliyModel
             {
                Id = student.StudentId,
                DateOfBirth = student.DateOfBirth,
                FirstName = student.FirstName,
                LastName = student.LastName,
                NationalityId = student.NationalityId,
                famliyMembers=student.StudentFamilies.Select(i=>new StudentFamilyMemberModel
                {
                    DateOfBirth=i.DateOfBirth,
                    FirstName=i.FirstName,
                    LastName=i.LastName,
                    NationalityId=i.NationalityId,
                    RelationshipId=i.RelationshipId,
                    Id=i.StudentFamilyId
                }).ToList()

            };
            return studentModel;
        }
        public async Task<StudentModel> GetStudentWithNationalityById(int studentId)
        {
            var student = await _dbContext.Students.Where(i => i.StudentId == studentId).Select(i => new StudentModel
            {
                Id = i.StudentId,
                DateOfBirth = i.DateOfBirth,
                FirstName = i.FirstName,
                LastName = i.LastName,
                NationalityId = i.NationalityId,
            }).FirstOrDefaultAsync();
            return student;
        }
        public async Task<StudentModel> AddNewStudent(StudentModel newStudentModel)
        {
            var newStudent = new Student
            {
                FirstName = newStudentModel.FirstName,
                LastName = newStudentModel.LastName,
                DateOfBirth = newStudentModel.DateOfBirth,
            };
            _dbContext.Students.Add(newStudent);
            var counts = await _dbContext.SaveChangesAsync();
            newStudentModel.Id = newStudent.StudentId;
            return newStudentModel;
        }
        public async Task<StudentModel> AddNewStudentWithFamliyMemebers(StudentWithFamliyModel newStudentModel)
        {
            var newStudent = new Student
            {
                FirstName = newStudentModel.FirstName,
                LastName = newStudentModel.LastName,
                DateOfBirth = newStudentModel.DateOfBirth,
            };
            if (newStudentModel.famliyMembers != null)
                newStudent.StudentFamilies = newStudentModel.famliyMembers.Select(i => new
                    StudentFamily
                {
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    DateOfBirth = i.DateOfBirth,
                    NationalityId = i.NationalityId,
                    RelationshipId = i.RelationshipId,
                }).ToList();
            _dbContext.Students.Add(newStudent);
            var counts = await _dbContext.SaveChangesAsync();
            newStudentModel.Id = newStudent.StudentId;
            return new StudentModel
            {
                FirstName = newStudentModel.FirstName,
                LastName = newStudentModel.LastName,
                DateOfBirth = newStudentModel.DateOfBirth,
                Id = newStudent.StudentId
            };
        }

        public async Task<StudentModel> UpdateStudentWithFamliyMemebers(StudentWithFamliyModel newStudentModel)
        {
            var student = await _dbContext.Students.Include(i => i.StudentFamilies).SingleAsync(i => i.StudentId == newStudentModel.Id);
            student.FirstName= newStudentModel.FirstName;
            student.LastName= newStudentModel.LastName;
            student.DateOfBirth = newStudentModel.DateOfBirth;
            student.NationalityId = newStudentModel.NationalityId;
            

            newStudentModel.famliyMembers = newStudentModel.famliyMembers ?? new List<StudentFamilyMemberModel>();
            student.StudentFamilies = student.StudentFamilies ?? new List<StudentFamily>();
            var newFMIds = newStudentModel.famliyMembers.Select(i => i.Id).ToList();
            var membersWillDelete = student.StudentFamilies.Where(i => !newFMIds.Contains(i.StudentFamilyId)).ToList();
            var newMembers = newStudentModel.famliyMembers.Where(i => i.Id == 0).Select(i => new StudentFamily
            {
                FirstName = i.FirstName,
                LastName = i.LastName,
                DateOfBirth = i.DateOfBirth,
                NationalityId = i.NationalityId,
                RelationshipId = i.RelationshipId,
            }).ToList();
            membersWillDelete.ForEach(i => student.StudentFamilies.Remove(i));
            student.StudentFamilies.ToList().ForEach(fm =>
            {
                var fmUpdate = newStudentModel.famliyMembers.First(s => s.Id == fm.StudentFamilyId);
                fm.FirstName = fmUpdate.FirstName;
                fm.LastName = fmUpdate.LastName;
                fm.DateOfBirth = fmUpdate.DateOfBirth;
                fm.NationalityId = fmUpdate.NationalityId;
                fm.RelationshipId = fmUpdate.RelationshipId;
            });
            newMembers.ForEach(i => student.StudentFamilies.Add(i)); 
            var counts = await _dbContext.SaveChangesAsync(); 
            return new StudentModel
            {
                FirstName = newStudentModel.FirstName,
                LastName = newStudentModel.LastName,
                DateOfBirth = newStudentModel.DateOfBirth,
                Id = student.StudentId
            };
        }
        public async Task<StudentModel> UpdateStudentNationality(int studentId, int nationalityId)
        {
            var student = await _dbContext.Students.SingleAsync(i => i.StudentId == studentId);
            student.NationalityId = nationalityId;
            var counts = await _dbContext.SaveChangesAsync();

            return new StudentModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                NationalityId = student.NationalityId,

            };
        }
        public async Task<StudentModel> UpdateStudent(int studentId, StudentModel studentModel)
        {
            var student = await _dbContext.Students.SingleAsync(i => i.StudentId == studentId);
            student.FirstName = studentModel.FirstName;
            student.LastName = studentModel.LastName;
            student.DateOfBirth = studentModel.DateOfBirth;
            var counts = await _dbContext.SaveChangesAsync();
            studentModel.Id = studentId;
            return studentModel;
        }
        public async Task<bool> DeleteStudent(int studentId)
        {
            // var count = await _dbContext.Students.Include(i=>i.StudentFamilies).Where(i => i.StudentId == studentId).ExecuteDeleteAsync();
            var student = await _dbContext.Students.Include(i => i.StudentFamilies).FirstOrDefaultAsync(i => i.StudentId == studentId);
              _dbContext.Students.Remove(student);
            var count = await _dbContext.SaveChangesAsync();
            return count > 0;
        }
        public async Task<List<StudentFamilyMemberModel>> GetAllStudentFamilyMembers(int studentId)
        {
            var data = await _dbContext.StudentFamilies.Where(i => i.StudentId == studentId).Select(i => new StudentFamilyMemberModel
            {
                Id = i.StudentFamilyId,
                DateOfBirth = i.DateOfBirth,
                FirstName = i.FirstName,
                LastName = i.LastName,
                RelationshipId = i.RelationshipId,
            }).ToListAsync();
            return data;
        }
        public async Task<StudentFamilyMemberModel> AddNewwStudnetFamilyMember(int studentId, StudentFamilyMemberModel familyMemberModel)
        {
            var newStudentFamily = new StudentFamily
            {
                FirstName = familyMemberModel.FirstName,
                StudentId = studentId,
                LastName = familyMemberModel.LastName,
                DateOfBirth = familyMemberModel.DateOfBirth,
                RelationshipId = familyMemberModel.RelationshipId
            };
            _dbContext.StudentFamilies.Add(newStudentFamily);
            var counts = await _dbContext.SaveChangesAsync();
            familyMemberModel.Id = newStudentFamily.StudentFamilyId;
            return familyMemberModel;
        }
    }
}
