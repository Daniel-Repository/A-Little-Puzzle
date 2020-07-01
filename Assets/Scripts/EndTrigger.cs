using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour {

    public GameManager gameManager;
    public GameObject playerGO;
    private AudioSource goalChime;
    private Behaviour halo;

    private void Start() {
        goalChime = GetComponent<AudioSource>();
        halo = (Behaviour)GetComponent("Halo");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == playerGO) {
            goalChime.Play();
            halo.enabled = false;
            gameManager.CompleteLevel();
        }
    }

}
