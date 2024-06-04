using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameStartCountdown : MonoBehaviour
{
    int countDownTime = 3;
    [SerializeField] GameObject countDownUI;
    TMP_Text countDownText;

    public UnityEvent countdownOver;

    void Awake()
    {
        countDownText = countDownUI.GetComponent<TMP_Text>();
    }

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    // countdown timer
    IEnumerator CountdownToStart()
    {
        while(countDownTime > 0)
        {
            countDownText.text = countDownTime.ToString();
            countDownUI.SetActive(true);
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }

        countDownText.text = "GO!";

        yield return new WaitForSeconds(0.5f);
        countDownUI.SetActive(false);
        countdownOver.Invoke();
    }

}
