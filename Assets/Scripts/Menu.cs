using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour {

    public GameObject fadePanelGO;

    private Animator fadePanel;

    public void startGame() {
        fadePanelGO.SetActive(true);
        fadePanel = fadePanelGO.GetComponent<Animator>();
        StartCoroutine(startgameCo());
    }

    public void showCredits() {

    }

    IEnumerator startgameCo() {
        fadePanel.enabled = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
