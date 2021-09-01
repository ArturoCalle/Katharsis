using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneController.instance.PlayerThroughCheckpoint(this);
            gameObject.SetActive(false);
        }
    }
}
