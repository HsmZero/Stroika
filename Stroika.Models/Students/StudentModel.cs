using Stroika.Models.FamilyMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroika.Models.Students
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? NationalityId { get; set; }
    }
    public class StudentWithFamliyModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? NationalityId { get; set; }
        public List<StudentFamilyMemberModel> famliyMembers { get; set; }
    }
}
