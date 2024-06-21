namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 生产入库月表
/// </summary>
[SugarTable("T_PRDINSTOCK_MONTH")]
public partial class PrdinstockMonthEntity : BaseEntity
{
	/// <summary>
	/// 日期
	/// </summary>
	[SugarColumn(ColumnName = "FDATE")]
	public string Fdate { get; set; }
	/// <summary>
	/// 物料编码
	/// </summary>
	[SugarColumn(ColumnName = "FNUMBER")]
	public string Fnumber { get; set; }
	/// <summary>
	/// 物料名称
	/// </summary>
	[SugarColumn(ColumnName = "FNAME")]
	public string Fname { get; set; }
	/// <summary>
	/// 实际数量
	/// </summary>
	[SugarColumn(ColumnName = "FREALQTY")]
	public double Frealqty { get; set; }
	/// <summary>
	/// 分子数量
	/// </summary>
	[SugarColumn(ColumnName = "fzsl")]
	public double Fzsl { get; set; }
	/// <summary>
	/// 分母数量
	/// </summary>
	[SugarColumn(ColumnName = "fmsl")]
	public double Fmsl { get; set; }
	/// <summary>
	/// 实际单位数量
	/// </summary>
	[SugarColumn(ColumnName = "FREALQTYUnit")]
	public double Frealqtyunit { get; set; }
}