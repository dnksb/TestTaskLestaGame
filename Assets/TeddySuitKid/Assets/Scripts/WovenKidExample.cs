using UnityEngine;
using UnityEngine.UI;
using System;
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
	
	int health;
	public Text healthText;
	public Text winText;
	
	public Vector3 startPosition;
	public GameObject loseMenu;
	public GameObject winMenu;

	public DateTime StartTime;

	public bool inWindZone;
	public GameObject windZone;

	void Start () {
		anim = GetComponent<Animator> ();
		Idle = Animator.StringToHash("Idle");
		Run = Animator.StringToHash("Run");
		Celebrates = Animator.StringToHash("Celebrates");
		FallSitted = Animator.StringToHash("FallSitted");
		
		transform.position = startPosition;
		health = 100;
		healthText.text = $"{health}/100";

		Cursor.visible = true;
        Screen.lockCursor = true;

		StartTime = DateTime.Now;
	}

	void Update () {
		float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
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
		controller.position -= inWindZone ? (windZone.GetComponent<FanController>().direction * windZone.GetComponent<FanController>().strength) * Time.deltaTime : Vector3.zero;
	}

	public void Hit()
	{
		health -= 20;
		if(health <= 0)
		{
			loseMenu.SetActive(true);
			GetComponent<WovenKidExample>().enabled = false;

			Cursor.visible = false;
			Screen.lockCursor = false;			
		}
		healthText.text = $"{health}/100";
	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "fire")
		{
			Hit();
		}
		else if(other.tag == "Finish")
		{
			var ts = DateTime.Now - StartTime;
			winText.text = "Победа!\nвремя: " + Math.Floor(ts.TotalSeconds).ToString() + "с";
			
			winMenu.SetActive(true);
			GetComponent<WovenKidExample>().enabled = false;

			Cursor.visible = false;
            Screen.lockCursor = false;
		}
		if(other.tag == "windArea")
		{
			windZone = other.gameObject;
			inWindZone = true;
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if(other.tag == "windArea")
		{
			inWindZone = false;
			windZone = null;
		}
	}

	public void Respawn()
	{
		loseMenu.SetActive(false);
		winMenu.SetActive(false);
		GetComponent<WovenKidExample>().enabled = true;

		transform.position = startPosition;

		health = 100;
		healthText.text = $"{health}/100";

		Cursor.visible = true;
        Screen.lockCursor = true;
		StartTime = DateTime.Now;
	}
}