using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedBoost : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float boostedSpeed = 10f;
    public float boostDuration = 2f;
    private float currentSpeed;
    private bool isBoostActive = false;

    private void Start()
    {
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isBoostActive)
        {
            StartSpeedBoost();
        }

        if (isBoostActive)
        {
            boostDuration -= Time.deltaTime;

            if (boostDuration <= 0f)
            {
                EndSpeedBoost();
            }
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement = movement.normalized * currentSpeed * Time.deltaTime;

        transform.Translate(movement);
    }

    private void StartSpeedBoost()
    {
        currentSpeed = boostedSpeed;
        isBoostActive = true;
        // You can add any visual or audio effects here to indicate the speed boost is active.
    }

    private void EndSpeedBoost()
    {
        currentSpeed = normalSpeed;
        isBoostActive = false;
        boostDuration = 2f;
        // You can reset any visual or audio effects here when the speed boost ends.
    }
}
