using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UStellar.Examples
{
    public class ExampleUI : MonoBehaviour
    {
        private static ExampleUI m_instance;

        public CanvasGroup root;
        public Text titleText;
        public Text descriptionText;
        public Text logText;
        public Button runButton;

        public void Awake()
        {
            SetInstance();
            Close();
        }

        private void SetInstance()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else if (m_instance != this)
            {
                Destroy(gameObject);
            }
        }

        public static ExampleUI GetInstance()
        {
            return m_instance;
        }

        public void Open(Example example)
        {
            SetExample(example);
            root.alpha = 1;
            root.blocksRaycasts = true;
        }

        public void Close()
        {
            root.alpha = 0;
            root.blocksRaycasts = false;
            runButton.onClick.RemoveAllListeners();
        }

        public void SetExample(Example example)
        {
            logText.text = "";
            titleText.text = string.Concat("EXAMPLE - ", example.title);
            descriptionText.text = example.description;
            runButton.onClick.AddListener(example.Run);
        }

        public void Log(string message, int newline = 1)
        {
            for (int i = 0; i < newline; i++)
            {
                message = string.Concat(System.Environment.NewLine, message);
            }

            logText.text += message;
        }

        public void ClearLog()
        {
            logText.text = "";
        }
    }
}