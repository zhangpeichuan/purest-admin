// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PurestAdmin.SqlSugar.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Yitter.IdGenerator;
using System.Diagnostics;
using SqlSugar.DistributedSystem.Snowflake;

namespace PurestAdmin.Zero.KingDee;
/// <summary>
/// 金蝶导入生产入库数量实现
/// </summary>
public class KingDeeService : ISingletonDependency
{
    private readonly ISqlSugarClient _db;
    public KingDeeService(ISqlSugarClient db)
    {
        _db = db;
    }
    public async Task Initialization()
    {
        Console.WriteLine("金蝶数据初始化准备就绪");
        //_db.CodeFirst.InitTables(typeof(PrdInStockEntity));

        try
        {
            Console.WriteLine("开启事务！");
            await _db.Ado.BeginTranAsync();
            //初始化数据
            await InsertPrdInStockDicAsync(_db);
            _db.Ado.CommitTran();
            Console.WriteLine("初始化金蝶数据完成");
        }
        catch (Exception)
        {
            _db.Ado.RollbackTran();
            throw;
        }
        Console.ReadLine();
    }
    /// <summary>
    /// 插入临时字段
    /// </summary>
    /// <param name="_db"></param>
    /// <returns></returns>
    public async Task InsertPrdInStockDicAsync(ISqlSugarClient _db)
    {
        var dictPrdInStockQueryId = YitIdHelper.NextId();
        DictCategoryEntity[] categorys = [
            new DictCategoryEntity() { Id = dictPrdInStockQueryId, Code = "dict_query_type", Name = "金蝶报表查询方式" },
            ];
        await _db.Insertable(categorys).ExecuteCommandAsync();
        Console.WriteLine("初始化PrdInStock字典数据");

        DictDataEntity[] dictDatas = [
            new DictDataEntity() { CategoryId = dictPrdInStockQueryId, Name = "天" },
            new DictDataEntity() { CategoryId = dictPrdInStockQueryId, Name = "月" },
            ];
        await _db.Insertable(dictDatas).ExecuteReturnSnowflakeIdListAsync();
        Console.WriteLine("初始化KingDee字典明细数据");

        var kingdeeId = YitIdHelper.NextId();
        var prdinstockId = YitIdHelper.NextId();
        var prdinstockmonthId = YitIdHelper.NextId();

        List<FunctionEntity> functions = [
                new FunctionEntity() { Id = kingdeeId, Name = "金蝶集成", Code = "kingdee" },
                new FunctionEntity() { Id = prdinstockId, ParentId = kingdeeId, Name = "生产入库集成", Code = "kingdee.prdinstock" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = prdinstockId, Name = "生产入库新增", Code = "kingdee.prdinstock.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = prdinstockId, Name = "生产入库编辑", Code = "kingdee.prdinstock.edit" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = prdinstockId, Name = "生产入库查看", Code = "kingdee.prdinstock.view" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = prdinstockId, Name = "生产入库删除", Code = "kingdee.prdinstock.delete" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = prdinstockId, Name = "生产入库导入", Code = "kingdee.prdinstock.import" },
                new FunctionEntity() { Id = prdinstockmonthId, ParentId = kingdeeId, Name = "生产入库月集成", Code = "kingdee.prdinstockmonth" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = prdinstockmonthId, Name = "生产入库月新增", Code = "kingdee.prdinstockmonth.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = prdinstockmonthId, Name = "生产入库月编辑", Code = "kingdee.prdinstockmonth.edit" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = prdinstockmonthId, Name = "生产入库月查看", Code = "kingdee.prdinstockmonth.view" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = prdinstockmonthId, Name = "生产入库月删除", Code = "kingdee.prdinstockmonth.delete" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = prdinstockmonthId, Name = "生产入库月导入", Code = "kingdee.prdinstockmonth.import" },
            ];
        await _db.Insertable(functions).ExecuteCommandAsync();
        Console.WriteLine("初始化KingDee功能数据");
    }
}
