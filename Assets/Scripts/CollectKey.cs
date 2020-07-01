using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject playerGO;
    AudioSource switchClick;
    bool switchPressed;

    private void Start() {
        switchClick = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if ((other.gameObject == playerGO && switchPressed == false)) {
            gameManager.collectKey();
            switchClick.Play();
            switchPressed = true;
        }
    }
}
