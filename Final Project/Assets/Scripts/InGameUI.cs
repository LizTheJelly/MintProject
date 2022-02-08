using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private TextMeshProUGUI livesCount;
    [SerializeField]
    private TextMeshProUGUI healthCount;

    // When a new game level is first loaded, it sets the InGameUI life counter to the current
    // player's lives. I learned this method in the Unity Documentation: SceneManager.sceneLoaded.
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int playerStartLives = GameStateManager.GetPlayerLives;
        InitializeLevel(playerStartLives);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.onLevelInit += InitializeLevel;
        PlayerLivesHealthManager playerObj = player.GetComponentInParent<PlayerLivesHealthManager>();
        playerObj.onLevelInitHealth += InitializeHealth;
        int playerStartHealth = playerObj.GetHealth;
        InitializeHealth(playerStartHealth);
    }

    // Updates the InGameUI Player's life counter to currentlives
    private void InitializeLevel(int currentLives)
    {
        livesCount.text = currentLives.ToString();
    }

    private void InitializeHealth(int currentHealth)
    {
        healthCount.text = currentHealth.ToString();
    }
}
