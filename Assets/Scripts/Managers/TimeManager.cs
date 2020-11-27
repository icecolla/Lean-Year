using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameObject directionLight;

    public float dayLength;

    private Vector3 secondChange;

    public static TimeManager instance;

    private void Awake()
    {
        if(TimeManager.instance != null)
        {
            Destroy(gameObject);
        }
        TimeManager.instance = this;
    }

    public void Start()
    {
        secondChange = new Vector3(360 / (dayLength * 60 * 60), 0, 0);
        StartCoroutine("TimeChanger");
    }
    IEnumerator TimeChanger()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.01f);
            directionLight.transform.Rotate(secondChange);
        }
    }
}
