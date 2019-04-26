using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerChanger : MonoBehaviour
{
    GameObject player_1;
    GameObject player_2;

    Rigidbody2D rb1;
    Rigidbody2D rb2;

    private float timeBtwChange;
    public float startTimeBtwChange;

    void Start()
    {
        player_1 = GameObject.Find("Player");
        player_2 = GameObject.Find("Player_2");

        rb1 = player_1.GetComponent<Rigidbody2D>();
        rb2 = player_2.GetComponent<Rigidbody2D>();

        player_2.SetActive(false);

        timeBtwChange = startTimeBtwChange;
    }

    void Update()
    {
        if(timeBtwChange <= 0)
        {
            if(player_1.activeSelf == true && player_2.activeSelf == false)
            {
                rb2.velocity = rb1.velocity;

                player_2.SetActive(true);
                player_1.SetActive(false);

                CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);

                player_2.transform.position = player_1.transform.position;

                GameObject.Find("CameraHolder").GetComponent<CameraFollow>().target = player_2.transform;

            }
            else if(player_1.activeSelf == false && player_2.activeSelf == true)
            {
                rb1.velocity = rb2.velocity;

                player_1.SetActive(true);
                player_2.SetActive(false);

                CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);

                player_1.transform.position = player_2.transform.position;

                GameObject.Find("CameraHolder").GetComponent<CameraFollow>().target = player_1.transform;
            }

            timeBtwChange = startTimeBtwChange;
        }
        else
        {
            timeBtwChange -= Time.deltaTime;
        }
    }
}
