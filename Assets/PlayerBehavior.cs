using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerBehavior: MonoBehaviour
{
    //text fields for score scroll
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI topScoreText;

    public float moveSpeed = 5f; //public keyword allows speed to be easily edited
    public Rigidbody2D body;
    private Vector2 movement; //can store an x and y

    public TMP_InputField inputField;

    private Animator animator;

    //scoring variables
    private string userName; //This is the user name used to call variables like score from player PlayerPrefs
    [SerializeField] int score; //holds the current player score
    public int level;
    private int high;
    private string bestPlayer;
    private int bigTop;
    private string sceneName;

	//array of scenes, which should contain all scenes in order
    private string[] sceneArray = {"1-1", "1-2", "2-1", "2-2",
	"3-1", "3-2"};
    
    //audio sources for sound effects
    [SerializeField] private AudioSource goldCollectionSound;
    [SerializeField] private AudioSource levelWinSound;

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
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + high.ToString();
        topScoreText.text = "Best: " + bestPlayer + ", " + bigTop.ToString();

		sceneName = SceneManager.GetActiveScene().name;
		//sets level to correct integer if at start of game or testing single scene
        if (sceneArray[level] != sceneName)
		{
			for (int i = 0; i < sceneArray.Length; i++)
			{
				if (sceneArray[i] == sceneName)
				{
					level = i;
				}
			}
		}

    }
	
	/*
	This method is called when gold is added to the player's score.
	*/
    private void FoundGold(int value)
    {
        goldCollectionSound.Play();
        score = score + value;//add the value of the gold found to score
        scoreText.text = "Score: " + score.ToString();
        updateHighScore();

    }

	/*
	This method is called when the player finds the crate at the end of a scene.
	*/
	private void ReachedDestination()
	{
        levelWinSound.Play();
        PlayerPrefs.SetInt(userName + "Score", score);
		if (sceneName != sceneArray[sceneArray.Length - 1]) //checks if not last scene
		{
			level = level + 1;
			PlayerPrefs.SetInt("Level", level);
			SceneManager.LoadScene(sceneArray[level]);
		}
		else
		{
			credits.SetActive(true);
			player.SetActive(false);
		}
	}

	/*
	This method updates the high score if necessary.
	*/
    private void updateHighScore()
    {
        //the high score will not carry across computers
        if(score > PlayerPrefs.GetInt(userName + "HighScore", 0))
        {
            PlayerPrefs.SetInt(userName + "HighScore", score);
            high = PlayerPrefs.GetInt(userName + "HighScore", 0);
            highScoreText.text = "High Score: " + high.ToString();
            if(high > bigTop)
            {
                PlayerPrefs.SetString("bestPlayer", userName);
                PlayerPrefs.SetInt("BestScore", high);
                topScoreText.text = "Best: " + userName + ", " + high.ToString();
                bestPlayer = userName;
                bigTop = high;
            }
        }
    }

	/*
	This method is called when the player collides with an object.
	*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "GFiveVal")
        {
            FoundGold(5);
            Destroy(other.gameObject); //get rid of gold so it can't be picked up twice
        }
		else if (other.gameObject.tag == "Destination")
		{
			ReachedDestination();
		}
    }

	/*
	This method reads the username from the input field. It is called from the input field.
	*/
    public void ReadStringInput()
    {
        userName = inputField.text;
        PlayerPrefs.SetString("userName", userName);
		score = 0;
		scoreText.text = "Score: " + score.ToString();
        high = PlayerPrefs.GetInt(userName + "HighScore", 0); //sets high score to match that for the chosen user name
        highScoreText.text = "High Score: " + high.ToString();

    }

	/*
	This method is called from outside this script to start a new game.
	*/
    public void NewGame() //reset everything
    {
        userName = null;
        score = 0;
        high = 0;
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + high.ToString();
    }

	/*
	This method should be called by the button on the last page of the credits. It loads the first scene.
	*/
	public void RestartGame()
	{
		SceneManager.LoadScene(sceneArray[0]);
	}

	/*
	This method is called when necessary to update the direction the player is facing.
	*/
    private void UpdateFacing()
    {
        //gives the animator the directions the player is currently moving in
        animator.SetFloat("x", movement.x);
        animator.SetFloat("y", movement.y);

    }

    // Update is called once per frame
    void Update()
    {
        if(userName != null) //the player cannot move until they have entered a user name
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
			if (movement.x != 0 || movement.y != 0)
			{
				UpdateFacing();
			}
        }
    }

    //called on a fixed interval
    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
