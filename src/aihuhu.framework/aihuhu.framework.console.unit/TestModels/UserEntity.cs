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
    public class UserEntity : ModelBase
    {
        private string m_UserName;
        private int m_UserId;
        private string m_Password;
        private UserStatus m_UserStatus;
        private string m_NickName;
        private DateTime m_CreateTime;
        private string m_CreateUser;
        private string m_HashCode;

        [ColumnName("UserName")]
        public string UserName
        {
            get
            {
                return m_UserName;
            }
            set
            {
                Set("UserName", value);
                this.m_UserName = value;
            }
        }

        [ColumnName("UserId", IsIdentity = true, IsPrimaryKey = true)]
        public int UserId
        {
            get
            {
                return m_UserId;
            }
            set
            {
                Set("UserId", value);
                this.m_UserId = value;
            }
        }

        [ColumnName("Password")]
        public string Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                Set("Password", value);
                this.m_Password = value;
            }
        }

        [ColumnName("HashCode")]
        public string HashCode
        {
            get
            {
                return m_HashCode;
            }
            set
            {
                Set("HashCode", value);
                this.m_HashCode = value;
            }
        }

        [ColumnName("Status")]
        public UserStatus Status
        {
            get
            {
                return this.m_UserStatus;
            }
            set
            {
                Set("Status", value);
                this.m_UserStatus = value;
            }
        }

        [ColumnName("NickName")]
        public string NickName
        {
            get
            {
                return m_NickName;
            }
            set
            {
                Set("NickName", value);
                this.m_NickName = value;
            }
        }

        [ColumnName("Create_Time")]
        public DateTime CreateTime
        {
            get
            {
                return this.m_CreateTime;
            }
            set
            {
                Set("CreateTime", value);
                this.m_CreateTime = value;
            }
        }

        [ColumnName("Create_User")]
        public string CreateUser
        {
            get
            {
                return m_CreateUser;
            }
            set
            {
                Set("CreateUser", value);
                this.m_CreateUser = value;
            }
        }
    }
}
