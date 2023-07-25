using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 5f;
    public float collisionSlowdownFactor = 0.5f;

    private bool isPlayerInRange = false;

    private void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseSpeed * Time.deltaTime)
        {
            // Player is within reach this frame, apply slowdown
            if (isPlayerInRange)
            {
                SlowDownPlayer();
            }
        }
        else
        {
            // Move towards the player at chaseSpeed
            isPlayerInRange = true; // Make sure we know the player is in range
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        // Calculate the direction to move towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move towards the player at chaseSpeed
        transform.Translate(direction * chaseSpeed * Time.deltaTime);
    }

    private void SlowDownPlayer()
    {
        // You can modify this part according to your game mechanics
        // For demonstration purposes, let's assume the player has a Rigidbody2D component

        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();

        if (playerRigidbody != null)
        {
            // Reduce the player's velocity to simulate slowdown
            playerRigidbody.velocity *= collisionSlowdownFactor;
        }
    }
}
