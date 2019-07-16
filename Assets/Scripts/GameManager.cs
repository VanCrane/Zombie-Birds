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
	[SerializeField] public Text totalScorelbl;
	[SerializeField] private Text totalScorelblGO;
	[SerializeField] private GameObject GameOverScreen;
	[SerializeField] private AudioSource mainMenuTheme;
	[SerializeField] private AudioSource levelTheme;
	[SerializeField] private AudioSource gameOverTheme;
	[SerializeField] private AudioSource screamOfDeath;

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
			totalScorelblGO = totalScorelbl;
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
		totalScorelbl.rectTransform.localPosition = new Vector3(0f, -462f, 0f);
		screamOfDeath.Play();


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
		Destroy(totalScorelbl);
		

	}


}
