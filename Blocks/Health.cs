using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health;
    public UnityEvent e;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            e.Invoke();
        }
    }
}
