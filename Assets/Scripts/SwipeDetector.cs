using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void SwipeDetectorTriggerEvent(Vector2 a, Vector2 b, float c);

public class SwipeDetector : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    public float swipeThreshold;

    public bool detectSwipeAfterRelease = false;

    public event SwipeDetectorTriggerEvent OnSwipeEvent;

    void Start()
    {
        swipeThreshold = ((float)Screen.height - (float)Screen.width) / 11;
    }

    public void CheckTouchPhase()
    {
        foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				startPos = touch.position;
				endPos = touch.position;
			}

			//Detects Swipe while finger is still moving on screen
			if (touch.phase == TouchPhase.Moved)
			{
				if (!detectSwipeAfterRelease)
				{
					endPos = touch.position;
					if (touch.deltaPosition.magnitude > swipeThreshold)
					{
						OnSwipeEvent(startPos, endPos, touch.deltaPosition.magnitude);
					}
				}
			}

			//Detects swipe after finger is released from screen
			if (touch.phase == TouchPhase.Ended)
			{
				endPos = touch.position;
				OnSwipeEvent(startPos, endPos, touch.deltaPosition.magnitude);
			}
		}
    }
}
