using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovements : MonoBehaviour
{
    // Determine the speed of the boat, can be change for the tests
    [SerializeField] private float boatSpeed = 5f;
    [SerializeField] private SwipeDetector swipeDetector;
    public bool rightToPlay = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (swipeDetector != null) {
            swipeDetector.OnSwipeEvent += BoatMovement;
        }
    }


    void Update()
    {
        if (rightToPlay) {
            swipeDetector.CheckTouchPhase();
        }
    }

    /// <summary>
    /// Manipulate the position of the boat
    /// The objectif is to go to the new direction
    /// </summary>
    /// <param name="tmpStart">First Input</param>
    /// <param name="tmpEnd">Last Input</param>
    /// <param name="newMagnitude">Magnitude to impulse power</param>
    private void BoatMovement(Vector2 startPos, Vector2 endPos, float magnitude) {
        float magnitudePower = magnitude;
        float newPosX = endPos.x - startPos.x;
        float newPosZ = endPos.y - startPos.y;

        Vector3 finalPos = new Vector3(newPosX, 0, newPosZ).normalized;

        magnitudePower /= swipeDetector.swipeThreshold;

        Vector3 force = finalPos * magnitudePower * boatSpeed;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
