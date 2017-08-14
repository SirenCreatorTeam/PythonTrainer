using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IronPython.Hosting;
using System.IO;
using pyAPI;

namespace net.mysandbox.learn_python
{
    public class Pyengine
    {

        // Use this for initialization
        public static void Execute()
        {
            string scr;
            try {
                using (StreamReader sr = new StreamReader(Application.persistentDataPath + "/temp.py", System.Text.Encoding.UTF8))
                {
                    scr = sr.ReadToEnd();
                    scr = BasicBehave.prescript + scr;
                    scr = scr
                        .Replace("print", "nlps.Say")
                        .Replace("Go_ahead", "nlps.Go_ahead")
                        .Replace("Go_backwards", "nlps.Go_backwards")
                        .Replace("Turn_left", "nlps.Turn_left")
                        .Replace("Turn_right", "nlps.Turn_right")
                        .Replace("Wait", "nlps.Wait");
                }
            }
            catch (IOException)
            {
                using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/temp.py", false, System.Text.Encoding.UTF8))
                {
                    sw.Write("#Enter your code here...");
                    return;
                }
            }
            Debug.print("Now executing code is " + scr);
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            var sorce = engine.CreateScriptSourceFromString(scr);

            sorce.Execute(scope);
        }
    }
}