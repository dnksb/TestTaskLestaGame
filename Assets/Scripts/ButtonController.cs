using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool charge;
    public bool hit;
    public bool wait;

    public Material mat1;
    public Material mat2;
    public Material mat3;

    public DateTime StartTime;

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            double seconds = Math.Floor((DateTime.Now - StartTime).TotalSeconds);
            if(wait)
            {
                if(seconds > 5)
                {
                    StartTime = DateTime.Now;
                    GetComponent<MeshRenderer>().material = mat2;
                    
                    charge = true;
                    hit = false;
                    wait = false;
                }
            }
            else if(charge)
            {
                if(seconds > 1)
                {
                    StartTime = DateTime.Now;
                    GetComponent<MeshRenderer>().material = mat3;

                    hit = true;
                    charge = false;
                    wait = false;
                }
            }
            else if(hit)
            {
                if(seconds > 0.01)
                {
                    StartTime = DateTime.Now;
                    GetComponent<MeshRenderer>().material = mat1;
                    other.GetComponent<WovenKidExample>().Hit();

                    wait = true;
                    hit = false;
                    charge = false;
                }
            }
        }
    }
}
