using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pyAPI;

namespace net.mysandbox.learn_python
{
    public class Debug : MonoBehaviour
    {
        int Was_run = 0;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            switch(Was_run)
            {
                case 0:
                    BasicBehave.Say("Hello, Python");
                    break;
                case 1:
                    BasicBehave.Go_ahead();
                    break;
                case 2:
                    BasicBehave.Turn_left();
                    break;
                case 3:
                    BasicBehave.Go_backwards();
                    break;
                case 4:
                    BasicBehave.Turn_right();
                    break;
                case 5:
                    BasicBehave.Wait(2);
                    break;
                default:
                    break;
            }
            Was_run++;
        }
    }
}