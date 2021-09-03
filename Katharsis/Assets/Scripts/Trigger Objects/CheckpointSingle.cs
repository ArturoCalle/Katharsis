using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CheckPointController.instance.PlayerThroughCheckpoint(this);
            gameObject.SetActive(false);
        }
    }
}
