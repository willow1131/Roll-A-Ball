using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public bool isPaused;

    
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            Debug.Log("Attempting to unpause");
        }
        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            Debug.Log("Attempting to pause");
        }

    }
    IEnumerator WaitForTogglePanel()
    {
        yield return new WaitForSeconds(0.1f);
        if(isPaused)
        {
            isPaused = false;

        }
        else
        {
            isPaused = true;
            
        }
    }
    
}
