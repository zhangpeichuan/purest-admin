﻿// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core;

/// <summary>
/// 分页泛型集合
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedList<T>
{
    /// <summary>
    /// 页码
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// 页容量
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 总条数
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    /// 当前页集合
    /// </summary>
    public IEnumerable<T> Items { get; set; }
}

public static class PagedListExtension
{
    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static async Task<PagedList<TResult>> ToFinalPagedListAsync<TResult>(this ISugarQueryable<TResult> queryable, int pageIndex, int pageSize)
    {
        RefAsync<int> total = 0;
        var items = await queryable.ToPageListAsync(pageIndex, pageSize, total);
        var totalPages = (int)Math.Ceiling(total / (double)pageSize);
        return new PagedList<TResult>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Items = items,
            Total = total.Value,
            PageCount = totalPages,
        };
    }
    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <param name="list"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static PagedList<TResult> ToFinalPagedList<TResult>(this IList<TResult> list, int pageIndex, int pageSize)
    {
        if (list == null)
        {
            return new PagedList<TResult>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = null,
                Total = 0,
                PageCount = 0,
            };
        }
        int total = list.Count;
        var items = list?.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        var totalPages = (int)Math.Ceiling(total / (double)pageSize);
        return new PagedList<TResult>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Items = items,
            Total = total,
            PageCount = totalPages,
        };
    }
}