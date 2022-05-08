using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float rotationCycle = 3.0f;

    private float currentTimer = 0.0f;
    private void Update()
    {
        RotateEveryCycle();
    }

    void RotateEveryCycle()
    {
        currentTimer += Time.deltaTime;
        if(currentTimer > rotationCycle)
        {
            gameObject.transform.Rotate(90.0f, 0.0f, 0.0f);
            currentTimer = 0.0f;
        }
    }
}
