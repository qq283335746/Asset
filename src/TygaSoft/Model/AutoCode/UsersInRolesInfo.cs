using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class UsersInRolesInfo
    {
        public UsersInRolesInfo() { }

        public UsersInRolesInfo(Guid userId, Guid roleId)
        {
            this.UserId = userId;
            this.RoleId = roleId;
        }

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
