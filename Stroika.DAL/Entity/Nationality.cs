using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stroika.DAL.Entity
{
    public partial class Nationality
{
    [Key]
    public int NationalityId { get; set; }

    [StringLength(50)]
    public string NationalityName { get; set; } = null!;

    [InverseProperty("Nationality")]
    public virtual ICollection<StudentFamily> StudentFamilies { get; set; } = new List<StudentFamily>();

    [InverseProperty("Nationality")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
}