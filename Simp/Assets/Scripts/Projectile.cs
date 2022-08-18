using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public GameObject shooter;
    BaseUnit shooterStat;
    // public GameObject particles;
    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        shooterStat = shooter.GetComponent<BaseUnit>();
    }

    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            // e.Fix();
        }
        
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other) {
        if(this.tag == "PlayerBullet") {
            if(other.tag == "Enemy") {
                BaseUnit enemyStat = other.gameObject.GetComponent<BaseUnit>();
                enemyStat.TakeDmg(shooterStat.dmg);
                Debug.Log("Enemy HP: " + enemyStat.currHP);        
                // Instantiate(particles,transform.position,Quaternion.identity);
                //AudioManager.PlaySound("BulletCollide");
                Destroy(gameObject);
            }
        } else if(this.tag == "EnemyBullet"){
            if(other.tag == "Player") {
                BaseUnit playerStat = other.gameObject.GetComponent<BaseUnit>();
                if(!playerStat.invincible){
                    // Instantiate(particles,transform.position,Quaternion.identity);
                    playerStat.TakeDmg(shooterStat.dmg);
                    Debug.Log("Player HP: " + playerStat.currHP);        
                    Destroy(gameObject);
                }
            }
        }
    }
}