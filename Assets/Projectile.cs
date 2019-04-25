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
        if(other.gameObject.tag == "Enemy")
        {
            CameraShaker.Instance.ShakeOnce(1f, 4f, 0.1f, 0.1f);
            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.TakeDamage(1);
        }
        if(other.gameObject.tag != "Player")
            DestroyProjectile();
    }
}
