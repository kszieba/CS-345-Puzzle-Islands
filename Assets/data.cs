using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data : MonoBehaviour
{
	int score;
	int high_score;
	
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
