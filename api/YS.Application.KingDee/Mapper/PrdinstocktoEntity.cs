// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Mapster;
using PurestAdmin.SqlSugar.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YS.Application.KingDee.PrdinstockServices.Dtos;

namespace YS.Application.KingDee.PrdinstockServices.Mapper;
public class AddProinstocktoEntity : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<AddPrdinstockInput, PrdinstockEntity>()
            .IgnoreNullValues(true)
            .Map(dest => dest.Fdate, src => src.Fdate)
            .Map(dest => dest.Fname, src => src.Fname)
            .Map(dest => dest.Fnumber, src => src.Fnumber)
            .Map(dest => dest.Frealqty, src => src.Frealqty)
            .Map(dest => dest.Frealqtyunit, src => src.Frealqtyunit)
            .Map(dest => dest.Fzsl, src => src.Fzsl)
            .Map(dest => dest.Fmsl, src => src.Fmsl);
    }
}
