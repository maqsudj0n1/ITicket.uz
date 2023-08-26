using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITicket.uz.Application.UseCases.Roles.Queries.GetAllRole
{
    public class RoleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string>? PermissionNames { get; set; }
    }
}