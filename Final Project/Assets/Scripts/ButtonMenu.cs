using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// The class ButtonMenu handles what a button click does in the title menu
// and pause menu.
public class ButtonMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject saveText;
    [SerializeField]
    private float timeShowMsg;
    public void OnClickNewGame()
    {
        GameStateManager.NewGame();
    }

    public void OnClickResumeGame()
    {
        GameStateManager.ResumeGame();
    }

    public void OnClickMainTitle()
    {
        GameStateManager.GoToMainMenu();
        saveText.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickSaveGame()
    {
        GameStateManager.SaveGame();
        StartCoroutine(PopUpMessage());   
    }

    // After the user presses the save button, a text will pop up and notified the user progress was saved.
    private IEnumerator PopUpMessage()
    {
        saveText.SetActive(true);
        yield return new WaitForSeconds(timeShowMsg);
    }
}
