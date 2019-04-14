using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionPeriodic
{
    // Inner class handling the function periodic behaviour
    // Allow to use MonoBehaviour functions
    private class MonoBehaviourHook: MonoBehaviour
    {
        public Action onUpdate;

        private void Update()
        {
            this.onUpdate?.Invoke();
        }
    }

    // Properties
    private Action mAction;
    private float mStartAfter, mStartAfter_tmp, mTimer, mTimer_tmp;
    private GameObject mGameObject;

    // Create function
    public static FunctionPeriodic Create(Action action, float startAfter, float timer)
    {
        GameObject gameObject = new GameObject("FunctionPeriodic", typeof(MonoBehaviourHook));
        FunctionPeriodic functionPeriodic = new FunctionPeriodic(action, startAfter, timer, gameObject);
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionPeriodic.Update;

        return functionPeriodic;
    }

    // Private constructor
    private FunctionPeriodic(Action action, float startAfter, float timer, GameObject gameObject)
    {
        this.mAction = action;
        this.mStartAfter = this.mStartAfter_tmp = startAfter;
        this.mTimer = this.mTimer_tmp = timer;
        this.mGameObject = gameObject;
    }

    // Update the function periodic model
    public void Update()
    {
        this.mStartAfter_tmp -= Time.deltaTime;
        this.mTimer_tmp -= Time.deltaTime;

        if (this.mStartAfter_tmp < 0f && this.mTimer_tmp < 0f)
        {
            // Trigger the action
            this.mAction();
            // Reset the timer and the start after
            this.Reset();
            this.Destroy();
        }
    }

    // Destroy the game object associated to function periodic
    private void Reset()
    {
        this.mStartAfter_tmp = this.mStartAfter;
        this.mTimer_tmp = this.mTimer;
    }

    // Destroy the function periodic if needed
    private void Destroy()
    {
        if (!GameAssets.mInstance.GetPlayer().IsAlive())
        {
            UnityEngine.Object.Destroy(this.mGameObject);
        }
    }
}
