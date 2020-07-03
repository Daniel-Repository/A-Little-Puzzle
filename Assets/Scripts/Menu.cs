using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour {

    public GameObject fadePanelGO;

    private Animator fadePanel;
    AudioSource optionSelectClick;

    private void Start() {
        optionSelectClick = GetComponent<AudioSource>();
    }

    public void startGame() {
        optionSelectClick.Play();
        fadePanelGO.SetActive(true);
        fadePanel = fadePanelGO.GetComponent<Animator>();
        StartCoroutine(startgameCo());
    }

    public void showCredits() {
        optionSelectClick.Play();
        Application.OpenURL("https://twitter.com/DanielCarroll_");
    }

    IEnumerator startgameCo() {
        fadePanel.enabled = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
