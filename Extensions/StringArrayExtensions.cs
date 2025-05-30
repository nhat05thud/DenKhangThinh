﻿using Examine;
using Examine.Search;

namespace DenKhangThinh.Extensions;

public static class StringArrayExtensions
{
    public static IExamineValue[]? Fuzzy(this string[] terms, float fuzziness = 0.5f)
    {
        if (terms == null) return null;

        List<IExamineValue> values = new List<IExamineValue>();
        foreach (var item in terms)
        {
            values.Add(item.Fuzzy(fuzziness));
        }

        return values.ToArray();
    }

    public static IExamineValue[]? Boost(this string[] terms, float boost)
    {
        if (terms == null) return null;

        List<IExamineValue> values = new List<IExamineValue>();
        foreach (var item in terms)
        {
            values.Add(item.Boost(boost));
        }

        return values.ToArray();
    }

    public static IExamineValue[]? MultipleCharacterWildcard(this string[] terms)
    {
        if (terms == null) return null;

        List<IExamineValue> values = new List<IExamineValue>();
        foreach (var item in terms)
        {
            values.Add(item.MultipleCharacterWildcard());
        }

        return values.ToArray();
    }
}