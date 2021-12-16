using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueIndicator : MonoBehaviour
{
    [SerializeField] Color defaultColor; //State 0
    [SerializeField] Color attackColor;  //State 1
    [SerializeField] Color defendColor;  //State 2

    [SerializeField] float speed = 1f;
    [SerializeField] float range = 1.5f;
    [SerializeField] float delay = 1f;
    [SerializeField] Vector3 defaultPos = new Vector3(0.5f, 0.5f, 4f);
    [SerializeField] GameObject indicatorPrefab;

    float lastCreated = 0;
    bool running = false;
    List<GameObject> currentIndicators = new List<GameObject>();


    public void createIndicator(int value, int state)
    {
        StartCoroutine(IndicatorDelay());
        running = true;
        currentIndicators.Add(Instantiate(indicatorPrefab));

        int index = currentIndicators.Count - 1;
        currentIndicators[index].transform.localPosition = defaultPos + gameObject.transform.position;
        currentIndicators[index].GetComponent<TextMeshPro>().text = ""+value;
        currentIndicators[index].GetComponent<TextMeshPro>().color = state == 1 ? attackColor : state == 2 ? defendColor : defaultColor;
    }

    void Update()
    {
        if(running)
        {
            for (int i = 0; i < currentIndicators.Count; i++)
            {
                currentIndicators[i].transform.position += new Vector3(0f, 1f, 0f) * speed * Time.deltaTime;
                if (currentIndicators[i].transform.position.y >=  defaultPos.y + gameObject.transform.position.y + range)
                {
                    Destroy(currentIndicators[i]);
                    currentIndicators.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    void OnDestroy()
    {
        foreach (GameObject indicator in currentIndicators)
        {
            Destroy(indicator);
        }
    }

    //doesnt seem to currently work
    //Ensures break between indicators
    IEnumerator IndicatorDelay()
    {
        if (Time.time - lastCreated > delay)
        {
            yield return new WaitForSeconds(delay - (Time.time - lastCreated));
        }
        lastCreated = Time.time;
    }
}
