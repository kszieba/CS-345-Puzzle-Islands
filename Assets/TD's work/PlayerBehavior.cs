using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerBehavior: MonoBehaviour
{
    private string UserName; //This is the user name used to call variables like score from player PlayerPrefs
    [SerializeField] int score; //holds the current player score
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI HighScoreText;
    public float move_speed = 5f;
    public Rigidbody2D body;
    private Vector2 movement; //can store an x and y
    public TMP_InputField input_field;
    private Animator animator;
    //private bool Paralyzed = true; //player can't move if this is true
	public GameObject data_object;
	
	public int level;
    private int high;

    private string[] destinationArray = {"t-maze", "t-maze 2", "maze_demi", "maze_demi2", 
	"k-maze"};

    private void Awake()
    {
        
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        UserName = PlayerPrefs.GetString("UserName", null);
        level = PlayerPrefs.GetInt("Level", 0);
		score = PlayerPrefs.GetInt(UserName + "Score", 0);
        high = PlayerPrefs.GetInt(UserName + "HighScore", 0);
        if (SceneManager.GetActiveScene().name == "t-maze")
		{
            Debug.Log("bro im in t-maze");
		}
        //Update UI text
		ScoreText.text = "Score: " + score.ToString();
        HighScoreText.text = "High Score: " + high.ToString();

        Debug.Log("I live: " + UserName + level + score);

    }

    public void foundGold(int value)
    {
        score = score + value;//add the value of the gold found to score
        ScoreText.text = "Score: " + score.ToString();
        updateHighScore();

    }
	
	public void reachedDestination()
	{
		PlayerPrefs.SetInt(UserName + "Score", score);
		level = level + 1;
		PlayerPrefs.SetInt("Level", level);
		SceneManager.LoadScene(destinationArray[level]);
	}

    private void updateHighScore()
    {
        //the high score will not carry across computers
        if(score > PlayerPrefs.GetInt(UserName + "HighScore", 0))
        {
            PlayerPrefs.SetInt(UserName + "HighScore", score);
            int high = PlayerPrefs.GetInt(UserName + "HighScore", 0);
            HighScoreText.text = "High Score: " + high.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        if (other.gameObject.tag == "TestGold")
        {
            foundGold(100);
            Destroy(other.gameObject); //get rid to gold so it can't be picked up twice
        }
        else if (other.gameObject.tag == "GFiveVal")
        {
            foundGold(5);
            Destroy(other.gameObject); //get rid to gold so it can't be picked up twice
        }
		else if (other.gameObject.tag == "Destination")
		{
			reachedDestination();
		}
    }

    public void ReadStringInput()
    {
        //Debug.Log(UserName);
        UserName = input_field.text;
        PlayerPrefs.SetString("UserName", UserName);
        //Paralyzed = false;
        Debug.Log(UserName);
        //load high score, dose doing this on awaken make doing it here pointless?
        high = PlayerPrefs.GetInt(UserName + "HighScore", 0);
        HighScoreText.text = "High Score: " + high.ToString();

    }

    public void NewGame() //reset everything
    {
        Debug.Log("New Game!");
        UserName = null;
        level = 0;
        score = 0;
        high = 0;
        //is this a dum work around, is there a better way for the game not to show the score from last game upon loading the first level
        ScoreText.text = "Score: " + score.ToString();
        HighScoreText.text = "High Score: " + high.ToString();
    }

    private void UpDateFacing()
    {
        if(movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("x", movement.x);
            animator.SetFloat("y", movement.y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UserName != null) //the player can not move until they have entered a username
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            UpDateFacing(); //this is probably calling this method way to often since its only needed when moveing
        }
    }

    //called on a fixed interval
    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * move_speed * Time.fixedDeltaTime);
    }
}
