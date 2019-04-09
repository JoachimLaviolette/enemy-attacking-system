using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // Return an random direction
    public static Vector3 GetRandomDir()
    {
        float minInterval = -1.5f;
        float maxInterval = 1.5f;

        return new Vector3(Random.Range(minInterval, maxInterval), 3f, 0f);
    }

    // Return an random direction for item bonus spawning
    public static Vector3 GetRandomDirItemBonus()
    {
        float minInterval = -.5f;
        float maxInterval = .5f;

        return new Vector3(Random.Range(minInterval, maxInterval), 3f, 0f);
    }

    // Return world space coordinates
    public static Vector3 GetWorldPosition(Vector3 position)
    {
        return Camera.main.ScreenToWorldPoint(position);
    }

    // Return world space coordinates
    public static Vector3 GetScreenPosition(Vector3 position)
    {
        return Camera.main.WorldToScreenPoint(position);
    }

    /*public static bool IsOffTheScreen(Vector3 objectPosition)
    {
        return true;
    }*/

    // Return the size of the sprite of the game object
    // In world space coordinates
    public static Vector3 GetSpriteSize(GameObject gameObject)
    {
        Vector2 spriteSize = gameObject.GetComponent<SpriteRenderer>().sprite.rect.size;

        // Get the local sprite size
        Vector2 localSpriteSize = spriteSize / gameObject.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        
        // Convert it in world space size
        Vector3 worldSize = localSpriteSize;
        worldSize.x *= gameObject.transform.lossyScale.x;
        worldSize.y *= gameObject.transform.lossyScale.y;

        return worldSize;
    }
}