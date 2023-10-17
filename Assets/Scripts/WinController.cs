using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
    public GameObject winMenu;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            winMenu.SetActive(true);
    }
}
