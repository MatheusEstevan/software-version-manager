using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareVersionManager.Entities.Models
{
    public class Software
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public ICollection<SoftwareVersion>? Versions { get; set; }

    }
}
