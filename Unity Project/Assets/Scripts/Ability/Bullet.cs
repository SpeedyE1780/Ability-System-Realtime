using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerController target;
    Vector3 direction;
    float speed;
    int damage;

    private void OnEnable()
    {
        EventManager.gameOver += DestroyBullet;
    }

    private void OnDisable()
    {
        EventManager.gameOver -= DestroyBullet;
    }

    //Set the bullet's target and damage
    public void InitializeBullet(PlayerController t , int dmg , Vector3 Pos)
    {
        speed = 5;
        this.transform.position = Pos;
        target = t;
        damage = dmg;
        direction = (target.transform.position - this.transform.position).normalized;
    }

    //Destroy bullets on gameover
    void DestroyBullet(GameObject gameObject)
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Move the bullet towards its target
        this.transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Bullet collided with target
        if (other.gameObject == target.gameObject)
        {
            target.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}