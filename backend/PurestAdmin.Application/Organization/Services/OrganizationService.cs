// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.Auth.Services;
using PurestAdmin.Application.Organization.Dtos;

namespace PurestAdmin.Application.Organization.Services;
/// <summary>
/// 组织机构服务
/// </summary>
public class OrganizationService : IOrganizationService, ITransient
{
    private readonly ISqlSugarClient _db;
    private readonly Repository<OrganizationEntity> _organizationRepository;
    private readonly IAuthService _authService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    /// <param name="organizationRepository"></param>
    /// <param name="authService"></param>
    public OrganizationService(ISqlSugarClient db, Repository<OrganizationEntity> organizationRepository, IAuthService authService)
    {
        _db = db;
        _organizationRepository = organizationRepository;
        _authService = authService;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<OrganizationProfile>> GetPagedListAsync(GetPagedListInput input)
    {
        var treeList = await _authService.GetOrganizationTreeAsync();
        var children = treeList.First().Children;
        return children.ToFinalPagedList(input.PageIndex, input.PageSize);
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<OrganizationProfile> GetAsync(long id)
    {
        var entity = await _organizationRepository.GetByIdAsync(id);
        return entity.Adapt<OrganizationProfile>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(AddOrganizationInput input)
    {
        var entity = input.Adapt<OrganizationEntity>();
        return await _organizationRepository.InsertReturnSnowflakeIdAsync(entity);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task PutAsync(long id, AddOrganizationInput input)
    {
        var entity = await _organizationRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        var newEntity = input.Adapt(entity);
        _ = await _organizationRepository.UpdateAsync(newEntity);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _organizationRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        await _organizationRepository.DeleteAsync(entity);
    }

}
