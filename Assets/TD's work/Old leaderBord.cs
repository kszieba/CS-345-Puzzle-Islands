using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class oldLeaderBord : MonoBehaviour
{

    /*
    private int[] leaderBord = new int[5];

    private void updateLeaderBord()
    {
        int i = 0; //index for the leaderboard
        bool placed = false; //remains false until the player is placed on the leaderboard
        string moveingName = UserName; //the name that needs to be placed
        while (i < 5 && placed == false)
        {
            string OtherName = PlayerPrefs.GetString(i.ToString() + " Name", null);
            int OtherHigh = PlayerPrefs.GetInt(OtherName + "HighScore", 0);
            Debug.Log(OtherName + " : " + OtherHigh.ToString());
            if (OtherName == null || OtherHigh < high) //if no one is in this place or the others score is lower 
            {
                PlayerPrefs.SetString(i.ToString() + " Name", moveingName);
                if (OtherName != null && OtherName != moveingName)
                {
                    //if the space was not empty then the other score must be shifted down a place
                    moveingName = OtherName;
                }
                else
                {
                    placed = true;
                }
            }
            leaderBord[i] = PlayerPrefs.GetInt((PlayerPrefs.GetString(i.ToString() + " Name", null)) + "HighScore", 0);
            i++;
        }
        for (int x = 0; x < 5; x++)
        {
            Debug.Log(x.ToString() + " : " + leaderBord[x].ToString());
        }

    }

    private void restLeaderBord() //removes all names from the leaderBord
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetString(i.ToString() + " Name", null);
            leaderBord[i] = 0;
        }

    }
    */
}
