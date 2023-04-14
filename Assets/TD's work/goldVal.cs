using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldVal : MonoBehaviour
{
    //[SerializeField] int scoreValue = 5;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit ? A");
        if (other.gameObject.tag == "Player")
        {
            //gameObject.active = false;
            Debug.Log("hit Player A");
        }
    }

}
