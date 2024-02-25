using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entities
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string Phone { get; set; }

    }
}
