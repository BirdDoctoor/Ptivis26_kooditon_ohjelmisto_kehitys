using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Timer : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0f;
    }

    public GameObject myObject;

    void Start()
    {
        myObject.SetActive(false);
        StartCoroutine(EnableAfterTime());
    }

    IEnumerator EnableAfterTime()
    {
        yield return new WaitForSecondsRealtime(5f);

        myObject.SetActive(true);
        Time.timeScale = 1f;
    }
}
