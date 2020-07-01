using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FallingTrigger : MonoBehaviour {

    public Rigidbody fallingRB;
    public GameObject fallingGroundCube;

    private void OnTriggerExit(Collider other) {
        fallingRB.isKinematic = false;
        Destroy(this, 2);
        Destroy(fallingGroundCube, 2);
    }
}
