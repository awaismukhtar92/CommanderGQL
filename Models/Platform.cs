using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace CommanderGQL.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string LicenseKey { get; set; }
        public List<Command> Commands { get; set; } = new List<Command>();
    }
}