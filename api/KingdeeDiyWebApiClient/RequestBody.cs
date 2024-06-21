using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdeeDiyWebApiClient
{
	public record CreatedBody(List<string> NeedUpDateFields,List<string> NeedReturnFields, string SubSystemId, string InterationFlags, CreatedModel Model
		, bool IsDeleteEntry=true,bool IsVerifyBaseDataField = false,bool IsEntryBatchFill = true,bool ValidateFlag = true,bool NumberSearch=true
		,bool IsAutoAdjustField = false,bool IgnoreInterationFlag = true,bool IsControlPrecision=false,bool ValidateRepeatJson=false);
	public record SubmitBody(List<string> Numbers,string Ids,int SelectedPostId=0, int CreateOrgId = 0, bool NetworkCtrl=false,bool IgnoreInterationFlag = true);
	public record DeleteBody(List<string> Numbers, string Ids,  int CreateOrgId = 0, bool NetworkCtrl = false);
	public record AuditBody(List<string> Numbers, string Ids, int SelectedPostId = 0, int CreateOrgId = 0, bool NetworkCtrl = false, bool IgnoreInterationFlag = true);
	public record ViewBody(string Number, string Id, int CreateOrgId = 0, bool IsSortBySeq = false);
}
