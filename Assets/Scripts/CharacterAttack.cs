using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private List<GameObject> weapon;

    public void Attack(int weaponLevel)
    {
        int[] selectedFirePoints = SelectedFirePoints(weaponLevel);
        if(firePoints.Length >= selectedFirePoints.Length)
        {
            for(int i = 0; i < selectedFirePoints.Length; i++)
            {
                int availableAmmo = Functions.FindAvailableGameObjectFromList(weapon);
                if(availableAmmo != -1)
                {
                    weapon[availableAmmo].transform.position = firePoints[selectedFirePoints[i]].position;
                    weapon[availableAmmo].SetActive(true);
                }
            }
        }
    }

    // select available fire points based on weaponLevel
    private int[] SelectedFirePoints(int weaponLevel)
    {
        return weaponLevel switch
        {
            1 => new int[] { 0 },
            2 => new int[] { 1, 2 },
            3 => new int[] { 0, 1, 2 },
            4 => new int[] { 1, 2, 3, 4 },
            5 => new int[] { 0, 1, 2, 3, 4 },
            _ => new int[] { 0 },
        };
    }
}
