using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train_to : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 middlePosition;
    private Vector3 targetPosition;
    private int k = 0;

    public AudioSource arrival;
    public AudioSource departure;

    void Start()
    {
        startPosition = transform.position;
        middlePosition = startPosition;
        targetPosition = transform.position + Vector3.right * 120;

        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
        for (float i=0; i<1; i+=Time.deltaTime/3)
        {
            transform.position = Vector3.Lerp(middlePosition, targetPosition, EasingSmoothSquared(i));
            yield return null;  
        }
            arrival.Play();
            k++;
        if (k % 2 == 1)
            {
                 middlePosition = targetPosition;
                 targetPosition = transform.position + Vector3.right * 120;
                 yield return new WaitForSeconds(30f);
                departure.Play();
            }
        else 
            {
                 middlePosition = startPosition;
                 targetPosition = middlePosition + Vector3.right * 120;
                 yield return new WaitForSeconds(15f);
            }
        }
    }

    float EasingSmoothSquared(float x)
    {
        return x < 0.5 ? x * x * 2 : (1 - (1 - x) * (1 - x) * 2);
    }
}
