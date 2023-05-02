using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerBehavior: MonoBehaviour
{
    //text fields for score scroll
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI HighScoreText;
    [SerializeField] TextMeshProUGUI TopScoreText;

    public float move_speed = 5f; //public keyword allows speed to be easily edited
    public Rigidbody2D body;
    private Vector2 movement; //can store an x and y

    public TMP_InputField input_field;

    private Animator animator;

    //scoring variables
    private string userName; //This is the user name used to call variables like score from player PlayerPrefs
    [SerializeField] int score; //holds the current player score
    public int level;
    private int high;
    private string bestPlayer;
    private int bigTop;
    private string scene_name;


    private string[] destinationArray = {"1-1", "1-2", "2-1", "2-2",
	"3-1", "3-2"};
    
    //audio sources for sound effects
    [SerializeField] private AudioSource goldCollectionSound;
    [SerializeField] private AudioSource LevelWinSound;

    public GameObject player;
	public GameObject credits; //this will be null in any scene except the last scene

    private void Awake()
    {

        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        userName = PlayerPrefs.GetString("userName", null);
        level = PlayerPrefs.GetInt("Level", 0);
		score = PlayerPrefs.GetInt(userName + "Score", 0);
        high = PlayerPrefs.GetInt(userName + "HighScore", 0);
        //Update UI text
        bestPlayer = PlayerPrefs.GetString("bestPlayer", "none");
        bigTop = PlayerPrefs.GetInt("BestScore", 0);

        //Update UI text
        ScoreText.text = "Score: " + score.ToString();
        HighScoreText.text = "High Score: " + high.ToString();
        TopScoreText.text = "Best: " + bestPlayer + ", " + bigTop.ToString();

		scene_name = SceneManager.GetActiveScene().name;
		//sets level to correct integer if at start of game or testing single scene
        if (destinationArray[level] != scene_name)
		{
			for (int i = 0; i < destinationArray.Length; i++)
			{
				if (destinationArray[i] == scene_name)
				{
					level = i;
				}
			}
		}

    }

    public void foundGold(int value)
    {
        goldCollectionSound.Play();
        score = score + value;//add the value of the gold found to score
        ScoreText.text = "Score: " + score.ToString();
        updateHighScore();

    }

	public void reachedDestination()
	{
        LevelWinSound.Play();
        PlayerPrefs.SetInt(userName + "Score", score);
		if (scene_name != destinationArray[destinationArray.Length - 1]) //checks if not last scene
		{
			level = level + 1;
			PlayerPrefs.SetInt("Level", level);
			SceneManager.LoadScene(destinationArray[level]);
		}
		else
		{
			credits.SetActive(true);
			player.SetActive(false);
		}
	}

    private void updateHighScore()
    {
        //the high score will not carry across computers
        if(score > PlayerPrefs.GetInt(userName + "HighScore", 0))
        {
            PlayerPrefs.SetInt(userName + "HighScore", score);
            high = PlayerPrefs.GetInt(userName + "HighScore", 0);
            HighScoreText.text = "High Score: " + high.ToString();
            if(high > bigTop)
            {
                PlayerPrefs.SetString("bestPlayer", userName);
                PlayerPrefs.SetInt("BestScore", high);
                TopScoreText.text = "Best: " + userName + ", " + high.ToString();
                bestPlayer = userName;
                bigTop = high;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "GFiveVal")
        {
            foundGold(5);
            Destroy(other.gameObject); //get rid of gold so it can't be picked up twice
        }
		else if (other.gameObject.tag == "Destination")
		{
			reachedDestination();
		}
    }

    public void ReadStringInput()
    {
        userName = input_field.text;
        PlayerPrefs.SetString("userName", userName);
        high = PlayerPrefs.GetInt(userName + "HighScore", 0); //sets high score to match that for the chosen username
        HighScoreText.text = "High Score: " + high.ToString();

    }

    public void NewGame() //reset everything
    {
        userName = null;
        score = 0;
        high = 0;
        //is this a dumb work around, is there a better way for the game not to show the score from last game upon loading the first level
        ScoreText.text = "Score: " + score.ToString();
        HighScoreText.text = "High Score: " + high.ToString();
    }

	/*
	This method should be called by the button on the last page of the credits. It loads the first scene.
	*/
	public void RestartGame()
	{
		SceneManager.LoadScene(destinationArray[0]);
	}

    private void UpdateFacing()
    {
        //gives the animator the directions the player is currently moving in
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("x", movement.x);
            animator.SetFloat("y", movement.y);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(userName != null) //the player cannot move until they have entered a username
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            UpdateFacing(); //this is probably calling this method way to often since it's only needed when moving
        }
    }

    //called on a fixed interval
    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * move_speed * Time.fixedDeltaTime);
    }
}
