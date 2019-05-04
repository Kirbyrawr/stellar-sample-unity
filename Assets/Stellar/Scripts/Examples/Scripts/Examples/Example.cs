using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UStellar.Examples
{
    public abstract class Example
    {
        public abstract int id
        {
            get;
        }

        public abstract string title
        {
            get;
        }

        public abstract string description
        {
            get;
        }

        public void Open()
        {
            ExampleUI.GetInstance().Open(this);
        }

        public void Close()
        {
            ExampleUI.GetInstance().Close();
        }

        public void SetExample()
        {
            ExampleUI.GetInstance().SetExample(this);
        }

        public void Log(string message, int newline = 1)
        {
            ExampleUI.GetInstance().Log(message, newline);
        }

        public virtual void Run()
        {
            ExampleUI.GetInstance().ClearLog();
        }
    }
}