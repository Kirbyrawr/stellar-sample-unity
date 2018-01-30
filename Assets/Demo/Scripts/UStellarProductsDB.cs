using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UStellarProductsDB : ScriptableObject
{
    [Serializable]
    public class ProductsDictionary : SerializableDictionary<string, UStellarProduct> { }

    public ProductsDictionary products;
}
