using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsHandler : MonoBehaviour {

    public static TipsHandler Instance { get; private set; }

    [SerializeField]
    string[] tips;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string GetTip()
    {
        int index = Random.Range(0, tips.Length);
        return tips[index];
    }
}
