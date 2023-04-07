using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private string UserName = "Tim";
    [SerializeField] int score; //holds the current player score
    [SerializeField] TextMeshProUGUI ScoreText;
    public float move_speed = 5f;
    public Rigidbody2D body;
    private Vector2 movement; //can store an x and y
    public TMP_InputField input_field;
    private Animator animator;
    private bool Paralyzed = false; //player can't move if this is true

    private void Awake()
    {
        Debug.Log("I live");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    public void foundGold()
    {
        score = score + 5;
        ScoreText.text = "Score: " + score.ToString();
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
        Paralyzed = false;
        Debug.Log(UserName);

    }

    // Update is called once per frame
    void Update()
    {
        if(Paralyzed == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        //int high = PlayerPrefs.GetInt(UserName + "HighScore", 0);
    }

    //called on a fixed interval
    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * move_speed * Time.fixedDeltaTime);
    }
}
