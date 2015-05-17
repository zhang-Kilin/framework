using aihuhu.framework.console.unit.TestModels;
using aihuhu.framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit.Logging
{

    [FrameworkTest]
    public class SerializeTest
    {
        [MethodTest]
        public void SerializeXml()
        {
            Emplooye emp = new Emplooye
            {
                Age = 30,
                Department = "Hello Kitty",
                Name = "Kilin",
                Salary = 124501M
            };

            string xml = SerializeHelper.SerializeXml(emp);

            Emplooye expect = (Emplooye)SerializeHelper.DeserializeXml(typeof(Emplooye), xml);

            if (expect == null)
            {
                throw new UnitTestException("测试不通过：SerializeHelper.DeserializeXml(typeof(Emplooye), xml) ");
            }

            xml = SerializeHelper.SerializeXml(expect, "emp");

            if (string.IsNullOrWhiteSpace(xml)
                || !Regex.IsMatch(xml,"xmlns:.+=\"emp\""))
            {
                throw new UnitTestException("SerializeHelper.SerializeXml(expect, \"emp\") ");
            }

            Console.WriteLine(xml);

        }
    }
}