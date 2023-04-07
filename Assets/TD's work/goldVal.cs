using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldVal : MonoBehaviour
{
    //[SerializeField] int goldToGain = 5;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //gameObject.active = false;
            Debug.Log("hit Player");
        }
    }


}
