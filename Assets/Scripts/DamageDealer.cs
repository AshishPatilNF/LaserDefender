using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    int damage = 50;

    public int Damage()
    {
        return damage;
    }
}
