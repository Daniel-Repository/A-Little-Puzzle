using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private GameObject[] fallingGround;
    private bool keyCollected = false;

    public GameObject wallGO;
    public GameObject upWallsGO;
    public GameObject upWallsColliderGO;
    public GameObject switchGO;
    public GameObject goalGO;
    public GameObject fadePanelGO;
    public GameObject uiFade;
    public PlayerMovement playerMove;

    private Animator wallAnim;
    private Animator upWallsAnim;
    private Animator goalAnim;
    private Animator switchAnim;
    private Animator fadeAnim;
    private Animator uiFadeAnim;
    private Scene currentScene;
    private string currSceneName;

    private void Start() {
        fadeAnim = fadePanelGO.GetComponent<Animator>();
        upWallsAnim = upWallsGO.GetComponent<Animator>();
        uiFadeAnim = uiFade.GetComponent<Animator>();
        fallingGround = GameObject.FindGameObjectsWithTag("FallingGround");
        fadeAnim.enabled = true;
        fadeAnim.Play("fade_level_in");
        currentScene = SceneManager.GetActiveScene();
        currSceneName = currentScene.name;
    }

    public void CompleteLevel() {
        goalAnim.enabled = false;
        playerMove.enabled = false;
        StartCoroutine(startTransition("NextLevel"));
    }

    public void playerFall() {
        
        foreach (GameObject fg in fallingGround) {
            fg.GetComponent<Rigidbody>().useGravity = false;
        }
        StartCoroutine(startTransition("Reset"));
    }

    public void collectKey() {
        if (keyCollected == false) {
            keyCollected = true;
            switchAnim = switchGO.GetComponent<Animator>();
            goalAnim = goalGO.GetComponent<Animator>();
            switchAnim.enabled = true;
            goalAnim.enabled = true;
            upWallsColliderGO.transform.position = new Vector3(upWallsColliderGO.transform.position.x, 1, upWallsColliderGO.transform.position.z);
            StartCoroutine(wallAnimation());
        }
    }

    public void slidePlayer(Vector3 iceBlockPos) {
        playerMove.slidePlayer(iceBlockPos);
    }

    IEnumerator wallAnimation() {
        wallAnim = wallGO.GetComponent<Animator>();
        wallAnim.enabled = true;
        upWallsAnim.enabled = true;
        yield return new WaitForSeconds(1f);
        wallGO.SetActive(false);
        wallGO.transform.position = new Vector3(wallGO.transform.position.x, -1, wallGO.transform.position.z);
    }

    IEnumerator startTransition(string state) {
        yield return new WaitForSeconds(1f);
        fadeAnim.Play("fade_level_out");
        uiFadeAnim.Play("UIFadeOut");
        yield return new WaitForSeconds(1f);
        if (state == "Reset") {
            SceneManager.LoadScene(currSceneName);
        }
        else if (state == "NextLevel") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
