using System;

namespace TygaSoft.Model
{
    public partial class StaffInfo
    {
        public string Email { get; set; }

        public Guid OrgId { get; set; }

        public string UserName { get; set; }

        public string OrgStep { get; set; }
    }
}
