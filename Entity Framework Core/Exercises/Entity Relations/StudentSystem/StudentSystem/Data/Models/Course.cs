using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public Course()
        {
            Homeworks = new HashSet<Homework>();
            Resources = new HashSet<Resource>();
            StudentsCourses = new HashSet<StudentCourse>();
        }

        public int CourseId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<Homework> Homeworks { get; set; }

        public ICollection<Resource> Resources { get; set; }

        public ICollection<StudentCourse> StudentsCourses { get; set; }

    }

}
