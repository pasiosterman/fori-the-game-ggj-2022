using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public float duration = 5.0f;
    float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;
        canvasGroup.alpha = 1.0f - (timer / duration);

        if(timer > duration){
            canvasGroup.alpha = 0.0f;
            gameObject.SetActive(false);
        }
    }
}
