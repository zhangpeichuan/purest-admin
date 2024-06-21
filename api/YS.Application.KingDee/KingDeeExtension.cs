// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Kingdee.CDP.WebApi.SDK;
using KingdeeDiyWebApiClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PurestAdmin.SqlSugar;
using PurestAdmin.SqlSugar.Entity;
using System.Data;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YS.Application.KingDee;
public static class KingDeeExtension
{
    public static KDApi kdApi { get; private set; }
    public static IServiceCollection AddKingDeeService(this IServiceCollection services)
    {
        var configuration = services.GetConfiguration();
        kdApi = configuration?.GetSection("KDApi").Get<KDApi>() ?? throw new Exception("未正确配置金蝶认证信息");
        var result = InvokeHelper.LoginByAppSecret(kdApi.ServerUrl, kdApi.AcctID, kdApi.UserName, kdApi.AppID, kdApi.AppSec);

        var iResult = JObject.Parse(result)["LoginResultType"].Value<int>();
        if (iResult == 1 || iResult == -5)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("@F_DATE_B@", "2023-06-01");
            dictionary.Add("@F_DATE_E@", "2023-06-01");
            dictionary.Add("@QueryType@", "1");
            List<object> args = new List<object> { dictionary };
            List<object> a = new List<object> { "abc", 123 };
            var resultString = InvokeHelper.AbstractWebApiBusinessService("Ys.Scm.PrdInStockPlugIn.WebApi.PrdInStock.ExecuteService,Ys.Scm.PrdInStockPlugIn.WebApi", args);
            // 解析JSON字符串为JObject  
            JObject jsonObject = JObject.Parse(resultString);
            JArray jsonArray = (JArray)jsonObject["Table"];
            var lists = new List<PrdinstockEntity>(); 
            foreach (JObject obj in jsonArray)
            {
                PrdinstockEntity entity = new PrdinstockEntity();
                string FDATE= obj["FDATE"].ToString();
                string FNUMBER = obj["FNUMBER"].ToString();
                string FNAME = obj["FNAME"].ToString();
                double FREALQTY = (double)obj["FREALQTY"];
                double FREALQTYUnit = (double)obj["FREALQTYUnit"];
                double fzsl = (double)obj["fzsl"];
                double fmsl = (double)obj["fmsl"];
                entity.Fdate = FDATE;
                entity.Fnumber = FNUMBER;
                entity.Fname = FNAME;
                entity.Frealqty = FREALQTY;
                entity.Frealqtyunit = FREALQTYUnit;
                entity.Fzsl = fzsl;
                entity.Fmsl = fmsl;
                lists.Add(entity);
            }
            Console.WriteLine(lists.Count());
            // 从JObject中提取名为"Table"的JArray  
            /*
                        DataTable dataTable = result.Tables[0];
              // 解析JSON字符串为JObject  
                    JObject jsonObject = JObject.Parse(json);  

                    // 从JObject中提取名为"Table"的JArray  
                    JArray jsonArray = (JArray)jsonObject["Table"];  
                        foreach (DataRow row in dataTable.Rows)
                        {
                            // 假设每个row都有一个名为"ColumnName"的列
                            string FDATE = row["FDATE"].ToString();
                            string FNUMBER = row["FNUMBER"].ToString();
                            string FNAME = row["FNAME"].ToString();
                            string fzsl = row["fzsl"].ToString();
                            string fmsl = row["fmsl"].ToString();
                            string FREALQTY = row["FREALQTY"].ToString();
                            string FREALQTYUnit = row["FREALQTYUnit"].ToString();
                            Console.WriteLine("FDATE" + FDATE + "FNUMBER" + FNUMBER
                                + "FNAME" + FNAME + "fzsl" + fzsl + "fmsl" + fmsl + "FREALQTY" + FREALQTY + "FREALQTYUnit" + FREALQTYUnit);
                        }*/
            Trace.WriteLine(kdApi.ServerUrl);

        }
        else
        {
            Console.WriteLine("login failed");
        }
        return services;
    }
}

