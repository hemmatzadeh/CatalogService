using System;
using System.Collections.Generic;
using System.Text;

namespace FH.CatalogService.Application.Specifications.Products;

public class ProductSpecPrams
{
    private const int MaxPageSize = 50;
    public int pageIndex { get; set; } = 1;
    private int _pageSize = 6;
    public int pageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
    public int? categoryId { get; set; }

    public double? minPrice { get; set; }
    public double? maxPrice { get; set; }
    public string? sort { get; set; }
    public string _search;
    public string Search
    {
        get => _search;
        set => _search = value.ToLower();
    }

}