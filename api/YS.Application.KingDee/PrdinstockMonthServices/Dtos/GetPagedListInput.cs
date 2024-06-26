using PurestAdmin.Application;
namespace YS.Application.KingDee.PrdinstockMonthServices.Dtos;
public class GetPagedListInput : PaginationParams
{
    public string FDate { get; set; }
}
