using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5.0f;
    private Rigidbody rb;
    private int pickupCount;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;
    private Timer timer;
    private bool gameOver;

    [Header("UI")]
    public GameObject inGamePanel;
    public GameObject GameOverPanel;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //get the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //run the check pickups function
        SetCountText();
        //get the timer object 
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
        //turn on our ingame panel
        inGamePanel.SetActive(true);
        //turn off our win panel
        GameOverPanel.SetActive(false);
        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        timerText.text = "HEART RATE:" + timer.GetTime().ToString("F2");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (resetting)
            return;

        if (gameOver == true)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);




    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pick Up")
        {
            Destroy(other.gameObject);
            //decrement the pickup count
            pickupCount -= 1;
            //run the check pickups function
            SetCountText();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;   
        GetComponent<Renderer>().material.color = Color.black;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f/resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;  
    }

    void SetCountText()
    {
        //display the amount of pickups left in our scene
        scoreText.text = "BLUD CLOTS: " + pickupCount;

        if (pickupCount == 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        //Set the game over to true 
        gameOver = true;
        //stop timer
        timer.StopTimer();
        // turn on our win panel
        GameOverPanel.SetActive(true);
        //turn off our win panel
        inGamePanel.SetActive(false);
        //display the timer on the win time text
        winTimeText.text = "Your time was:" + timer.GetTime().ToString("F2");

        //set the velocirty of the rigid body to zero
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }

    public void RestartGame()

    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}