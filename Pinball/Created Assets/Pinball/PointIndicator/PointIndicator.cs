using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointIndicator : MonoBehaviour
{
    float startVerticalVelocity = 0.06f;
    float verticalVelocity;
    float horizontalVelocity = 0.03f;
    float gravitationalAcceleration = 0.2f;
    float counter = 0;
    float length = 1;

    public void Initialize(float value, float startX, float startY)
    {
        gameObject.SetActive(true);
        SetValue(value);
        transform.position = new Vector3(startX, startY, 0);
        counter = 0;
        verticalVelocity = startVerticalVelocity;
    }

    void Update()
    {
        counter += Time.deltaTime;
        transform.position += new Vector3(horizontalVelocity, verticalVelocity, 0);
        verticalVelocity -= gravitationalAcceleration*Time.deltaTime;
        if (counter >= length)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        FindObjectOfType<SceneData>()?.AddDisabledIndicatorToPool(gameObject);
    }

    void SetValue(float value)
    {
        if(value != 0)
        {
            GetComponent<TMP_Text>().text = "$" + value;
        }
    }
}
