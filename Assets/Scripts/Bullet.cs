using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidBody;
    PlayerMovement player;

    float xspeed;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xspeed = player.transform.localScale.x * bulletSpeed;//bullet speed
    }


    void Update()
    {
        myRigidBody.velocity = new Vector2(xspeed, 0f);//bullet goes forward
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            Destroy(other.gameObject);
        }
        Destroy(gameObject);//destroying itself
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject); //destroying itself
    }
        
    }


