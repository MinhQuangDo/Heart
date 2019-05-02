using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        if(gameObject)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject boss = GameObject.Find("Boss");
        GameObject boss2 = GameObject.Find("Boss2");
        if (boss || boss2)
        {
            if(other.gameObject.tag == "Player")
            {
                CharacterHealth player = other.GetComponent<CharacterHealth>();
                if(player)
                {
                    CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);
                    player.TakeDamage(1, transform.position);
                    DestroyProjectile();
                }
            }
        }

        if(other.gameObject.tag == "Enemy")
        {
            CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);
            WallEnemyAI e = other.GetComponent<WallEnemyAI>();
            NEnemy e2 = other.GetComponent<NEnemy>();
            FlyEnemy e3 = other.GetComponent<FlyEnemy>();
            if (e)
            {
                CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);
                e.TakeDamage(1);
            }
            if (e2)
            {
                CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);
                e2.TakeDamage(1);
            }
            if (e3)
            {
                CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);
                e3.TakeDamage(1);
            }
        }
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Boss")
            DestroyProjectile();
    }
}
