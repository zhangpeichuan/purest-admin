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
using YiKdWebClient.AuthService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace YS.Application.KingDee;
public static class KingDeeExtension
{
    //默认60分钟，少于55分钟即可重新登录刷新状态
    public const int refreshWebApiLoginToken  = 1 * 60;
    public static DateTime lastLoginTime;
    public static KDApi kdApi { get; private set; }
    public static IServiceCollection AddKingDeeService(this IServiceCollection services)
    {
        var configuration = services.GetConfiguration();
        kdApi = configuration?.GetSection("KDApi").Get<KDApi>() ?? throw new Exception("未正确配置金蝶认证信息");

        return services;
    }
    public static int doLoginByAppSecret()
    {
        var result = InvokeHelper.LoginByAppSecret(kdApi.ServerUrl, kdApi.AcctID, kdApi.UserName, kdApi.AppID, kdApi.AppSec);
        var iResult = JObject.Parse(result)["LoginResultType"].Value<int>();
        if (iResult == 1 || iResult == -5)
        {
            lastLoginTime = DateTime.Now;
            Console.WriteLine("login success "+lastLoginTime.ToString());
        }
        else
        {
            Console.WriteLine("login failed ");

        }
        return iResult;
    }
    public static bool validKDWebApiToken()
    {
        TimeSpan interval = DateTime.Now - lastLoginTime;
        double seconds = interval.TotalSeconds;
        if (seconds < refreshWebApiLoginToken)
        {
            return true;
        }
        return false;
    }
    public static string DoPrdinstockWebApi(List<object> args) {
        string resultString = "";
        if(validKDWebApiToken())
        {
            resultString = InvokeHelper.AbstractWebApiBusinessService(KDWebApiConst.PrdinstockWebAPI, args);
        }
        else
        {
           int iresult = doLoginByAppSecret();
            if(iresult == 1 || iresult == -5)
            {
                resultString = InvokeHelper.AbstractWebApiBusinessService(KDWebApiConst.PrdinstockWebAPI, args);
            }
        }
        return resultString;
    }

}

