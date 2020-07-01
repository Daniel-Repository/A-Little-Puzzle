using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.CompilerServices;

public class ControlsScreen : MonoBehaviour {

    public GameObject fadePanelGO;
    private Animator fadePanel;
    private bool spacePress = false;

    // Start is called before the first frame update
    void Start() {
        fadePanel = fadePanelGO.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space") && spacePress == false) {
            spacePress = true;
            StartCoroutine(startgameCo());
        }
    }

    IEnumerator startgameCo() {
        fadePanel.Play("fade_screen_out");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
