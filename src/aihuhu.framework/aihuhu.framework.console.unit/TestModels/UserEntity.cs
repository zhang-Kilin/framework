using aihuhu.framework.data.orm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit.TestModels
{
    [Serializable]
    public enum UserStatus
    {
        Normal,
        Disabled
    }

    [Table("Users", Schema = "dbo")]
    public class UserEntity
    {
        [ColumnName("UserName")]
        public string UserName { get; set; }

        [ColumnName("UserId", IsIdentity = true, IsPrimaryKey = true)]
        public int UserId { get; set; }

        [ColumnName("Password")]
        public string Password { get; set; }

        [ColumnName("Status")]
        public UserStatus Status { get; set; }

        [ColumnName("NickName")]
        public string NickName { get; set; }

        [ColumnName("Create_Time")]
        public DateTime CreateTime { get; set; }

        [ColumnName("Create_User")]
        public string CreateUser { get; set; }
    }
}
