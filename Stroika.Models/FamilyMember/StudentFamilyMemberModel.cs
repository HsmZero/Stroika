using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stroika.Models.FamilyMember
{
    public class StudentFamilyMemberModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? NationalityId { get; set; }
        public int RelationshipId { get; set; }
    }
}
