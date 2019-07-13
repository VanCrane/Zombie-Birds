using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;
	private bool playerActive = false;
	private bool gameOver = false;
	private bool gameStarted = false;

	[SerializeField] private GameObject mainMenu;
	[SerializeField] private Text totalScorelbl;
	[SerializeField] private GameObject GameOverScreen;
	[SerializeField] private AudioSource mainMenuTheme;
	[SerializeField] private AudioSource levelTheme;
	[SerializeField] private AudioSource gameOverTheme;

	private float totalscore = 0f;
	



	public bool PlayerActive
	{
		get { return playerActive; }
	}

	public bool GameOver
	{
		get { return gameOver; }
	}

	public bool GameStarted
	{
		get { return gameStarted; }
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	
		else if (instance != null)
		{
			Destroy(gameObject);
		}
	
		DontDestroyOnLoad(gameObject);

		Assert.IsNotNull (mainMenu);
	}


	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	IEnumerator ScoreCoroutine()
	{
		while (gameStarted == true)
		{
			totalscore += 1 * (Time.deltaTime / 3);
			float finalScore = Mathf.Round(totalscore);
			totalScorelbl.text = finalScore.ToString();
			yield return null;
		}

	}

	public void PlayerCollided ()
	{
		gameOver = true;
		StopCoroutine(ScoreCoroutine());
		GameOverScreen.SetActive(true);
		gameStarted = false;
		playerActive = false;
		levelTheme.Stop();
		gameOverTheme.Play();
		
	}

	public void PlayerStartedGame()
	{
		playerActive = true;
		StartCoroutine(ScoreCoroutine());

	}

	public void EnterGame()
	{
		mainMenu.SetActive(false);
		gameStarted = true;
		GameOverScreen.SetActive(false);
		mainMenuTheme.Stop();
		levelTheme.Play();
	}

	public void Replay()
	{
		GameOverScreen.SetActive(false);
		SceneManager.LoadScene("SampleScene");
		mainMenu.SetActive(true);
		gameOverTheme.Stop();
		
	}


}
