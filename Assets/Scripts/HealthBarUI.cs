using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image healthBarForegroundImage;
    public void UpdateHealthBar(float healthVal)
    {
        healthBarForegroundImage.fillAmount = healthVal/100;
    }
}
