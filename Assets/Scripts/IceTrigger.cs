using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class IceTrigger : MonoBehaviour {

    public GameManager gameManager;
    bool canTrigger = true;

    private void OnTriggerEnter(Collider other) {
        if (canTrigger == true) {
            var iceBlockPos = transform.position;
            gameManager.slidePlayer(iceBlockPos);
            canTrigger = false;
        }
    }

    public void OnTriggerExit(Collider other) {
        canTrigger = true;
    }
}   
