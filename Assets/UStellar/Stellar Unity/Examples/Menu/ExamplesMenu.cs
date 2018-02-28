using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplesMenu : MonoBehaviour 
{
	public List<Example> examples = new List<Example>();
	private Example currentExample;

	public void Awake() 
	{
		UStellar.Core.UStellarManager.SetStellarTestNetwork();
        UStellar.Core.UStellarManager.Init();
	}

	public void ReturnToMenu() 
	{
		currentExample.Close();
		gameObject.SetActive(true);
	}

	//Create Account
	public void OnClickExample(string id) 
	{
		OpenExample(id);
	}

	private void OpenExample(string id) 
	{
		for (int i = 0; i < examples.Count; i++)
		{
			if(examples[i].id == id) 
			{
				currentExample = examples[i];
				gameObject.SetActive(false);
				currentExample.Open();
				break;
			}
		}
	}
	
}
