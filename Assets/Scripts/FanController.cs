using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    public int minAngle;
    public int maxAngle;

    public DateTime StartTime;

    private System.Random r;
    public float strength;
    public Vector3 direction;

    void Start()
    {
        r = new System.Random();
    }

    void Update()
    {
        double seconds = Math.Floor((DateTime.Now - StartTime).TotalSeconds);
        if(seconds > 2)
        {
            StartTime = DateTime.Now;
            transform.eulerAngles = new Vector3(90, r.Next(minAngle, maxAngle), 0);
            direction = new Vector3(maxAngle - transform.eulerAngles.y, transform.eulerAngles.z, transform.eulerAngles.y);
        }
    }
}
