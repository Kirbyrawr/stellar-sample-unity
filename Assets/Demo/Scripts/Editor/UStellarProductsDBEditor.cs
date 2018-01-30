using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(UStellarProductsDB))]
public class UStellarProductsDBEditor : Editor
{
    private UStellarProductsDB instance;
    private IEnumerable<KeyValuePair<string, UStellarProduct>> productsOrdered;
    private string newProductID = string.Empty;


    [MenuItem("Window/UStellar/Create Products DB")]
    public static void Create()
    {
        //Create instance
        UStellarProductsDB productsDB = (UStellarProductsDB)CreateInstance(typeof(UStellarProductsDB));

        //Open Save Panel
        string path = EditorUtility.SaveFilePanelInProject("Save products DB", "", "asset", "");

        //Save if the path is not empty
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(productsDB, path);
        }
    }

    private void OnEnable()
    {
        instance = (UStellarProductsDB)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Products", EditorStyles.boldLabel);

        productsOrdered = instance.products.OrderByDescending(kvp => kvp.Key).Reverse();

        //Draw Products
        foreach (var pair in productsOrdered)
        {
            DrawProduct(pair);
            GUILayout.Space(5);
        }

        //Draw Add button
        DrawCreateNewProduct();

        if(GUI.changed) 
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(instance);
        }

        Repaint();
    }

    public void DrawProduct(KeyValuePair<string, UStellarProduct> pair)
    {
        string originalID = pair.Key;

        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.BeginHorizontal();
        //Draw Remove button
        GUI.color = Color.red;
        if (GUILayout.Button("-", GUILayout.ExpandWidth(false)))
        {
            instance.products.Remove(originalID);
        }
        GUI.color = Color.white;
        EditorGUILayout.EndHorizontal();

        EditorGUI.BeginChangeCheck();
        pair.Value.id = EditorGUILayout.DelayedTextField("ID", pair.Value.id);
        if (EditorGUI.EndChangeCheck())
        {
            if (instance.products.ContainsKey(pair.Value.id))
            {
                Debug.LogError("There is already a product with the same ID");
            }
            else
            {
                instance.products.RenameKey(originalID, pair.Value.id);
            }
        }

        pair.Value.currency = EditorGUILayout.TextField("Currency", pair.Value.currency);
        pair.Value.amount = EditorGUILayout.TextField("Amount", pair.Value.amount);
        EditorGUILayout.EndVertical();
    }

    private void DrawCreateNewProduct()
    {
        EditorGUILayout.LabelField("Add New Product", EditorStyles.boldLabel);
        newProductID = EditorGUILayout.TextField("ID", newProductID);

        if (GUILayout.Button("Create"))
        {
            //Create the product and assing the id
            UStellarProduct product = new UStellarProduct();
            product.id = newProductID;

            //Add the product to the dictionary
            instance.products.Add(newProductID, product);

            //Reset the field
            newProductID = string.Empty;
            GUIUtility.keyboardControl = 0;
        }
    }

}
