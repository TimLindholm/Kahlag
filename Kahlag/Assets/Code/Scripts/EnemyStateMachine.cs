using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    public enum State
    {
        Idle,
        Patrol,
        Attack,
    }

    public State state;

    IEnumerator CrawlState()
    {
        Debug.Log("Idle: Enter");
        while (state == State.Idle)
        {
            yield return 0;
        }
        Debug.Log("Idle: Exit");
        NextState();
    }

    IEnumerator WalkState()
    {
        Debug.Log("Patrol: Enter");
        while (state == State.Patrol)
        {
            yield return 0;
        }
        Debug.Log("Patrol: Exit");
        NextState();
    }

    IEnumerator DieState()
    {
        Debug.Log("Attack: Enter");
        while (state == State.Attack)
        {
            yield return 0;
        }
        Debug.Log("Attack: Exit");
        NextState();
    }

    void Start()
    {
        NextState();
    }

    void NextState()
    {
            string methodName = state.ToString() + "State";
            System.Reflection.MethodInfo info = GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}
