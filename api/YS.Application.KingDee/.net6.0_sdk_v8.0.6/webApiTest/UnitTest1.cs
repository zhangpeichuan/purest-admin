using System;
using Kingdee.BOS.WebApi.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace webApiTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            #region Init
            // 星空站点Url
            var webSite = "http://172.16.5.112/k3cloud/";
            #endregion
            #region Action
            var client = new K3CloudApiClient(webSite);
            var rval = client.Execute<Kingdee.BOS.JSON.JSONObject>("Ys.Scm.PrdInStockPlugIn.WebApi.PrdInStock.DoSth,Ys.Scm.PrdInStockPlugIn.WebApi", new object[] { "abc", 123 });
            #endregion
            #region Assert
            Assert.IsTrue(rval != null);
            Console.WriteLine(JsonConvert.SerializeObject(rval));
            #endregion

        }
    }
}
