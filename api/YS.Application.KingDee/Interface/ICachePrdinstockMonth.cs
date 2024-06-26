// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;
using PurestAdmin.SqlSugar.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Application.KingDee.Interface;
public interface ICachePrdinstockMonth
{
    List<PrdinstockMonthEntity> GetPrdinstockMonths(string yyyyMM);

    void SetPrdinstockMonths(List<PrdinstockMonthEntity> items,string yyyyMM);
}