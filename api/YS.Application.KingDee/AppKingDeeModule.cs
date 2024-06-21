// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Core.Mapster;
using PurestAdmin.Multiplex;
using PurestAdmin.SqlSugar;
using System.Reflection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using YS.Application.KingDee.PrdinstockServices;

namespace YS.Application.KingDee;
[DependsOn(typeof(AdminSqlSugarModule),
    typeof(AdminMultiplexModule))]
public class AppKingDeeModule:AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapsterIRegister(Assembly.GetExecutingAssembly());
        context.Services.AddKingDeeService();
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            
            options.ConventionalControllers.Create(typeof(AppKingDeeModule).Assembly, opts =>
            {
                opts.RootPath = "v1";
                //opts.UrlActionNameNormalizer = (action) =>
                //{
                //    return action.ActionNameInUrl;
                //};
            });
        });
        base.ConfigureServices(context);
    }
}
