using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int PHealth = 20;
    PlayerHealth PH;

    void Awake()
    {
        PH = FindObjectOfType<PlayerHealth>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            PH.GetCure(PHealth);

            Debug.Log("cure +20");
        }
    }
}
