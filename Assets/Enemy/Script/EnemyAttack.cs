using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 1.0f; // Time in seconds between each Attack
    public int attackDamage = 10;

    GameObject player; // reference to players
    CharacterHealth playerHealth; // reference to player's health
    bool playerInRange; //whether player is within the trigger collider and can be attacked
    float timer; // time for counting up to the next attack

    void Awake()
    {
        //Setting up the references
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<CharacterHealth>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If the entering collider is the player
        if (other.gameObject == player)
        {
            // player is in range
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //If the exiting collider is the player...
        if (other.gameObject == player)
        {
            //the player is no longer in range
            playerInRange = false;
        }
    }

    void Update()
    {
        player = GameObject.Find("Player");
        
        if (!player)
        {
            player = GameObject.Find("Player_2");
            playerHealth = player.GetComponent<CharacterHealth>();
        }
        else
        {
            playerHealth = player.GetComponent<CharacterHealth>();
        }

        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange)
        {
            attack();
        }
    }

    void attack()
    {
        // Reset the timer
        timer = 0f;
        playerHealth.TakeDamage(attackDamage, transform.position);
    }
}
