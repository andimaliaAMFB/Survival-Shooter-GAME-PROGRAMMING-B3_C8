using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    public float PSpeed = 4f;
    public float timer = 5f;
    bool speeditemHit;
    PlayerMovement PM;

    void Awake()
    {
        PM = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if(speeditemHit && timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            speeditemHit = false;
        }

        if (!speeditemHit)
        {
            Debug.Log("SPEED TIME UP");
            PM.speed = 6f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            PM.speed = PM.speed + PSpeed;
            Debug.Log("Faster");

            speeditemHit = true;
            timer = 5f;
        }
    }
}
