using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableDoor : MonoBehaviour
{
    [SerializeField]
    private int moveUnits;
    [SerializeField]
    private float moveTime;
    [SerializeField]
    private float waitTime;
    private bool hasMoved = false;

    // In OnCollisionEnter2D(), I used coroutine to move tiles horizontally to 
    // stimulate a door sliding to the side for the player to get through.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!hasMoved)
            {
                StartCoroutine(MoveDoor());
                hasMoved = true;
            }
        }
    }

    // MoveDoor() is my way of animating a sliding door using coroutine. It only
    // slides horizontally once.
    private IEnumerator MoveDoor()
    {
        yield return new WaitForSeconds(waitTime);
        float totalTime = 0;
        float originX = transform.position.x;
        while (totalTime < waitTime)
        {
            float x = Mathf.Lerp(0, moveUnits, totalTime/moveTime);
            transform.position = new Vector3(originX + x, transform.position.y, transform.position.z);
            totalTime += Time.deltaTime;
            yield return null;
        }
    }
}
