using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    public float lifetime = 4.0f;
    public int damage = 5;

    private void Start()
    {
        // Zerstört die Fackel nach der angegebenen Lebensdauer
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {       Debug.Log("trigger Burn");
        // Überprüft, ob das andere Objekt der Gegner ist
        if (other.CompareTag("Player"))
        {   
            // Gegner-Skript sollte eine Funktion haben, um Schaden zu erhalten
            StatusEffectManager playerburn = other.GetComponent<StatusEffectManager>();
            if (playerburn != null)
            {
                playerburn.ApplyBurn(damage);
            }
        }
    }
}

