using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private string UserName = "Tim";
    [SerializeField] int score;
    [SerializeField] TextMeshProUGUI ScoreText;
    public float move_speed = 10f;
    public Rigidbody2D body;
    Vector2 movement; //can store an x and y
    public TMP_InputField input_field;

    public void foundGold()
    {
        score = score + 5;
        ScoreText.text = score.ToString();
        updateHighScore();

        

    }

    private void updateHighScore()
    {
        //the high score will not carry across computers
        if(score > PlayerPrefs.GetInt(UserName + "HighScore", 0))
        {
            PlayerPrefs.SetInt(UserName + "HighScore", score);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.gameObject.tag == "TestGold")
        {
            foundGold();
        }
    }

    public void ReadStringInput()
    {
        Debug.Log(UserName);
        UserName = input_field.text;
        Debug.Log(UserName);
        //transform.Find("InputField (TMP)").gameObject.setActive(false);
        //transform.GetChild("InputField (TMP)").gameObject.SetActive(false);
        //gameObject.GetChild("childname").SetActive(false);
        //Debug.Log(gameObject.GetChild(0));

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //int high = PlayerPrefs.GetInt(UserName + "HighScore", 0);
        //Debug.Log(high);
    }

    //called on a fixed interval
    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * move_speed * Time.fixedDeltaTime);
    }
}
