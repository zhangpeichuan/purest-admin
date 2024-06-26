// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Application.KingDee;
public static class KDWebApiConst
{
   public const string AbstractBusinessServiceNameSpace = "Ys.Scm.PrdInStockPlugIn.WebApi";
    public const string AbstractBusinessServiceClassName = "PrdInStock";
    public const string AbstractBusinessServiceMethedName = "ExecuteService";
    public const string ExecuteProcMethedName = "ExecuteProc";
    public const string PrdinstockWebAPI = AbstractBusinessServiceNameSpace + "."
        + AbstractBusinessServiceClassName + "." + AbstractBusinessServiceMethedName +
        ","+ AbstractBusinessServiceNameSpace;
    public const string ExecuteProcWebAPI = AbstractBusinessServiceNameSpace + "."
    + AbstractBusinessServiceClassName+ "." + ExecuteProcMethedName +
    "," + AbstractBusinessServiceNameSpace;
}
