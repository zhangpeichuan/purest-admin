
using KingdeeDiyWebApiClient;
using Mapster;
using Newtonsoft.Json.Linq;
using PurestAdmin.Core.Oops;
using PurestAdmin.Multiplex.AdminUser;
using PurestAdmin.Multiplex.Contracts.Enums;
using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.SqlSugar;
using PurestAdmin.SqlSugar.Entity;
using SqlSugar;
using System.Collections.Generic;
using System.Diagnostics;
using Volo.Abp.Application.Services;
using YS.Application.KingDee.Interface;
using YS.Application.KingDee.PrdinstockMonthServices.Dtos;

namespace YS.Application.KingDee.PrdinstockMonthServices;
/// <summary>
/// PrdinstockMonth服务
/// </summary>
public class PrdinstockMonthService(ISqlSugarClient db, ICachePrdinstockMonth cachePrdinstockMonth, Repository<PrdinstockMonthEntity> prdmonthRepository) : ApplicationService
{

    private readonly ISqlSugarClient _db = db;
    private readonly Repository<PrdinstockMonthEntity> _prdmonthRepository = prdmonthRepository;
    private readonly ICachePrdinstockMonth _cachePrdinstockMonth = cachePrdinstockMonth;

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<PrdinstockMonthOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<PrdinstockMonthEntity>()
              .WhereIF(!input.FDate.IsNullOrEmpty(), x => x.Fdate.Contains(input.FDate))
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<PrdinstockMonthOutput>>();
    }
    public async Task<int> ImportPrdMonthFromKingDee(PrdinstockMonthImport monthImport)
    {
        var cacheLists= _cachePrdinstockMonth.GetPrdinstockMonths(monthImport.F_DATE_B);
        if (cacheLists.Count() >0)
        {
            return cacheLists.Count();
        }
        GetPagedListInput input = new GetPagedListInput();
        input.FDate = monthImport.F_DATE_B.Substring(0, 4)+ monthImport.F_DATE_B.Substring(5, 2);
        var pagedList = await _db.Queryable<PrdinstockMonthEntity>()
              .WhereIF(!input.FDate.IsNullOrEmpty(), x => x.Fdate.Equals(input.FDate)).ToListAsync();
        //数据库没有数据进行导入
        if(pagedList.Count() == 0)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("@F_DATE_B@", monthImport.F_DATE_B);
            dictionary.Add("@QueryType@", PrdinstockMonthImport.QueryType);
            List<object> args = new List<object> { dictionary };
            var resultString = KingDeeExtension.DoPrdinstockWebApi(args);
            // 解析JSON字符串为JObject  
            JObject jsonObject = JObject.Parse(resultString);
            JArray jsonArray = (JArray)jsonObject["Table"];
            var list = jsonArray.Select(obj => new PrdinstockMonthEntity
            {
                Fdate = obj["FDATE"].ToString(),
                Fnumber = obj["FNUMBER"].ToString(),
                Fname = obj["FNAME"].ToString(),
                Frealqty = (double)obj["FREALQTY"],
                Frealqtyunit = (double)obj["FREALQTYUnit"],
                Fzsl = (double)obj["fzsl"],
                Fmsl = (double)obj["fmsl"],
                Remark = "ImportPrdMonthFromKingDee"
            }).ToList();
            // 记录开始时间  
            DateTime startTime = DateTime.Now;
            // 执行批量插入  

            var rowsAffected = await _prdmonthRepository.InsertReturnSnowflakeIdAsync(list); // 返回影响的行数  
                                                                                              // 记录结束时间  
            DateTime endTime = DateTime.Now;
            if (rowsAffected.Count() > 0)
            {
                Console.WriteLine("批量插入成功，插入了 {0} 行数据。", rowsAffected.Count());

                // 计算执行时间（毫秒）  
                TimeSpan elapsedTime = endTime - startTime;
                double milliseconds = elapsedTime.TotalMilliseconds;

                // 输出执行时间  
                Console.WriteLine("插入操作执行时间: " + milliseconds + " 毫秒");

                Console.WriteLine("----------");

            }
            return rowsAffected.Count();
        }
        //已有数据不需导入
        else
        {
            _cachePrdinstockMonth.SetPrdinstockMonths(pagedList, monthImport.F_DATE_B);
            return pagedList.Count();
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
