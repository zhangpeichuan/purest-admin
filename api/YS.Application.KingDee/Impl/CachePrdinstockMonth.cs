// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Core.Cache;
using PurestAdmin.Multiplex.Contracts.Consts;
using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;
using PurestAdmin.SqlSugar.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using YS.Application.KingDee.Interface;

namespace YS.Application.KingDee.Impl;
public class CachePrdinstockMonth(IAdminCache cache) : ICachePrdinstockMonth, ISingletonDependency
{
    private readonly IAdminCache _cache = cache;

    /// <summary>
    /// 获取月份生产入库数据
    /// </summary>
    /// <returns></returns>

    public List<PrdinstockMonthEntity> GetPrdinstockMonths(string yyyyMM)
    {
        return _cache.Get<List<PrdinstockMonthEntity>>(yyyyMM) ?? [];
    }

    /// <summary>
    /// 设置月份生产入库数据
    /// </summary>
    /// <param name="items"></param>
    public void SetPrdinstockMonths(List<PrdinstockMonthEntity> items,string yyyyMM)
    {
        _cache.Set(yyyyMM, items);
    }
}
