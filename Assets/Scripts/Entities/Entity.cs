using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Vector3 lastMoveDir;
    protected float mHealth = 0f;
    protected float mSpeed = 0f;

    // Update is called once per frame
    private void Update()
    {
        HandleMovements();
    }

    public Vector3 GetCurrentPosition()
    {
        return this.transform.position;
    }

    abstract protected void HandleMovements();
}
