using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryExtensions {

    public static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey){
        TValue value = dic[fromKey];
        dic.Remove(fromKey);
        dic[toKey] = value;
    }

    public static void SwapKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey)
    {
        TValue value = dic[fromKey];
        dic[fromKey] = dic[toKey];
        dic[toKey] = value;
    }
}
