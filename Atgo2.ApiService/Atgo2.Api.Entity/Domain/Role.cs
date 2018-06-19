using System;
using System.Collections.Generic;
using System.Text;

namespace Atgo2.Api.Entity.Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Access { get; set; }
        public string ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public int DataCount { get; set; }
        public int UserCount { get; set; }
    }
}
