using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseController : MonoBehaviour
{
    public GameObject loseMenu;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            loseMenu.SetActive(true);
            Cursor.visible = false;
            Screen.lockCursor = false;
        }
    }
}
