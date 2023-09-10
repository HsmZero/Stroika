using Stroika.Models.FamilyMember;

namespace Stroika.Services.Interface
{
    public interface IStudentFamilyService
    {
        Task<bool> DeleteFamilyMember(int studentFamilyId);
        Task<StudentFamilyMemberModel> UpdateNationalityForFamilyMembers(int studentFamilyId, int nationalityId);
        Task<StudentFamilyMemberModel> UpdatesFamilyMember(int studentFamilyId, StudentFamilyMemberModel familyMemberModel);
    }
}