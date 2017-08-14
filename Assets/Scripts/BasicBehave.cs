using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using net.mysandbox.learn_python;


namespace pyAPI
{
    public static class BasicBehave
    {
#if UNITY_EDITOR
        private static string path = Application.dataPath + "/../Temp/bin/Debug/Assembly-CSharp.dll";
#else
        private static string path = Application.dataPath + "/Managed/Assembly-CSharp.dll";
#endif
        public static readonly string prescript = $@"
#-*-coding:utf-8;-*-
import clr
clr.AddReferenceByPartialName('UnityEngine')
import UnityEngine as ue
path = '{path}'
clr.AddReferenceToFile(path)
from pyAPI import BasicBehave as nlps

";
        public static GameObject plobe;
        public static Transform trfm;
        public static GameObject TextField;
        public static __behave mover;
        public static TextMesh textmesh;

        public static void init()
        {
            plobe = GameObject.FindGameObjectWithTag("Main");
            mover = plobe.GetComponent<__behave>();
            trfm = plobe.transform;
            TextField = GameObject.Find("TextArea");
            textmesh = TextField.transform.GetChild(0).GetComponent<TextMesh>();
        }
        
        public static void Go_ahead()
        {
            mover.func.Enqueue("Go_ahead");
        }

        public static void Go_backwards()
        {
            mover.func.Enqueue("Go_backwards");
        }

        public static void Turn_right()
        {
            mover.func.Enqueue("Turn_right");
        }

        public static void Turn_left()
        {
            mover.func.Enqueue("Turn_left");
        }

        public static void Say(string text)
        {
            mover.func.Enqueue("Say");
            mover.Say_text.Enqueue(text);
        }

        public static void Wait(float sec)
        {
            mover.func.Enqueue("Wait");
            mover.sec.Enqueue(sec);
        }
    }
}