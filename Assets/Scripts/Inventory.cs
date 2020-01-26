using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class Inventory
{
    private readonly List<Item> inventory = new List<Item>();

    public void Add(Item item)
    {
        inventory.Add(item);
    }

    public BigInteger GetPropertySum(string key, BigInteger @default)
    {
        var values = inventory.Where(i => i.HasProperty(key)).Select(i => i[key]);
        if (!values.Any())
        {
            return @default;
        }
        
        return values.Aggregate(BigInteger.Add);
    }
}