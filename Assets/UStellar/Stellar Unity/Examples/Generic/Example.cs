using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Example : MonoBehaviour
{
    public abstract string id
    {
        get;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public abstract void Run();
}
