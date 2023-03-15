using System;
using System.Collections.Generic;
using System.Text;

namespace RB444.Model.ViewModel
{
    public class AccessPermissionModel
    {
        public string token { get; set; }        
        public List<AccessModel> accessModels { get; set; } = new List<AccessModel>();
        public UserInfo userDetails { get; set; }
    }
    public class AccessModel
    {
        public string formName { get; set; }
        public bool isRead { get; set; }
        public bool isWrite { get; set; }
        public bool isDelete { get; set; }
    }

    public class UserInfo
    {
        public string UserFullName { get; set; }
        public string UserRoleName { get; set; }
    }
}
