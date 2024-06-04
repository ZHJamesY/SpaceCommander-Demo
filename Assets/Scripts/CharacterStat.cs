using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    private float health = 100;
    private float shield = 0;
    private float moveSpeed = 3;
    private float attackSpeed = 1;
    private int weaponLevel = 1;
    private float damage = 50;

    public string GetAllStat
    {
        get{return health + ", " + shield + ", " + moveSpeed + ", " + attackSpeed + ", " + weaponLevel + ", " + damage;}
    }

    public (float, float, float, float, int, float) SetAllStat
    {
        set
        {
            (health, shield, moveSpeed, attackSpeed, weaponLevel, damage) = value;
        }
    }

    public float Health
    {
        get { return health; } 
        set { health = value; }
    }

    public float Shield
    {
        get { return shield; } 
        set { shield = value; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; } 
        set { moveSpeed = value; }

    }

    public float AttackSpeed
    {
        get { return attackSpeed; } 
        set { attackSpeed = value; }
    }

    public int WeaponLevel
    {
        get { return weaponLevel; } 
        set { weaponLevel = value; }
    }

    public float Damage
    {
        get { return damage; } 
        set { damage = value; }
    }
}
