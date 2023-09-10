using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stroika.DAL.Entity
{
    [Table("StudentFamily")]
public partial class StudentFamily
{
    [Key]
    public int StudentFamilyId { get; set; }

    public int StudentId { get; set; }

    [Column(TypeName = "date")]
    public DateTime DateOfBirth { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Column("lastName")]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    public int RelationshipId { get; set; }

    public int? NationalityId { get; set; }

    [ForeignKey("NationalityId")]
    [InverseProperty("StudentFamilies")]
    public virtual Nationality Nationality { get; set; } = null!;

    [ForeignKey("RelationshipId")]
    [InverseProperty("StudentFamilies")]
    public virtual Relationship Relationship { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("StudentFamilies")]
    public virtual Student Student { get; set; } = null!;
}
}