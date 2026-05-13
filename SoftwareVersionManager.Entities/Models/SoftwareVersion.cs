using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareVersionManager.Entities.Models
{
    public class SoftwareVersion
    {
        public int Id { get; set; }
        public string? VersionNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int SoftwareId { get; set; }
        public bool isDeprecated { get; set; }
        public string? Description { get; set; }
        //public DateTime deprecationDate { get; set; }

    }
}
