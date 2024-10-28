using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Roles

        List<Role> GetRoles();
        void AddRolesToUser(List<int> roleIds,int userId);
        void EditRolesUser(int userId, List<int> rolesId);

        #endregion
    }
}
