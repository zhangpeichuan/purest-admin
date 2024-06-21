using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdeeDiyWebApiClient
{
	public static class InvokeHelper
	{
		private static string CloudUrl;//K/3 Cloud 业务站点地址

		/// <summary>
		/// 登陆
		/// </summary>
		public static string Login(string cloudUrl, string webCode,string userName,string password)
		{
			CloudUrl=cloudUrl;
			HttpClient httpClient = new HttpClient();
			httpClient.Url = string.Concat(cloudUrl, "Kingdee.BOS.WebApi.ServicesStub.AuthService.ValidateUser.common.kdsvc");

			List<object> Parameters = new List<object>();
			Parameters.Add(webCode);//账套标示
			Parameters.Add(userName);//用户名
			Parameters.Add(password);//密码
			Parameters.Add(2052);//2052代表中文
			httpClient.Content = JsonConvert.SerializeObject(Parameters);

			return httpClient.SysncRequest();
		}
        /// <summary>
        /// 登陆
        /// </summary>
        public static string LoginByAppSecret(string cloudUrl, string dbId, string userName , string appId, string appSecret)
        {
            CloudUrl = cloudUrl;
            HttpClient httpClient = new HttpClient();
            httpClient.Url = string.Concat(cloudUrl, "Kingdee.BOS.WebApi.ServicesStub.AuthService.LoginByAppSecret.common.kdsvc");
            List<object> Parameters = new List<object>();
            Parameters.Add(dbId);//账套标示
            Parameters.Add(userName);//用户名
            Parameters.Add(appId);//密码
            Parameters.Add(appSecret);//密码
            Parameters.Add(2052);//2052代表中文
            httpClient.Content = JsonConvert.SerializeObject(Parameters);
            return httpClient.SysncRequest();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Save(string formId, CreatedBody content)
		{
			HttpClient httpClient = new HttpClient();
			httpClient.Url = string.Concat(CloudUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Save.common.kdsvc");

			List<object> Parameters = new List<object>();
			//业务对象Id 
			Parameters.Add(formId);
			//Json字串
			Parameters.Add(content);
			httpClient.Content = JsonConvert.SerializeObject(Parameters);
			return httpClient.SysncRequest();
		}

		/// <summary>
		/// 提交
		/// </summary>
		/// <param name="formId"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static string Submit(string formId, CreatedBody content)
		{
			HttpClient httpClient = new HttpClient();
			httpClient.Url = string.Concat(CloudUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Submit.common.kdsvc");

			List<object> Parameters = new List<object>();
			//业务对象Id 
			Parameters.Add(formId);
			//Json字串
			Parameters.Add(content);
			httpClient.Content = JsonConvert.SerializeObject(Parameters);
			return httpClient.SysncRequest();
		}
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="formId"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static string Delete(string formId, DeleteBody content)
		{
			HttpClient httpClient = new HttpClient();
			httpClient.Url = string.Concat(CloudUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Delete.common.kdsvc");

			List<object> Parameters = new List<object>();
			//业务对象Id 
			Parameters.Add(formId);
			//Json字串
			Parameters.Add(content);
			httpClient.Content = JsonConvert.SerializeObject(Parameters);
			return httpClient.SysncRequest();
		}

		/// <summary>
		/// 审核
		/// </summary>
		/// <param name="formId"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static string Audit(string formId, AuditBody content)
		{
			HttpClient httpClient = new HttpClient();
			httpClient.Url = string.Concat(CloudUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.Audit.common.kdsvc");

			List<object> Parameters = new List<object>();
			//业务对象Id 
			Parameters.Add(formId);
			//Json字串
			Parameters.Add(content);
			httpClient.Content = JsonConvert.SerializeObject(Parameters);
			return httpClient.SysncRequest();
		}
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="formId"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		public static string View(string formId, ViewBody content)
		{
			HttpClient httpClient = new HttpClient();
			httpClient.Url = string.Concat(CloudUrl, "Kingdee.BOS.WebApi.ServicesStub.DynamicFormService.View.common.kdsvc");

			List<object> Parameters = new List<object>();
			//业务对象Id 
			Parameters.Add(formId);
			//Json字串
			Parameters.Add(content);
			httpClient.Content = JsonConvert.SerializeObject(Parameters);
			return httpClient.SysncRequest();
		}

		/// <summary>
		/// 自定义
		/// </summary>
		/// <param name="key">自定义方法标识</param>
		/// <param name="args">参数</param>
		/// <returns></returns>
		public static string AbstractWebApiBusinessService(string key, List<object> args)
		{
			HttpClient httpClient = new HttpClient();
			httpClient.Url = string.Concat(CloudUrl, key, ".common.kdsvc");

			httpClient.Content = JsonConvert.SerializeObject(args);
			return httpClient.SysncRequest();
		}
	}
}
