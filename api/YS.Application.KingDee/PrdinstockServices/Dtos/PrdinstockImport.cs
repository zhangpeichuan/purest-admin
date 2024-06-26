// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Application.KingDee.PrdinstockServices.Dtos;
public class PrdinstockImport
{
    public DateTime F_DATE_B { get; set; }
    public DateTime F_DATE_E { get; set; }
    public const string QueryType = "1";
}