using Kingdee.BOS;
using Kingdee.BOS.App.Data;
using Kingdee.BOS.ServiceFacade.KDServiceFx;
using Kingdee.BOS.WebApi.ServicesStub;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace YS.Scm.PrdInStockPlugIn.WebApi
{
    public class PrdInStock : AbstractWebApiBusinessService
    {
        public PrdInStock(KDServiceContext context) : base(context)
        {

        }
        public DataSet ExecuteService(JObject parameter)
        {
            var ctx = KDContext.Session.AppContext;
            if (ctx == null)
            {
                // 会话超时，需重新登录
                throw new Exception("ctx = null");
            }
            // 访问数据库获取用户名
            var sql = "SELECT FNAME FROM T_SEC_USER WHERE FUSERID=@FUSERID";
            var paramUserId = new Kingdee.BOS.SqlParam("@FUSERID", Kingdee.BOS.KDDbType.Int32, ctx.UserId);
            var userName = DBUtils.ExecuteScalar(ctx, sql, string.Empty, paramUserId);
            if (!userName.Equals("张培川"))
            {
                DataTable dataTable = new DataTable();
                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(dataTable);
                return dataSet;
            }

            var procName = "proc_scrkrb";
            string F_DATE_B = MyConvert.ToString(parameter["@F_DATE_B@"]);
            string F_DATE_E = MyConvert.ToString(parameter["@F_DATE_E@"]);

            string queryType = MyConvert.ToString(parameter["@QueryType@"]);
            var lstParam = new List<SqlParam>();
            if (F_DATE_B.Length > 0) { lstParam.Add(new SqlParam("@F_DATE_B@", KDDbType.DateTime, F_DATE_B)); }
            if (F_DATE_E.Length > 0) { lstParam.Add(new SqlParam("@F_DATE_E@", KDDbType.DateTime, F_DATE_E)); }
            if (queryType.Length > 0) { lstParam.Add(new SqlParam("@QueryType@", KDDbType.Int32, queryType)); }
            var ds = DBUtils.ExecuteDataSet(ctx, CommandType.StoredProcedure, "/*dialect*/" + procName, lstParam);
            return ds;
        }
        public DataSet ExecuteProc(JObject parameter)
        {
            var ctx = KDContext.Session.AppContext;
            if (ctx == null)
            {
                // 会话超时，需重新登录
                throw new Exception("ctx = null");
            }
            // 访问数据库获取用户名
            var sql = "SELECT FNAME FROM T_SEC_USER WHERE FUSERID=@FUSERID";
            var paramUserId = new Kingdee.BOS.SqlParam("@FUSERID", Kingdee.BOS.KDDbType.Int32, ctx.UserId);
            var userName = DBUtils.ExecuteScalar(ctx, sql, string.Empty, paramUserId);
            if (!userName.Equals("张培川"))
            {
                DataTable dataTable = new DataTable();
                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(dataTable);
                return dataSet;
            }



            string procName = MyConvert.ToString(parameter["procName"]);
            var lstParam = new List<SqlParam>();



            string F_DATE_B = MyConvert.ToString(parameter["@F_DATE_B@"]);
            string F_DATE_E = MyConvert.ToString(parameter["@F_DATE_E@"]);
            string f_date = MyConvert.ToString(parameter["@f_date"]);
            string f_date_b = MyConvert.ToString(parameter["@f_date_b"]);
            string f_date_e = MyConvert.ToString(parameter["@f_date_e"]);

            if (f_date.Length > 0){lstParam.Add(new SqlParam("@f_date", KDDbType.DateTime, f_date));}
            if (F_DATE_B.Length > 0){lstParam.Add(new SqlParam("@F_DATE_B@", KDDbType.DateTime, F_DATE_B));}
            if (F_DATE_E.Length > 0){lstParam.Add(new SqlParam("@F_DATE_E@", KDDbType.DateTime, F_DATE_E));}
            if (f_date_b.Length > 0) { lstParam.Add(new SqlParam("@f_date_b", KDDbType.Date, f_date_b)); }
            if (f_date_e.Length > 0) { lstParam.Add(new SqlParam("@f_date_e", KDDbType.Date, f_date_e)); }

            string WL = MyConvert.ToString(parameter["@WL@"]);
            string WLBM = MyConvert.ToString(parameter["@WLBM@"]);
            string BMMC = MyConvert.ToString(parameter["@BMMC@"]);
            string MGName = MyConvert.ToString(parameter["@MGName@"]);
            if (WL.Length > 0) { lstParam.Add(new SqlParam("@WL@", KDDbType.String, WL)); }
            if (WLBM.Length > 0) { lstParam.Add(new SqlParam("@WLBM@", KDDbType.String, WLBM)); }
            if (BMMC.Length > 0) { lstParam.Add(new SqlParam("@BMMC@", KDDbType.String, BMMC)); }
            if (MGName.Length > 0) { lstParam.Add(new SqlParam("@MGName", KDDbType.String, MGName)); }
            string FFNUMBER = MyConvert.ToString(parameter["@FFNUMBER"]);
            string FFMOBILL = MyConvert.ToString(parameter["@FFMOBILL"]);
            string MaterialGroupKey = MyConvert.ToString(parameter["@MaterialGroupKey@"]);
            string GYS = MyConvert.ToString(parameter["@GYS@"]);
            if (FFNUMBER.Length > 0) { lstParam.Add(new SqlParam("@FFNUMBER", KDDbType.String, FFNUMBER)); }
            if (FFMOBILL.Length > 0) { lstParam.Add(new SqlParam("@FFMOBILL", KDDbType.String, FFMOBILL)); }
            if (MaterialGroupKey.Length > 0) { lstParam.Add(new SqlParam("@MaterialGroupKey@", KDDbType.String, MaterialGroupKey)); }
            if (GYS.Length > 0) { lstParam.Add(new SqlParam("@GYS", KDDbType.Date, GYS)); }


            string QueryType = MyConvert.ToString(parameter["@QueryType@"]);
            string QueryKey = MyConvert.ToString(parameter["@QueryKey@"]);
            string QueryMonth = MyConvert.ToString(parameter["@QueryMonth@"]);
            if (QueryType.Length > 0){lstParam.Add(new SqlParam("@QueryType@", KDDbType.Int32, QueryType));}
            if (QueryKey.Length > 0) { lstParam.Add(new SqlParam("@QueryKey@", KDDbType.Int32, QueryKey)); }
            if (QueryMonth.Length > 0) { lstParam.Add(new SqlParam("@QueryMonth@", KDDbType.Int32, QueryMonth)); }

            var ds = DBUtils.ExecuteDataSet(ctx, CommandType.StoredProcedure, "/*dialect*/" + procName, lstParam);
            return ds;
        }
        /// <summary>
        /// 测试接口
        /// 不需要使用上下文，不需要访问数据中心，则不需要客户端登录
        /// </summary>
        /// <param name="arg1">参数1</param>
        /// <param name="arg2">参数2</param>
        /// <returns>返回请求方法和请求参数</returns>
        public object DoSth(string arg1, int arg2)
        {
            var responseDto = new
            {
                ApiName = System.Reflection.MethodInfo.GetCurrentMethod().Name,
                Args = new object[] { arg1, arg2 }
            };
            return responseDto;
        }
        public object DoSth2(string arg1, int arg2)
        {
            var ctx = KDContext.Session.AppContext;
            if (ctx == null)
            {
                // 会话超时，需重新登录
                throw new Exception("ctx = null");
            }
            // 访问数据库获取用户名
            var sql = "SELECT FNAME FROM T_SEC_USER WHERE FUSERID=@FUSERID";
            var paramUserId = new Kingdee.BOS.SqlParam("@FUSERID", Kingdee.BOS.KDDbType.Int32, ctx.UserId);
            var userName = DBUtils.ExecuteScalar(ctx, sql, string.Empty, paramUserId);
            var responseDto = new
            {
                UserName = userName,
                ApiName = System.Reflection.MethodInfo.GetCurrentMethod().Name,
                Args = new object[] { arg1, arg2 }
            };
            return responseDto;
        }


    }
}
