using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Stroika.DAL.Entity
{
    public partial class Relationship
{
    [Key]
    public int RelationshipId { get; set; }

    [StringLength(10)]
    public string? RelationshipName { get; set; }

    [InverseProperty("Relationship")]
    public virtual ICollection<StudentFamily> StudentFamilies { get; set; } = new List<StudentFamily>();
}
}