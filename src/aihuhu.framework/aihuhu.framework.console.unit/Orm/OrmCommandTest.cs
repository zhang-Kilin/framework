using aihuhu.framework.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aihuhu.framework.data.orm;
using aihuhu.framework.console.unit.TestModels;
using System.Diagnostics;
using System.Threading;

namespace aihuhu.framework.console.unit.Orm
{
    [FrameworkTest]
    public class OrmCommandTest
    {
        [MethodTest]
        public void CommandTest()
        {
            IDataCommand command = DataCommand.Create("UserCommand.QueryUserByUserName");
            command.SetParameterValue("UserName", "admin");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IList<UserEntity> users = command.ExecuteList<UserEntity>();
            watch.Stop();
            Console.WriteLine("ORM:{0} {1}  耗时：{2}ms  CurrentThread:{3}", users[0].CreateUser, users[0].Status, watch.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId);
        }

        [MethodTest]
        public void CommandTest1()
        {
            for (int i = 0; i < 10; i++)
            {
                CommandTest();
            }
        }

        [MethodTest]
        public void ContextTest()
        {
            Context context = new Context("myframework");
            Collection<UserEntity> collection = new Collection<UserEntity>();
            UserEntity u = collection.New();
            u.CreateTime = DateTime.Now;
            u.CreateUser = "system";
            u.NickName = "Kilin";
            u.Password = "456123";
            u.Status = UserStatus.Disabled;
            u.UserName = "Kilin";
            u.HashCode = "987456783";
            context.Save<UserEntity>(collection);

            Console.WriteLine("UserId:" + u.UserId);
        }
    }
}
