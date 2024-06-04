using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private float damage = 100;
    public float DamageDealt
    {
        get{return damage;}
        set{damage = value;}
    }
}
