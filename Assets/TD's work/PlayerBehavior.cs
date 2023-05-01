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
	
	public int level;
    private int high;
	private string scene_name;

    private string[] destinationArray = {"t-maze", "t-maze 2", "maze_demi", "maze_demi2", 
	"k-maze", "k-maze2"};

    [SerializeField] private AudioSource goldCollectionSound;
	
	public GameObject player;
	public GameObject credits1;

    private void Awake()
    {
        
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        UserName = PlayerPrefs.GetString("UserName", null);
        level = PlayerPrefs.GetInt("Level", 0);
		score = PlayerPrefs.GetInt(UserName + "Score", 0);
        high = PlayerPrefs.GetInt(UserName + "HighScore", 0);
        //Update UI text
		ScoreText.text = "Score: " + score.ToString();
        HighScoreText.text = "High Score: " + high.ToString();

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
		PlayerPrefs.SetInt(UserName + "Score", score);
		if (scene_name != destinationArray[destinationArray.Length - 1]) //checks if last scene
		{
			level = level + 1;
			PlayerPrefs.SetInt("Level", level);
			SceneManager.LoadScene(destinationArray[level]);
		}
		else
		{
			credits1.SetActive(true);
			player.SetActive(false);
		}
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
        if (other.gameObject.tag == "TestGold")
        {
            foundGold(100);
            Destroy(other.gameObject); //get rid of gold so it can't be picked up twice
        }
        else if (other.gameObject.tag == "GFiveVal")
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
        UserName = input_field.text;
        PlayerPrefs.SetString("UserName", UserName);
        high = PlayerPrefs.GetInt(UserName + "HighScore", 0); //sets high score to match that for the chosen username
        HighScoreText.text = "High Score: " + high.ToString();

    }

    public void NewGame() //reset everything
    {
        UserName = null;
        level = 0; //now unnecessary
        score = 0;
        high = 0;
        //is this a dumb work around, is there a better way for the game not to show the score from last game upon loading the first level
        ScoreText.text = "Score: " + score.ToString();
        HighScoreText.text = "High Score: " + high.ToString();
    }
	
	public void RestartGame()
	{
		SceneManager.LoadScene(destinationArray[0]);
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
        if(UserName != null) //the player cannot move until they have entered a username
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            UpDateFacing(); //this is probably calling this method way to often since it's only needed when moving
        }
    }

    //called on a fixed interval
    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * move_speed * Time.fixedDeltaTime);
    }
}
