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
    private bool grounded = true; 
    

    //Controllers
    GameController gameController;
    CameraController cameraController;
    SoundController soundController;

    [Header("UI")]
    public GameObject inGamePanel;
    public GameObject GameOverPanel;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;
    private object Wall;



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
        gameController = FindObjectOfType<GameController>();
        cameraController = FindObjectOfType<CameraController>();
        soundController = FindObjectOfType<SoundController>();
    }

    private void Update()
    {
        timerText.text = "HEART RATE:" + timer.GetTime().ToString("F2");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (gameController.controlType == ControlType.WorldTilt)
            return;

        if (resetting)
            return;

        if (gameOver == true)
            return;

      

        if (grounded)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);


            if (cameraController.cameraStyle == CameraController.CameraStyle.Free)
            {
                //Rotates the player to the direction of the camera
                transform.eulerAngles = Camera.main.transform.eulerAngles;
                //Translates the unput vectors into coordinates
                movement = gameObject.transform.TransformDirection(movement);
            }

            rb.AddForce(movement * speed * Time.deltaTime);
        }
       
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
            soundController.PLayPickupSound();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = false;
    }

    private void onCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            soundController.PlayCollisionSound(collision.gameObject);
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
        soundController.PLayWinSound();

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