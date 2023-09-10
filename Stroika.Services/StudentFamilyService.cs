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
    public class StudentFamilyService : IStudentFamilyService
    {
        StroikaDBContext _dbContext;
        public StudentFamilyService(StroikaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentFamilyMemberModel> UpdatesFamilyMember(int studentFamilyId, StudentFamilyMemberModel familyMemberModel)
        {
            var newStudentFamily = new StudentFamily
            {
                FirstName = familyMemberModel.FirstName,
                LastName = familyMemberModel.LastName,
                DateOfBirth = familyMemberModel.DateOfBirth,
                RelationshipId = familyMemberModel.RelationshipId
            };
            _dbContext.StudentFamilies.Add(newStudentFamily);
            var counts = await _dbContext.SaveChangesAsync();
            familyMemberModel.Id = newStudentFamily.StudentFamilyId;
            return familyMemberModel;
        }
        public async Task<bool> DeleteFamilyMember(int studentFamilyId)
        {
            var counts = await _dbContext.StudentFamilies.Where(i => i.StudentFamilyId == studentFamilyId).ExecuteDeleteAsync();

            return counts > 0;
        }
        public async Task<StudentFamilyMemberModel> UpdateNationalityForFamilyMembers(int studentFamilyId, int nationalityId)
        {
            var familyMember = await _dbContext.StudentFamilies.SingleAsync(i => i.StudentFamilyId == studentFamilyId);
            familyMember.NationalityId = nationalityId;
            var counts = await _dbContext.SaveChangesAsync();

            return new StudentFamilyMemberModel
            {
                Id = studentFamilyId,
                FirstName = familyMember.FirstName,
                LastName = familyMember.LastName,
                DateOfBirth = familyMember.DateOfBirth,
                NationalityId = familyMember.NationalityId,
                RelationshipId = familyMember.RelationshipId
            };
        }
    }
}
