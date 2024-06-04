using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    private float bulletSpeed = 15;
    private float damage = 100;

    public float BulletSpeed
    {
        get{return bulletSpeed;}
        set{bulletSpeed = value;}
    }

    public float Damage
    {
        get{return damage;}
        set{damage = value;}
    }

}
