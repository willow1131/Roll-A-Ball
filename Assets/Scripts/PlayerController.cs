using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public float speed = 5.0f;
    private Rigidbody rb;
    private int pickupCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //get the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //run the check pickups function
        CheckPickups();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal,0, moveVertical);
        rb.AddForce(movement * speed);

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pick Up")
        {
            Destroy(other.gameObject);
            //decrement the pickup count
            pickupCount -= 1;
           //run the check pickups function
           CheckPickups();
        }
 
    }

    void CheckPickups()
    {
        //print the amount of pickups left in our scene
        print("PickUps Left: " + pickupCount);

        if (pickupCount == 0)
        {
            print("yippi");
        }
    }
}
