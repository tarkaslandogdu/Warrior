using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollector : MonoBehaviour
{
    private int coines = 0;

    [SerializeField] private Text coinsText;
    private AudioSource aSource;
    private audioManager aManager;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        aManager = audioManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            aManager.PlaySound("CoinPickup");
            coinTotal.totalScore += 1;
            Destroy(collision.gameObject);
            
        }
    }
}
