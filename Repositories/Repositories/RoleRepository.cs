using BOL;
using DAL.DAO;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IGenericDAO<Role> _roleDao;
        public RoleRepository(IGenericDAO<Role> roleDao)
        {
            _roleDao = roleDao;
        }
        public Role GetStaffRole()
        {
            return _roleDao.GetAll(c => true).Where(c => c.RoleName == "Staff").FirstOrDefault();
        }
    }
}
