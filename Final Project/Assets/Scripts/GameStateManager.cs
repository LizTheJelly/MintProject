using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager _instance;
    [SerializeField]
    private List<string> levels = new List<string>();
    [SerializeField]
    private string mainMenuScene;
    [SerializeField]
    private string winningScene;
    [SerializeField]
    private string gameOverScene;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private string gameObjTag;

    // Audio
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip backgroundMusic;


    // Player's Lives
    [SerializeField]
    private int startingNumLives;
    private static int playerLives;

    private static bool spawnPlayer = false;
    private static float timer;
    private static bool stopTimer;

    enum GAMESTATE
    {
        MAINMENU,
        PAUSED,
        PLAYING,
        WINNING,
        GAMEOVER
    }
    private static GAMESTATE state;

    public delegate void InitializeLevel(int currentLives);

    public static InitializeLevel onLevelInit;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            state = GAMESTATE.MAINMENU;
            timer = 0;
            stopTimer = true;
            playerLives = _instance.startingNumLives;
            DontDestroyOnLoad(_instance.pauseMenu);
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu != null)
            {
                GameStateManager.Pause();
            }
        }

        // If timer is not stopped:
        if (!stopTimer)
        {
            timer += (Time.deltaTime * 1); 
        }
    }

    public static void NewGame()
    {
        state = GAMESTATE.PLAYING;
        playerLives = _instance.startingNumLives;
        stopTimer = false;
        PlayerPrefs.DeleteAll();
        _instance.audioSource.PlayOneShot(_instance.backgroundMusic);
        if (_instance.levels.Count > 0)
        {
            SceneManager.LoadScene(_instance.levels[0]);
        }
    }

    public static void ResumeGame()
    {
        if (PlayerPrefs.HasKey("playerLives"))
        {
            state = GAMESTATE.PLAYING;
            timer = PlayerPrefs.GetFloat("timer");
            stopTimer = false;
            playerLives = PlayerPrefs.GetInt("playerLives");
            _instance.audioSource.PlayOneShot(_instance.backgroundMusic);
            if (_instance.levels.Count > 0)
            {
                SceneManager.LoadScene(_instance.levels[0]);
            }
        }
    }

    public static void GoToMainMenu()
    {
        state = GAMESTATE.MAINMENU;
        stopTimer = true;
        _instance.audioSource.Stop();
        GameStateManager.Pause();
        SceneManager.LoadScene(_instance.mainMenuScene);
    }

    public static void GoToGameOver()
    {
        state = GAMESTATE.GAMEOVER;
        stopTimer = true;
        _instance.audioSource.Stop();
        GameStateManager.Pause();
        SceneManager.LoadScene(_instance.gameOverScene);
    }

    public static void GoToWinningMenu()
    {
        state = GAMESTATE.WINNING;
        stopTimer = true;
        _instance.audioSource.Stop();
        GameStateManager.Pause();
        SceneManager.LoadScene(_instance.winningScene);
    }

    // The way I save progress is using PlayerPrefs. I saved the player's position, lives, health, and time they
    // spent so far in the game.
    public static void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag(_instance.gameObjTag);
        if (player != null)
        {
            PlayerLivesHealthManager playerObj = player.GetComponentInParent<PlayerLivesHealthManager>();
            Vector2 playerPos = player.transform.position;
            PlayerPrefs.SetFloat("playerPosX", playerPos.x);
            PlayerPrefs.SetFloat("playerPosY", playerPos.y);
            PlayerPrefs.SetInt("playerLives", playerLives);
            PlayerPrefs.SetInt("playerHealth", playerObj.GetHealth);
            PlayerPrefs.SetFloat("timer", timer);
            PlayerPrefs.Save();
        }
    }

    // Pause() pauses the game and activate the pause menu if GAMESTATE is PLAYING.
    // If GAMESTATE is currently MAINMENU, it closes the pause menu.
    public static void Pause()
    {
        if (state == GAMESTATE.PLAYING)
        {
            state = GAMESTATE.PAUSED;
            Time.timeScale = 0;
            stopTimer = true;
            _instance.audioSource.Pause();
            if (_instance.pauseMenu != null)
            {
                _instance.pauseMenu.SetActive(true);
            }
        }
        else if (state == GAMESTATE.MAINMENU || state == GAMESTATE.WINNING 
            || state == GAMESTATE.GAMEOVER)
        {
            Time.timeScale = 1;
            stopTimer = true;
            if (_instance.pauseMenu != null)
            {
                _instance.pauseMenu.SetActive(false);
            }
        }
        else
        {
            state = GAMESTATE.PLAYING;
            Time.timeScale = 1;
            stopTimer = false;
            _instance.audioSource.UnPause();
            if (_instance.pauseMenu != null)
            {
                _instance.pauseMenu.SetActive(false);
            }
        }
    }

    // GetPlayerLives solely get and return the current playerLives
    public static int GetPlayerLives
    {
        get
        {
            return playerLives;
        }
        private set
        {}
    }

    // playerLoseLife() subtracts a life from player's lives counter and 
    // checks if lives counter is below 0. If below 0, gameover.
    public static void PlayerLoseLife()
    {
        playerLives--;
        spawnPlayer = true;
        if (playerLives < 0)
        {
            GameStateManager.GoToGameOver();
        }

        if (onLevelInit != null)
        {
            onLevelInit(playerLives);
        }
    }

    public static bool IsPauseActive
    {
        get
        {
            if (state == GAMESTATE.PAUSED)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private set
        {}
    }

    public static float GetTimer
    {
        get
        {
            return timer;
        }
        private set
        { }
    }

    public static bool GetSpawnBool
    {
        get 
        {
            return spawnPlayer;
        }
        set 
        {
            spawnPlayer = value;
        }
    }
}
