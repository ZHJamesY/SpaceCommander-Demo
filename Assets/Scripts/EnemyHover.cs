using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHover : MonoBehaviour
{
    float hoverBounceSpeed = 3f;
    bool isHover = false;
    float dirProbability = 0.5f;
    float hoverDestination;
    int currentDirIndex = -1;
    float rad;
    float cos;
    float sin;
    float pauseBeforeHoverTime = 0.5f;
    float pauseBeforeHoverTimer;
    Vector2[] directions = new Vector2[4];
    List<float> angleList = new() {15f, 45f, 75f};
    Vector2 hoverObjSize;
    float randomVal;
    Movement hoverMovement;
    bool isDied = false;

    public bool IsDied
    {
        get { return isDied; }
        set { isDied = value; }
    }

    public bool IsHover
    {
        get{return isHover;}
        set{isHover = value;}
    }

    void Awake()
    {
        hoverMovement = GetComponent<Movement>();
        hoverObjSize = GetComponent<Renderer>().bounds.size;
    }

    void Start()
    {
        // enemy hover when entered desired destination
        hoverDestination = (float)(Camera.main.orthographicSize - (Camera.main.orthographicSize * 0.4));
    }

    void Update()
    {
        // when isHover is true, countdown hoverTimer, bounce around after hoverTimer/2 of time had passed
        if(isHover && !isDied)
        {
            pauseBeforeHoverTimer -= Time.deltaTime;

            if(pauseBeforeHoverTimer <= 0)
            {
                currentDirIndex = BounceDirection();
                hoverMovement.Move(directions[currentDirIndex], hoverBounceSpeed);
            }
        }

        // activate isHover -> true, when entered hoverDestination
        if(transform.position.y <= hoverDestination && !isHover && !isDied)
        {
            isHover = true;
            pauseBeforeHoverTimer = pauseBeforeHoverTime;
        }
    }

    // enemy hover, type: bounce
    int BounceDirection()
    {
        // top:bottom = {[0,2], [1,3]}  left:right = {[0, 1], [2, 3]}
        float heightBound = Camera.main.orthographicSize - hoverObjSize.y/2;
        float widthBound =  Camera.main.orthographicSize * (float)Screen.width/ (float)Screen.height - hoverObjSize.x/2;

        // when first appear
        if(currentDirIndex == -1)
        {
            int randomAngleIndex = UnityEngine.Random.Range(0, 3);
            ReCalculateDirectionAngle(angleList[randomAngleIndex]);
            randomVal = UnityEngine.Random.value;
            int startingDir = (randomVal < dirProbability) ? 0 : 1;
            
            switch(startingDir)
            {
                case 0:
                    dirProbability -= 0.25f;
                    break;
                case 1:
                    dirProbability += 0.25f;
                    break;
            }
            return (randomVal < dirProbability) ? 0 : 1;
        }
        
        // when reached top, bottom boundary
        if(transform.position.y >= heightBound)
        {
            int randomAngleIndex = UnityEngine.Random.Range(0, 3);
            ReCalculateDirectionAngle(angleList[randomAngleIndex]);

            return currentDirIndex switch
            {
                0 => 2,
                1 => 3,
                _ => currentDirIndex,
            };
        }
        else if(transform.position.y <= -heightBound)
        {
            int randomAngleIndex = UnityEngine.Random.Range(0, 3);
            ReCalculateDirectionAngle(angleList[randomAngleIndex]);

            return currentDirIndex switch
            {
                2 => 0,
                3 => 1,
                _ => currentDirIndex,
            };
        }

        // when reached left, right boundary
        if(transform.position.x >= widthBound)
        {
            int randomAngleIndex = UnityEngine.Random.Range(0, 3);
            ReCalculateDirectionAngle(angleList[randomAngleIndex]);

            return currentDirIndex switch
            {
                0 => 1,
                2 => 3,
                _ => currentDirIndex,
            };
        }
        else if(transform.position.x <= -widthBound)
        {
            int randomAngleIndex = UnityEngine.Random.Range(0, 3);
            ReCalculateDirectionAngle(angleList[randomAngleIndex]);

            return currentDirIndex switch
            {
                1 => 0,
                3 => 2,
                _ => currentDirIndex,
            };
        }
        return currentDirIndex;
    }

    // recalculate hover direction with new angle
    void ReCalculateDirectionAngle(float newAngle)
    {
        rad = Mathf.Deg2Rad * newAngle;
        cos = Mathf.Cos(rad);
        sin = Mathf.Sin(rad);

        directions[0] = new Vector2(cos, sin);      // top-right 0 -> 90
        directions[1] = new Vector2(-cos, sin);     // top-left 180 -> 90
        directions[2] = new Vector2(cos, -sin);     // down-right 360 -> 270
        directions[3] = new Vector2(-cos, -sin);    // down-left 180 ->270
    }

    void OnDisable()
    {
        currentDirIndex = -1;
    }

}
