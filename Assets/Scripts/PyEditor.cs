using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using pyAPI;

namespace net.mysandbox.learn_python
{
    class PyEditor: MonoBehaviour
    {
        private string code;
        public GUISkin skin;
        public bool Is_Pausing = false;
        private bool In_Editing = false;
        private bool Edited = false;
        public float[] rect = {0f, 0f, 0f, 0f};
        public float[] wrect = {0, 0, 0f, 0f};
        public float lowest = 0;
        public float defbutsiz;

        void Start()
        {
            /*
            print(BasicBehave.prescript);
            print("Show default datapath below:");
            print(" Data_Path: " + Application.dataPath);
            print(" Persistent_Data_Path: " + Application.persistentDataPath);
            print(" Streaming_Data_Path: " + Application.streamingAssetsPath);
            print(" Temporary_Cache_Path: " + Application.temporaryCachePath);
            */
            skin.button.fontSize = (int)(Screen.width / 6 / 5);
            rect[2] = Screen.width / 6;
            rect[3] = Screen.height / 8;
            wrect[2] = Screen.width / 4 * 3;
            wrect[3] = Screen.height / 4 * 3;
            wrect[0] = Screen.width / 2 - wrect[2] / 2;
            wrect[1] = Screen.height / 2 - wrect[3] / 2;
            defbutsiz = wrect[3] * 0.2f / 2;
            try
            {
                using (StreamReader sr = new StreamReader(Application.persistentDataPath + "/temp.py", System.Text.Encoding.UTF8))
                {
                    code = sr.ReadToEnd();
                }
            }catch(IOException)
            {
                code = "#Enter your code here...";
            }
            BasicBehave.init();
            Pyengine.Execute();
        }

        void OnGUI()
        {
            GUI.skin = skin;
            if (!Is_Pausing)
            {
                Time.timeScale = 1;
                if (GUI.Button(new Rect(rect[0], rect[1], rect[2], rect[3]), "pause menu"))
                {
                    Is_Pausing = true;
                }
            }
            else
            {
                Time.timeScale = 0;
                GUI.Window(1, new Rect(wrect[0], wrect[1], wrect[2], wrect[3]), WindowGUI, "Pause Menu");
            }
        }

        void WindowGUI(int wid)
        {
            if (!In_Editing)
            {
                if (GUI.Button(getRect(4, 0), "Continue"))
                {
                    Is_Pausing = false;
                    if (Edited)
                    {
                        print("Your code now executing...");
                        Pyengine.Execute();
                        Edited = false;
                    }
                }
                if (GUI.Button(getRect(4, 1), "Edit Script"))
                {
                    In_Editing = true;
                }
                if (GUI.Button(getRect(4, 2), "Reload World"))
                {
                    SceneManager.LoadScene("main");
                }
                if (GUI.Button(getRect(4, 3), "Exit to Desktop"))
                {
                    Application.Quit();
                }
            }
            else
            {
                code = GUI.TextArea(new Rect(wrect[2] / 16, lowest + defbutsiz / 2, wrect[2] / 8 * 7, wrect[3] - lowest - defbutsiz), code);
                if (GUI.Button(getEditRect(defbutsiz, 2, 1, 0), "Write Script as New"))
                {
                    code = "#Enter your code here...";
                }
                if (GUI.Button(getEditRect(defbutsiz, 2, 2, 0), "Cancel and Exit Edit Mode"))
                {
                    In_Editing = false;
                }
                if (GUI.Button(getEditRect(defbutsiz, 1, 1, 1), "Save and Exit Edit Mode"))
                {
                    In_Editing = false;
                    Edited = true;
                    using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/temp.py", false, System.Text.Encoding.UTF8))
                    {
                        sw.Write(code);
                    }
                }
            }
        }
        Rect getEditRect(float h, int w, int p, int n)
        {
            float wid = wrect[2] * 0.9f / w;
            if (lowest < (h + 10f) * (n + 1))
            {
                lowest = (h + 10f) * (n + 1);
            }
            return new Rect(wrect[2] / (w * 2) * (2 * p - 1) - wid / 2, (h + 10) * n + 20, wid, h);
        }
        Rect getRect(int x, int n)
        {
            float siz = ((wrect[3] - 20) / x) / 3 * 2;
            float wid = wrect[2] / 2;
            return new Rect(wrect[2] / 2 - wid / 2, ((wrect[3] - 20) / x) * n + siz / 2, wid, siz);
        }
    }
}
