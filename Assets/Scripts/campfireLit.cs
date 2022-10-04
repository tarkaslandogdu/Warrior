using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class campfireLit : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] GameObject Fire;
    [SerializeField] GameObject Glow;
    [SerializeField] GameObject Light;

    private bool campfire = false;

    private void Campfire(GameObject fire)
    {
       
        Vector3 fireSpawnPosition = transform.position;
        GameObject newFire = Instantiate(fire, fireSpawnPosition, Quaternion.identity) as GameObject;
        newFire.transform.localScale = newFire.transform.localScale.x * transform.localScale;

    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.gameObject.name == "Player" && !campfire)
        {
            Campfire(Fire);
            Campfire(Glow);
            Campfire(Light);
            campfire = true;
        }
    }
}