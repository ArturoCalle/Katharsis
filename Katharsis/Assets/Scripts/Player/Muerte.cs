using UnityEngine;

public class Muerte : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mortal")
        {
            UIController.instance.EndGame();
            Debug.Log("ouch");

        }
    }
}
