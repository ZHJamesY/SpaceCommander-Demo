using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public void Move(Vector2 direction, float speed)
    {
        transform.Translate(Time.deltaTime * speed * direction);
    }
}
