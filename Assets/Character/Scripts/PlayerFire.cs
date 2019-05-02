using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    void Update()
    {
        if(timeBtwShots <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(transform.localScale.x < 0)
                {
                    Instantiate(projectile, shotPoint.position, Quaternion.Euler(0, 0, 180));
                }
                else
                {
                    Instantiate(projectile, shotPoint.position, Quaternion.Euler(0, 0, 0));
                }

                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
