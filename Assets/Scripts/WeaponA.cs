using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponA : MonoBehaviour
{
    Vector3 weaponSize;
    WeaponStat weaponStat;
    Movement bulletMovement;

    // dealDamage.DamageDealt += 10 -> increase weapon damage
    DealDamage dealDamage;
    Animator animator;
    bool isHit = false;

    // animation states
    string bullet = "Bullet";
    string explode = "Explode";

    void Awake()
    {
        weaponStat = GetComponentInParent<WeaponStat>();
        bulletMovement = GetComponent<Movement>();
        dealDamage = GetComponent<DealDamage>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        weaponSize = renderer.bounds.size;

    }

    // Update is called once per frame
    void Update()
    {
        if(!isHit)
        {
            if(transform.position.y - (weaponSize.y/2) < Camera.main.orthographicSize)
            {
                animator.Play(bullet);
                bulletMovement.Move(Vector2.up, weaponStat.BulletSpeed);
            }
            else gameObject.SetActive(false);
        }
    
    }

    void OnTriggerEnter2D()
    {
        isHit = true;
        animator.Play(explode);
        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        // animator.Play() will play animation starting next frame,
        // wait next frame to get the correct current animation state
        yield return null;

        // wait until the current animation finish
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        gameObject.SetActive(false);
        isHit = false;
    }
}
