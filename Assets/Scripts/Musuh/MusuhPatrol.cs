using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusuhPatrol : MonoBehaviour
{

    public float Speed;
    public Transform[] PatrolPoints;
    public float WaitTime;
    int currentPoint;

    bool once;

    // Update is called once per frame
    private void Update()

    {
        if (transform.position != PatrolPoints[currentPoint].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[currentPoint].position, Speed * Time.deltaTime);
        } else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }

        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(WaitTime);
        if (currentPoint + 1 < PatrolPoints.Length)
        {
            currentPoint++;
        } else
        {
            currentPoint = 0;
        }
        once = false;
        
    }
}
