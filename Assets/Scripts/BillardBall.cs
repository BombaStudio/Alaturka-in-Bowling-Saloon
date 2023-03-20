using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillardBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wins")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SteamSystem>().win();
        }
    }
}
