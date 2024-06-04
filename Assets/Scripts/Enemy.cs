using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Enemy : MonoBehaviour
{
    Movement enemyMovement;
    CharacterStat enemyStat;
    DealDamage dealDamage;
    EnemyHover enemyHover;
    Vector3 enemyStartingPosition;
    EnemyGroup enemyGroup;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    Animator animator;
    EnemySpawnManagement enemySpawnManagement;
    int score;
    string enemyDied = "EnemyDied";
    Color color;

    void Awake()
    {
        enemyStat = GetComponent<CharacterStat>();
        enemyMovement = GetComponent<Movement>();
        dealDamage = GetComponent<DealDamage>();
        enemyHover = GetComponent<EnemyHover>();
        enemyGroup = GetComponentInParent<EnemyGroup>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        enemySpawnManagement = transform.parent.parent.GetComponent<EnemySpawnManagement>();;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = enemyGroup.Score;
        enemyStartingPosition = transform.position;
        dealDamage.DamageDealt = 50;
        color = spriteRenderer.color;
        enemyStat.Health = spawnEnemyHealth(enemySpawnManagement.GetCurrentDifficultyIndex);
    }

    // Update is called once per frame
    void Update()
    {
        // enter scene
        if(!enemyHover.IsHover && !enemyHover.IsDied)
        {
            enemyMovement.Move(Vector2.down, enemyStat.MoveSpeed + 5);
        }

        // play die animation and perform other commands when enemy killed by player
        if(enemyStat.Health <= 0)
        {            
            enemyHover.IsDied = true;
            ScoreController.Score += score;

            // update health the next spawn
            enemyStat.Health = spawnEnemyHealth(enemySpawnManagement.GetCurrentDifficultyIndex);
            animator.Play(enemyDied);
        }
    }

    private int spawnEnemyHealth(int levelDifficultyIndex)
    {
        return levelDifficultyIndex switch
        {
            0 => 100,
            1 => 150,
            2 => 200,
            3 => 250,
            _ => 100,
        };
    }

    // add animation event at last keyframe in animation tab, call Deactivate() at event
    void Deactivate()
    {
        transform.position = enemyStartingPosition;
        enemyHover.IsHover = false;
        circleCollider.enabled = true;
        enemyHover.IsDied = false;
        spriteRenderer.color = color;
        gameObject.SetActive(false);
    }
}
