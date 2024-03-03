using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utillities
{
    public static List<T> SortOrder<T>(List<T> list, int amount)
    {
        return list.OrderBy(d => System.Guid.NewGuid()).Take(amount).ToList();
    }
}
