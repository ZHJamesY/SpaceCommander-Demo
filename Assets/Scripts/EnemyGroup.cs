using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesCurrentLevel_Group;
    int score = 10;

    public int Score
    {
        get { return score; }
    }

    public List<GameObject> GetEnemiesCurrentLevel_Group
    {
        get{return enemiesCurrentLevel_Group;}
    }

}
