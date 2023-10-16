using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WovenKidExample : MonoBehaviour {
	private Animator anim;
	int Idle;
	int Run;
	int Celebrates;
	int FallSitted;

	public float Force;
    public Transform controller; 
	public Transform camera; 
    float smoothVelocity;
    public float smothTime;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		Idle = Animator.StringToHash("Idle");
		Run = Animator.StringToHash("Run");
		Celebrates = Animator.StringToHash("Celebrates");
		FallSitted = Animator.StringToHash("FallSitted");
	}

	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Debug.Log(horizontal);
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

		if (direction.magnitude == 0f)
		{
			anim.SetBool (Idle, true);                                      
			anim.SetBool (Run, false); 
			anim.SetBool (Celebrates, false);
			anim.SetBool (FallSitted, false);
		}
		else
		{
			anim.SetBool (Idle, false);                                      
			anim.SetBool (Run, true); 
			anim.SetBool (Celebrates, false);
			anim.SetBool (FallSitted, false);
		}

        if (direction.magnitude >= 0.1f) 
        {
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(controller.eulerAngles.y, rotationAngle, ref smoothVelocity, smothTime);
            controller.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 move = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;

            controller.position += (move.normalized * Force * Time.deltaTime);
        }
	}
}