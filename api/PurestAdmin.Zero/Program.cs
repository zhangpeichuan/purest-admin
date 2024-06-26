// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.Zero;
using PurestAdmin.Zero.KingDee;
using Volo.Abp;

var app = await AbpApplicationFactory.CreateAsync<AdminZeroModule>();
// 初始化应用
await app.InitializeAsync();
while (true)
{
    string[] operatorArr = ["初始化种子数据", "Entity代码生成", "Service代码生成","金蝶生产入库单报表汇总"];
    Console.WriteLine("------请选择您要做的事-------");
    for (int i = 0; i < operatorArr.Length; i++)
    {
        Console.WriteLine($"{i}\t{operatorArr[i]}");
    }
    var replay = Console.ReadLine();
    switch (replay)
    {
        case "0":
            var dataSeed = app.Services.GetRequiredService<DataSeed>();
            await dataSeed.Initialization();
            break;
        case "1":
            var autoEntity = app.Services.GetRequiredService<AutoEntity>();
            autoEntity.Initialization();
            break;
        case "2":
            var autoService = app.Services.GetRequiredService<AutoService>();
            autoService.Initialization();
            break;
        case "3":
            var kingDeeService = app.Services.GetRequiredService<KingDeeService>();
            await kingDeeService.Initialization();
            break;
        default:
            Console.WriteLine("未进行任何操作！");
            break;
    };
    Thread.Sleep(1000);
}
