using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMainMenu : MonoBehaviour
{

    public void startGameButton()
    {
        SceneManager.LoadScene(0);
    }
    public void exitGameButton() 
    { 
        Application.Quit();
    }


}