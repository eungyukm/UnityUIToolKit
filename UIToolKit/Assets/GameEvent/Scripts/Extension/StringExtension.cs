using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class StringExtensions
{
    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source?.IndexOf(toCheck, comp) >= 0;
    }

    public static string EraseWhiteSpace(this string source)
    {
        var erased = string.Concat(source.Where(c => !char.IsWhiteSpace(c)));
        return erased;
    }
    
    public static string GetPath(this Component component) {
        return component.transform.GetPath() + "/" + component.GetType().ToString();
    }
    
    public static string GetPath(this Transform current) {
        if (current.parent == null) return current.name;
        return current.parent.GetPath() + "/" + current.name;
    }
}
