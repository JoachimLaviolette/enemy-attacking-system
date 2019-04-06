using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 GetRandomDir()
    {
        float minInterval = -1.5f;
        float maxInterval = 1.5f;

        return new Vector3(Random.Range(minInterval, maxInterval), 3f, 0f);
    }
}