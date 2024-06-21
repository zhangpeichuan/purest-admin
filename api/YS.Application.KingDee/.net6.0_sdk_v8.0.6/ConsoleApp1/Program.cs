using Kingdee.BOS.WebApi.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new K3CloudApiClient("http://172.16.5.112/k3cloud/");
            string loginResult = client.LoginByAppSecret(
             "64ed9963bc8ab7",//账套
             "张培川",//用户名
             "280017_Q/5pT8vESIrV1X0HW+4qT89N5g4/wtss",//应用ID
             "764c356ac9004f1397177a1dae6a6269",//应用秘钥
             2052 //中文
             );

            Newtonsoft.Json.Linq.JObject loginResultObj = JObject.Parse(loginResult);

            JToken loginResultType;

            loginResultObj.TryGetValue("LoginResultType", out loginResultType);

            if (loginResultType != null && loginResultType.Value<Int32>() == 1)
            {
                JObject prdParameters = new JObject();
                DateTime dt = DateTime.Now;
                string formattedDate = dt.AddYears(-1).ToString("yyyy-MM-dd");
                prdParameters.Add("FDATE", formattedDate+" 00:00:00");
                var parameters= new object[] { prdParameters };
                var result = client.Execute<DataSet>("Ys.Scm.PrdInStockPlugIn.WebApi.PrdInStock.ExecuteService1,Ys.Scm.PrdInStockPlugIn.WebApi", parameters);

                Console.WriteLine(result);

            }
            Console.ReadLine();
        }
    }
}
