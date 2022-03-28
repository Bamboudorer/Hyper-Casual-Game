using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private SpawnPoints sp;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sp = GameObject.Find("Spawn").GetComponent<SpawnPoints>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            gm.AddScore();

            if (sp != null) {
                sp.SpwanSinglePoint();
            }

            Destroy(gameObject);
        }
    }
}
