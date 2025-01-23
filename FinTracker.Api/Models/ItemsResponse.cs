using System.Collections.Generic;

namespace FinTracker.Api.Models;

public class ItemsResponse<T>
{
    public int Count => Items?.Count ?? 0;
    
    public ICollection<T> Items { get; }
    
    public ItemsResponse(ICollection<T> items)
    {
        this.Items = items;
    }
}