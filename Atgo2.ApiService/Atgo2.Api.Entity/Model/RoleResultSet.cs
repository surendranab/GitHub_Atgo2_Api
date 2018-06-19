using System;
using System.Collections.Generic;
using System.Text;

namespace Atgo2.Api.Entity.Model
{
    public class RoleResultSet
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string ModuleName { get; set; }
        public string Action { get; set; }
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
