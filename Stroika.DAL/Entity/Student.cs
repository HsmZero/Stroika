using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Stroika.DAL.Entity
{
    public partial class Student
    {
        [Key]
        public int StudentId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [Column("lastName")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public int? NationalityId { get; set; }

        [ForeignKey("NationalityId")]
        [InverseProperty("Students")]
        public virtual Nationality Nationality { get; set; } = null!;

        [InverseProperty("Student")]
        public virtual ICollection<StudentFamily> StudentFamilies { get; set; } = new List<StudentFamily>();
    }
}