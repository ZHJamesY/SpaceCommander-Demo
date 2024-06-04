using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawnManagement : MonoBehaviour
{
    [SerializeField] TMP_Dropdown difficultyDropdown;
    [SerializeField] List<GameObject> prefab;
    List<GameObject> tempInstantiateList = new();
    LevelManagement levelManagement;
    List<List<GameObject>> enemyGroups_Enemies = new();
    List<GameObject> enemyGroups;
    List<GameObject> positionList;
    int numSpawnPositions;
    int[] spawnPositionsArray;
    float spawnTime = 4;
    float spawnTimer;
    int enemyType = 1;
    int currentDifficultyIndex;
    public int GetCurrentDifficultyIndex
    {
        get{return currentDifficultyIndex;}
    }

    void Awake()
    {
        FindAllEnemyGroups();
        levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        positionList = Functions.GetAllChildObjects(GameObject.Find("EnemySpawnPosition").transform.Find("Vertical").gameObject);

        spawnTimer = 1;
    }

    void Start()
    {
        currentDifficultyIndex = levelManagement.CurrentDifficultyIndex;
        numSpawnPositions = levelManagement.GetFromDifficultyList(currentDifficultyIndex);
        spawnPositionsArray = SelectSpawnPosition(numSpawnPositions);
        if (difficultyDropdown != null)
        {
            // add a listener for when the value of the dropdown changes
            difficultyDropdown.onValueChanged.AddListener(delegate {
                OnDropdownValueChanged();
            });

        }
    }

    void Update()
    {
        if(levelManagement.GetGameStartStatus) 
        {
            spawnTimer -= Time.deltaTime;
            if(spawnTimer <= 0)
            {

                Spawn();
                // CheckDestroyInstantiateObj();
            }
        }
    }

    void FindAllEnemyGroups()
    {
        List<GameObject> allGroups = Functions.GetAllChildObjects(gameObject);
        enemyGroups = allGroups;
        for(int i = 0; i < allGroups.Count; i++)
        {
            enemyGroups_Enemies.Add(Functions.GetAllChildObjects(allGroups[i]));
        }
    }

    void Spawn()
    {
        for(int i = 0; i < numSpawnPositions; i++)
        {
            for(int j = 0; j < enemyType; j++)
            {
                int availableEnemyIndex = Functions.FindAvailableGameObjectFromList(enemyGroups_Enemies[j]);
                Vector3 currentSpawnPosition = positionList[spawnPositionsArray[i]].transform.position;
                Quaternion currentSpawnRotation = positionList[spawnPositionsArray[i]].transform.rotation;

                // if current no enemy objs available, -1, then initiate an enemy, add to pooling list, 
                // will destroy when scene changes or exit game
                if(availableEnemyIndex == -1)
                {
                    GameObject instantiateObj = Instantiate(prefab[j], currentSpawnPosition, currentSpawnRotation, enemyGroups[j].transform);
                    // tempInstantiateList.Add(instantiateObj);
                    enemyGroups_Enemies[j].Add(instantiateObj);
                    spawnTimer = spawnTime;
                }
                else
                {
                    enemyGroups_Enemies[j][availableEnemyIndex].SetActive(true);
                    enemyGroups_Enemies[j][availableEnemyIndex].transform.position = positionList[spawnPositionsArray[i]].transform.position;
                    spawnTimer = spawnTime;
                }
            }

        }  
    }

    void CheckDestroyInstantiateObj()
    {
        int index = 0;
        if (tempInstantiateList.Count != 0)
        {
            while (index < tempInstantiateList.Count && tempInstantiateList.Count != 0)
            {
                if (!tempInstantiateList[index].activeSelf)
                {
                    GameObject tempObj = tempInstantiateList[index];
                    tempInstantiateList.RemoveAt(index);
                    Destroy(tempObj);
                }
                else
                {
                    index++; 
                }
            }
        }
    }
 
    private int[] SelectSpawnPosition(int levelDifficulty)
    {
        return levelDifficulty switch
        {
            3 => new int[] { 0, 1, 2 },
            5 => new int[] { 0, 1, 2, 3, 4 },
            7 => new int[] { 0, 1, 2, 3, 4, 5, 6 },
            9 => new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 },
            _ => new int[] { 0, 1, 2 },
        };
    }

    // event listener, change number of enemy spawn according to level difficulty -> Dropdown UI
    void OnDropdownValueChanged()
    {
        currentDifficultyIndex = difficultyDropdown.value;
        numSpawnPositions = levelManagement.GetFromDifficultyList(difficultyDropdown.value);
        spawnPositionsArray = SelectSpawnPosition(numSpawnPositions);
    }

    void OnDestroy()
    {
        // remove the listener to prevent memory leaks
        if (difficultyDropdown != null)
        {
            difficultyDropdown.onValueChanged.RemoveAllListeners();
        }
    }
}
