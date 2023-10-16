using UnityEngine;
using System.Collections;

public class BackPackExample : MonoBehaviour {
	private Animator anim;
	int tailSwagIdle;
	int tailSwagRun;

	void Start () {
		anim = GetComponent<Animator> ();
		tailSwagIdle = Animator.StringToHash("tailSwagIdle");
		tailSwagRun = Animator.StringToHash("tailSwagRun");	
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.W)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("tailSwagIdle")) {  
				anim.SetBool (tailSwagRun, true); 
				anim.SetBool (tailSwagIdle, false);                                      
			}
		} else if (Input.GetKeyUp (KeyCode.W)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("tailSwagRun")) {
				anim.SetBool (tailSwagRun, false); 
				anim.SetBool (tailSwagIdle, true);                                      
			}
		} 
	}
}