
namespace YS.Application.KingDee.PrdinstockServices.Dtos;
public class PrdinstockOutput
{
	/// <summary>
	/// 主键Id
	/// </summary>
	public string Id { get; set; }
	/// <summary>
	/// 日期
	/// </summary>
	public string Fdate { get; set; }
	/// <summary>
	/// 物料编码
	/// </summary>
	public string Fnumber { get; set; }
	/// <summary>
	/// 物料名称
	/// </summary>
	public string Fname { get; set; }
	/// <summary>
	/// 实际数量
	/// </summary>
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
	public string Frealqtyunit { get; set; }
}
