using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // will change our scene to the string passed in
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    // Reloads the current scene we are in
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    //loads title scene. must be called title exaclty
    public void ToTitleScene()
    {
        GameController.instance.controlType = ControlType.Normal;
        SceneManager.LoadScene("Title");
    }

    //gets our active scenes name
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //quits our game 
    public void QuitGame()
    {
        Application.Quit();
    }
}
