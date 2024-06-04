using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterStat))]
public class Player : MonoBehaviour
{
    [SerializeField] TMP_Dropdown weaponLevelDropdown;
    CharacterStat playerStat;
    Movement playerMovement;
    CharacterAttack playerAttack;
    Vector2 playerSize;
    LevelManagement levelManagement;
    private float attackTimer = 0;
    private bool openFire = true;
    bool spawningState = true;



    void Awake()
    {
        playerStat = GetComponent<CharacterStat>();
        playerMovement = GetComponent<Movement>();
        playerAttack = GetComponent<CharacterAttack>();
        playerSize = GetComponent<Renderer>().bounds.size;
        levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStat.Health = 100;
        playerStat.Damage = 100;
        playerStat.AttackSpeed = 0.3f;
        playerStat.WeaponLevel = 1;
        playerStat.MoveSpeed = 6;
        // place player starting position outside the screen
        transform.position = new(0, -(Camera.main.orthographicSize + playerSize.y/2 + 1));

        if (weaponLevelDropdown != null)
        {
            // add a listener for when the value of the dropdown changes
            weaponLevelDropdown.onValueChanged.AddListener(delegate {
                OnDropdownValueChanged();
            });

        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(spawningState)
        {
            // spawn player when game starts
            case true:
            {
                PlayerSpawning();
                break;
            }
            // allow player control movement after spawning
            case false:
            {
                if(levelManagement.GetGameStartStatus)
                {
                    PlayerControl();
                }
                break;
            }
        }

    }

    void PlayerSpawning()
    {
        float heightBoundary = Camera.main.orthographicSize - playerSize.y/2;
        if(transform.position.y < -(heightBoundary - 2))
        {
            playerMovement.Move(Vector2.up, playerStat.MoveSpeed * 2);
        }
        else
        {
            spawningState = false;
        }
    }

    void PlayerControl()
    {
        // player movement
        playerMovement.Move(new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), playerStat.MoveSpeed);

        // height boundary
        float heightBoundary = Camera.main.orthographicSize - playerSize.y/2;
        if(transform.position.y >= heightBoundary)
        {
            transform.position = new(transform.position.x, heightBoundary);
        }
        if(transform.position.y <= -heightBoundary)
        {
            transform.position = new(transform.position.x, -heightBoundary);
        }

        // width boundary = orthographicSize * screen ratio
        float widthBoundary = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height - playerSize.x/2;
        if(transform.position.x >= widthBoundary)
        {
            transform.position = new(widthBoundary, transform.position.y);
        }
        if(transform.position.x <= -widthBoundary)
        {
            transform.position = new(-widthBoundary, transform.position.y);
        }
        
        // attack frequency, open fire
        attackTimer += Time.deltaTime;
        if(openFire && attackTimer >= playerStat.AttackSpeed)
        {
            playerAttack.Attack(playerStat.WeaponLevel);
            attackTimer = 0f;
        }
    }

        // event listener, change number of enemy spawn according to level difficulty -> Dropdown UI
    void OnDropdownValueChanged()
    {
        playerStat.WeaponLevel = weaponLevelDropdown.value + 1;
    }

    void OnDestroy()
    {
        // Remove the listener to prevent memory leaks
        if (weaponLevelDropdown != null)
        {
            weaponLevelDropdown.onValueChanged.RemoveAllListeners();
        }
    }

}
