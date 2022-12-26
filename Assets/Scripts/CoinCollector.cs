using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{   
    [SerializeField] AudioClip coinSound;
    [SerializeField] int pointsForCoinPickup = 100;
    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !wasCollected){
            wasCollected = true;
           FindObjectOfType<GameSessions>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }

}
