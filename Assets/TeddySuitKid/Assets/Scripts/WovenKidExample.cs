using UnityEngine;
using UnityEngine.UI;
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
	
	public Vector3 startPosition;
	public GameObject loseMenu;
	public GameObject winMenu;

	// Use this for initialization
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
	}

	// Update is called once per frame
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
	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "fire")
		{
			health -= 20;
			healthText.text = $"{health}/100";
			if(health <= 0)
			{
				loseMenu.SetActive(true);
				Cursor.visible = false;
				Screen.lockCursor = false;
				healthText.text = $"{health}/100";
			}
		}
		else if(other.tag == "finish")
		{
			winMenu.SetActive(true);
		}
	}

	public void Respawn()
	{
		loseMenu.SetActive(false);
		winMenu.SetActive(false);
		
		transform.position = startPosition;

		health = 100;
		healthText.text = $"{health}/100";

		Cursor.visible = true;
        Screen.lockCursor = true;
	}
}