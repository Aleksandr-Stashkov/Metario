using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Star could not locate Player script.");
            }
            else
            {
                player.AddStar();
            }

            Destroy(gameObject);
        }
    }
}
