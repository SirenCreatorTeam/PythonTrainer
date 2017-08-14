using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pyAPI;
using System;

namespace net.mysandbox.learn_python
{
    public class __behave : MonoBehaviour
    {
        public Queue<string> func = new Queue<string>();
        public Queue<string> Say_text = new Queue<string>();
        public Queue<float> sec = new Queue<float>();
        // Use this for initialization
        void Start()
        {
            StartCoroutine(Process());
        }

        // Update is called once per frame
        IEnumerator Process()
        {
            yield return new WaitForSeconds(4.0f);
            BasicBehave.TextField.SetActive(false);
            while (true)
            {
                if (func.Count == 0)
                {
                    func.Enqueue("stop");
                }
                string cor = func.Dequeue();
                if (cor != "stop" && cor != "Wait" && cor != "Say")
                {
                    StartCoroutine(cor);
                }
                if(cor == "Say")
                {
                    StartCoroutine(Say());
                    yield return new WaitForSeconds(2f);
                }
                if (sec.Count != 0 && cor == "Wait")
                {
                    yield return new WaitForSeconds(sec.Dequeue());
                }
                else
                {
                    yield return new WaitForSeconds(1.0f);
                }
            }
        }

        void Go_ahead()
        {
            transform.Translate(transform.TransformVector(Vector3.forward));
            //Debug.print("waiting breaked");
        }

        void Go_backwards()
        {
            transform.Translate(transform.TransformVector(Vector3.back));
            //Debug.print("waiting breaked");
        }

        void Turn_right()
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 30f, 0f);
            //Debug.print("waiting breaked");
        }

        void Turn_left()
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y - 30f, 0f);
            //Debug.print("waiting breaked");
        }

        IEnumerator Say()
        {
            var txt = Say_text.Dequeue();
            if((int)(txt.Length / 16) > 0)
            {
                for (int c = 0; c < (int)(txt.Length / 16); c++)
                {
                    txt = txt.Insert(15 + 16 * c, "\n");
                }
            }
            BasicBehave.textmesh.text = txt;
            BasicBehave.TextField.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            //Debug.print("waiting breaked");
            BasicBehave.TextField.SetActive(false);
            //Debug.print("waiting breaked");
        }
    }
}
