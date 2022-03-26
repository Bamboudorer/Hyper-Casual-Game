using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    // Determine the speed of the boat, can be change for the tests
    [SerializeField] private float boatSpeed = 0.01f;

    // All next varaibles will be used for the manipulation and the deplacement of the boat
    // First start with the input and the positions
    private Touch touch;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector2 finalPos;
    [SerializeField] private Vector2 localisation;

    // They will be used to restrain the player inputs
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private float ratio;


    // The Awake is used to perform the initilization of some variables
    void Awake() 
    {
        width = (float)Screen.width;
        height = (float)Screen.height;

        ratio = height / width;

        // Set the position of the player to the center if he is not well placed
        transform.position = new Vector3(0.0f, 0.5f, 0.0f);
    }


    // // Start is called before the first frame update
    // void Start()
    // {
    //     boatSpeed = 0.1f;
    // }

    // Update is called once per frame
    void Update()
    {
        // if (Input.touchCount > 0) {
        //     touch = Input.GetTouch(0);

        //     if (touch.phase == TouchPhase.Moved) {
        //         transform.position = new Vector3(
        //             transform.position.x + touch.deltaPosition.x * boatSpeed,
        //             transform.position.y,
        //             transform.position.z + touch.deltaPosition.y * boatSpeed
        //         );
        //     }
        // }

        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                Debug.Log(transform.position);
                startPos = new Vector2(
                    touch.position.x - width / 2,
                    touch.position.y - height / 2
                );
                finalPos = new Vector2(
                    startPos.x / (width / 50),
                    startPos.y / (height / 50)
                );
                direction = new Vector2(
                    (finalPos.x - transform.position.x),
                    (finalPos.y - transform.position.z)
                );
                transform.position = new Vector3(
                    transform.position.x + direction.x,
                    transform.position.y,
                    transform.position.z + direction.y
                );
            }
        }

        // CHange the position of the boat
        BoatMovement();
    }

    /// <summary>
    /// Manipulate the position of the boat
    /// The objectif is to go to the new emplacement
    /// </summary>
    void BoatMovement() {
        // localisation = new Vector2(
        //     transform.position.x * ratio,
        //     transform.position.z * ratio
        // );

        // // Set all the variables to positives
        // if (localisation.x < 0)
        //     localisation.x *= -1;
        // if (localisation.y < 0)
        //     localisation.y *= -1;

        // if (localisation == finalPos) {
        //     direction = new Vector2(0.0f, 0.0f);
        // }

        // transform.position = new Vector3(
        //     transform.position.x + direction.x,
        //     transform.position.y,
        //     transform.position.z + direction.y
        // );
    }
}
