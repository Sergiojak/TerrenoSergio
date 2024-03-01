using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMenus : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
