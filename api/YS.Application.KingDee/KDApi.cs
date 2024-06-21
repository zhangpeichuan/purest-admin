// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Application.KingDee;
public class KDApi
{
    /// <summary>
    /// AcctID
    /// </summary>
    public string AcctID { get; set; }
    /// <summary>
    /// UserName
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// AppID
    /// </summary>
    public string AppID { get; set; }
    /// <summary>
    /// AppSec
    /// </summary>
    public string AppSec { get; set; }
    /// <summary>
    /// LCID
    /// </summary>
    public int LCID { get; set; }
    /// <summary>
    /// 金蝶webapi地址
    /// </summary>
    public string ServerUrl { get; set; }
}
