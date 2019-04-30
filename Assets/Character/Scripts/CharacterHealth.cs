using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;

    private void Start()
    {
        health = maxHealth;
        // DontDestroyOnLoad(health);
    }

    private void Update()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHearts;
            }
            else
            {
                hearts[i].sprite = emptyHearts;
            }

            if(i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
