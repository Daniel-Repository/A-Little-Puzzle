using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody playerRB;
    public GameManager gameManager;
    public GameObject wallGO;
    public GameObject upWallsColliderGO;
    public float slideDuration = 0.3f;
    public float tumblingDuration = 0.2f;
    bool canMove = true;
    bool canSlide = true;
    bool isTumbling = false;
    private Vector3 lastDir;
    private Queue<IEnumerator> slideroutineQueue = new Queue<IEnumerator>();
    private BoxCollider playerBC;
    AudioSource moveAudio;
    
    private void Start() {
        playerBC = GetComponent<BoxCollider>();
        StartCoroutine(CoroutineCoordinator());
        moveAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

        checkBeneath();

        var dir = Vector3.zero;
        Vector3 targetPosition = transform.position + dir;

        if (canMove == true) {

            if (Input.GetKeyDown("w")) {
                dir = Vector3.right;
                lastDir = Vector3.right;
                targetPosition = transform.position + dir;
            }

            if (Input.GetKeyDown("s")) {
                dir = Vector3.left;
                lastDir = Vector3.left;
                targetPosition = transform.position + dir;
            }

            if (Input.GetKeyDown("a")) {
                dir = Vector3.forward;
                lastDir = Vector3.forward;
                targetPosition = transform.position + dir;
            }

            if (Input.GetKeyDown("d")) {
                dir = Vector3.back;
                lastDir = Vector3.back;
                targetPosition = transform.position + dir;
            }

            if (dir != Vector3.zero && !isTumbling && targetPosition != wallGO.transform.position) {
                if (upWallsColliderGO != null && targetPosition != upWallsColliderGO.transform.position)  {
                    StartCoroutine(Tumble(dir));
                }
                else if (upWallsColliderGO == null)  {
                    StartCoroutine(Tumble(dir));
                }
                
            }
        }
    }

    private void checkBeneath() {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;

        
        if(Physics.Raycast(ray, out hitInfo, 1) && hitInfo.distance < 1) {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
        }
        else {
            canMove = false;
            canSlide = false;
            playerRB.isKinematic = false;
            playerBC.isTrigger = true;
            gameManager.playerFall();
            Debug.DrawLine(ray.origin, hitInfo.point, Color.blue);
        }
        
    }

    public void slidePlayer(Vector3 iceBlockPos) {
        var startPosition = new Vector3(iceBlockPos.x, 1, iceBlockPos.z);
        var endPosition = (new Vector3(iceBlockPos.x, 1, iceBlockPos.z)) + lastDir;
        if(endPosition != wallGO.transform.position) {
            if (upWallsColliderGO != null && endPosition != upWallsColliderGO.transform.position) {
                slideroutineQueue.Enqueue(Slide(startPosition, endPosition, slideDuration));
            }
            else if (upWallsColliderGO == null) {
                slideroutineQueue.Enqueue(Slide(startPosition, endPosition, slideDuration));
            }
            
        }  
    }
    
    IEnumerator CoroutineCoordinator() {
        while (true) {
            while (slideroutineQueue.Count > 0 && canSlide == true) {
                canMove = false;
                yield return StartCoroutine(slideroutineQueue.Dequeue());
            }
            canMove = true;
            yield return null;
        }
    }

    IEnumerator Slide(Vector3 startPosition, Vector3 endPosition, float LerpTime) {

        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;

        while (Time.time < EndTime) {
            float timeProgressed = (Time.time - StartTime) / LerpTime; //This will be 0 at start and 1 at end.
             transform.position = Vector3.Lerp(startPosition, endPosition, timeProgressed);
             yield return new WaitForFixedUpdate();
        }
        transform.position = endPosition;
    }

    IEnumerator Tumble(Vector3 direction) {
        isTumbling = true;

        var startPosition = transform.position;
        var endPosition = transform.position + direction;

        var rotAxis = Vector3.Cross(Vector3.up, direction);
        var pivot = (transform.position + Vector3.down * 0.5f) + direction * 0.5f;

        var startRotation = transform.rotation;
        var endRotation = Quaternion.AngleAxis(90.0f, rotAxis) * startRotation;

        var rotSpeed = 90.0f / tumblingDuration;
        var t = 0.0f;

        while (t < tumblingDuration) {
            t += Time.deltaTime;
            if (t < tumblingDuration) {
                transform.RotateAround(pivot, rotAxis, rotSpeed * Time.deltaTime);
                yield return null;
            }
            else {
                transform.rotation = endRotation;
                transform.position = endPosition;
            }
        }
        if (canMove == true) {
            moveAudio.Play();
        }
        transform.position = endPosition;
        isTumbling = false;
        
    }
    
}
