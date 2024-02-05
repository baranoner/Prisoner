using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinValue;
   AudioSource myAudioSource;
     void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
           myAudioSource = FindObjectOfType<AudioSource>();
           myAudioSource.PlayOneShot(myAudioSource.clip);
           FindObjectOfType<GameSession>().score = FindObjectOfType<GameSession>().score + coinValue;
           FindObjectOfType<GameSession>().scoreText.text = FindObjectOfType<GameSession>().score.ToString();
            Destroy(gameObject);
        }
    }
}
