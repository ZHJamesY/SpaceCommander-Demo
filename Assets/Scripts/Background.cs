using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResizeSprite();
    }

    // Update is called once per frame
    void Update()
    {
        ResizeSprite();
    }

    void ResizeSprite()
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight / (float)Screen.height * (float)Screen.width;

        float spriteWidth = spriteRenderer.sprite.bounds.size.x;
        float spriteHeight = spriteRenderer.sprite.bounds.size.y;

        // Origin scale = 1, 
        // newScale.x * I (screenWidth / spriteWidth) = screenWidth
        // newScale.y * J (screenHeight / spriteHeight) = screenHeight
        Vector3 newScale = transform.localScale;
        newScale.x = screenWidth / spriteWidth;
        newScale.y = screenHeight / spriteHeight;

        transform.localScale = newScale;
    }
}
