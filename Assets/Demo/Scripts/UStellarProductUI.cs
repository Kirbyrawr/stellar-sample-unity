using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UStellarProductUI : MonoBehaviour 
{
	public string productID;
	public Text priceLabel;
	private UStellarProduct product;

	public void Start() 
	{
		product = UStellarManager.GetProduct(productID);
		priceLabel.text = string.Concat(product.amount, " ", product.currency.ToUpper());
	}

	public void OnClickProduct()
    {
        //Buy
        UStellarManager.Buy(productID);
    }
}
