
using System.ComponentModel.DataAnnotations;

namespace YS.Application.KingDee.PrdinstockMonthServices.Dtos;
public class AddPrdinstockMonthInput
{
	/// <summary>
	/// 日期
	/// </summary>
	[Required(ErrorMessage = "日期不能为空"), MaxLength(20, ErrorMessage = "日期最大长度为：20")]
	public string Fdate { get; set; }
	/// <summary>
	/// 物料编码
	/// </summary>
	[Required(ErrorMessage = "物料编码不能为空"), MaxLength(50, ErrorMessage = "物料编码最大长度为：50")]
	public string Fnumber { get; set; }
	/// <summary>
	/// 物料名称
	/// </summary>
	[Required(ErrorMessage = "物料名称不能为空"), MaxLength(50, ErrorMessage = "物料名称最大长度为：50")]
	public string Fname { get; set; }
	/// <summary>
	/// 实际数量
	/// </summary>
	[Required(ErrorMessage = "实际数量不能为空"), MaxLength(19, ErrorMessage = "实际数量最大长度为：19")]
	public string Frealqty { get; set; }
	/// <summary>
	/// 分子数量
	/// </summary>
	public string Fzsl { get; set; }
	/// <summary>
	/// 分母数量
	/// </summary>
	public string Fmsl { get; set; }
	/// <summary>
	/// 实际单位数量
	/// </summary>
	[Required(ErrorMessage = "实际单位数量不能为空"), MaxLength(19, ErrorMessage = "实际单位数量最大长度为：19")]
	public string Frealqtyunit { get; set; }
}
