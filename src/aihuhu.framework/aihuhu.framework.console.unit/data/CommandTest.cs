using aihuhu.framework.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit.data
{
    [FrameworkTest]
    public class CommandTest
    {
        [MethodTest]
        public void ExecuteReaderTest()
        {
            DataCommand command = DataCommand.Create("UserCommand.QueryUserByUserName");
            command.SetParameterValue("UserName", "admin");

            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.WriteLine(reader["NickName"]);
                }
            }
        }

        [MethodTest]
        public void ExecuteReaderOutputParameter()
        {
            DataCommand command = DataCommand.Create("UserCommand.QueryByStatus");
            command.SetParameterValue("Status", "Normal");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader["NickName"]);
                }
            }
            watch.Stop();

            Console.WriteLine("记录总数：{0}，耗时：{1}ms", command.GetParameterValue("Records"), watch.ElapsedMilliseconds);
        }
    }
}
