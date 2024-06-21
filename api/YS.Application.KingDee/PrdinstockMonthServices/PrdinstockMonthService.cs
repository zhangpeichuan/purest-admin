
using KingdeeDiyWebApiClient;
using Mapster;
using Newtonsoft.Json.Linq;
using PurestAdmin.Core.Oops;
using PurestAdmin.Multiplex.Contracts.Enums;
using PurestAdmin.SqlSugar;
using PurestAdmin.SqlSugar.Entity;
using SqlSugar;
using System.Diagnostics;
using Volo.Abp.Application.Services;
using YS.Application.KingDee.PrdinstockMonthServices.Dtos;

namespace YS.Application.KingDee.PrdinstockMonthServices;
/// <summary>
/// PrdinstockMonth服务
/// </summary>
public class PrdinstockMonthService(ISqlSugarClient db, Repository<PrdinstockMonthEntity> prdmonthRepository) : ApplicationService
{

    private readonly ISqlSugarClient _db = db;
    private readonly Repository<PrdinstockMonthEntity> _prdmonthRepository = prdmonthRepository;



    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<PrdinstockMonthOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<PrdinstockMonthEntity>().ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<PrdinstockMonthOutput>>();
    }
    public async void ImportPrdMonthFromKingDee()
    {
        KDApi kdApi = KingDeeExtension.kdApi;
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
            var lists = new List<PrdinstockMonthEntity>();
            foreach (JObject obj in jsonArray)
            {
                PrdinstockMonthEntity entity = new PrdinstockMonthEntity();
                string FDATE = obj["FDATE"].ToString();
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
                entity.Remark = "ImportPrdMonthFromKingDee";
                lists.Add(entity);
            }
            Console.WriteLine(lists.Count());
            // 记录开始时间  
            DateTime startTime = DateTime.Now;
            // 执行批量插入  

            var rowsAffected = await _prdmonthRepository.InsertReturnSnowflakeIdAsync(lists); // 返回影响的行数  
                                                                      // 记录结束时间  
            DateTime endTime = DateTime.Now;
            if (rowsAffected.Count() > 0)
            {
                Debug.WriteLine("批量插入成功，插入了 {0} 行数据。", rowsAffected);

                // 计算执行时间（毫秒）  
                TimeSpan elapsedTime = endTime - startTime;
                double milliseconds = elapsedTime.TotalMilliseconds;

                // 输出执行时间  
                Debug.WriteLine("插入操作执行时间: " + milliseconds + " 毫秒");

                Debug.WriteLine("----------");

            }
        }
    }
    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<PrdinstockMonthOutput> GetAsync(long id)
    {
        var entity = await _db.Queryable<PrdinstockMonthEntity>().FirstAsync(x => x.Id == id);
        return entity.Adapt<PrdinstockMonthOutput>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(AddPrdinstockMonthInput input)
    {
        var entity = input.Adapt<PrdinstockMonthEntity>();
        return await _db.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task PutAsync(long id, PutPrdinstockMonthInput input)
    {
        var entity = await _db.Queryable<PrdinstockMonthEntity>().FirstAsync(x => x.Id == id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        var newEntity = input.Adapt(entity);
        _ = await _db.Updateable(newEntity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _db.Queryable<PrdinstockMonthEntity>().FirstAsync(x => x.Id == id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        _ = await _db.Deleteable(entity).ExecuteCommandAsync();
    }
}
