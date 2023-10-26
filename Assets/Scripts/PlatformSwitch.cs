using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlatformSwitch : MonoBehaviour
{

    private PlatformEffector2D effector;
    public float waitTime;
    public float resetTime;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Downward();
        JumpReset();
        TimerReset();
    }

    private void Downward()
    {
        //timer input S selama 0.1 detik
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTime = 0.1f;
        }
        //jika timer kurang dari sama dengan 0 detik, rotation menjadi 180
        if (Input.GetKey(KeyCode.S))
        {
            if (waitTime <= 0.09f)
            {
                effector.rotationalOffset = 180f;
            }

            else
            {
                //jika player melepas tekanan S kurang dari waitTime maka timer menjadi semula
                waitTime -= Time.deltaTime;
            }
        }
        
    }

    private void JumpReset()
    {
        //input space rotation kembali semula
        if (Input.GetKey(KeyCode.Space))
        {
            effector.rotationalOffset = 0;
        }
    }

    private void TimerReset()
    {
        if (waitTime >= 0.1f)
        {
            effector.rotationalOffset = 0;
        }
    }

}
