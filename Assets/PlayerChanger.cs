using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChanger : MonoBehaviour
{
    GameObject player_1;
    GameObject player_2;

    private float timeBtwChange;
    public float startTimeBtwChange;

    void Start()
    {
        player_1 = GameObject.Find("Player");
        player_2 = GameObject.Find("Player_2");
    }

    void Update()
    {
        if(timeBtwChange <= 0)
        {

        }
    }
}
