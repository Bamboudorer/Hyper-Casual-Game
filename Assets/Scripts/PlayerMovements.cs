using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovements : MonoBehaviour
{
    // Determine the speed of the boat, can be change for the tests
    [SerializeField] private float boatSpeed = 2.5f;
    [SerializeField] private SwipeDetector swipeDetector;
    public bool rightToPlay = false;
    private Rigidbody rb;

    [SerializeField] private float maxRightField = 16f;
    [SerializeField] private float minLeftField = -16f;
    [SerializeField] private float maxUpField = 26f;
    [SerializeField] private float minDownField = -26f;

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

        CheckOutFieldPosition();
    }

    /// <summary>
    /// Manipulate the position of the boat
    /// The objectif is to go to the new direction
    /// </summary>
    /// <param name="tmpStart">First Input</param>
    /// <param name="tmpEnd">Last Input</param>
    /// <param name="newMagnitude">Magnitude to impulse power</param>
    private void BoatMovement(Vector2 startPos, Vector2 endPos, float magnitude)
    {
        float magnitudePower = magnitude;
        float newPosX = endPos.x - startPos.x;
        float newPosZ = endPos.y - startPos.y;

        Vector3 finalPos = new Vector3(newPosX, 0, newPosZ).normalized;

        magnitudePower /= swipeDetector.swipeThreshold;

        Vector3 force = finalPos * magnitudePower * boatSpeed;
        rb.AddForce(force, ForceMode.Impulse);
    }

    private void CheckOutFieldPosition()
    {
        if (transform.position.x < minLeftField || transform.position.x > maxRightField) {
            ResetPosition();
        }
        if (transform.position.z < minDownField || transform.position.z > maxUpField) {
            ResetPosition();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall") {
            ResetVelocity();
        }
    }

    /// <summary>
    /// Reset the velocity, reset the speed of the boat to 0
    /// </summary>
    private void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    /// <summary>
    /// Reset to position to the center of the game
    /// X = 0 | Y = 0.5 | Z = 0
    /// </summary>
    private void ResetPosition()
    {
        ResetVelocity();
        transform.position = new Vector3(0f, 0.5f, 0f);
    }
}
