using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[Serializable]
public class DogController : MonoBehaviour
{

    public Transform[] camMounts;
    public Canvas board;
    private bool mouseOver = false;

    public Camera playerCam;
    private bool gameRunning = false;
    public float camMoveTime;

    // Start is called before the first frame update
    void Start()
    {
        camMounts = GetComponentsInChildren<Transform>().Where(x => x.name.Contains("Mount")).ToArray();
        board = FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mouseOver) {
            Debug.Log("noted...");
            //lock game
            gameRunning = true;
            //get camera
            playerCam = FindObjectOfType<SimpleCharacterControlFree>().ClaimCameraControl(this.gameObject);
            //move camera to correct point
            StartCoroutine(MoveCamera());
            //activate canvas
            board.GetComponent<TargetBoard>().StartGame(this);            
        }
    }

    IEnumerator MoveCamera()
    {
        Transform target = camMounts[Random.Range(0, camMounts.Length - 1)];
        Vector3 startPos = playerCam.transform.position;
        Quaternion startRot = playerCam.transform.rotation;
        float moveTime = 0f;

        while (moveTime <= camMoveTime)
        {
            moveTime += Time.deltaTime;
            float t = moveTime / camMoveTime;

            Vector3 newPos = Vector3.Slerp(startPos, target.position, t);
            playerCam.transform.position = newPos;

            Quaternion newRot = Quaternion.Slerp(startRot, target.rotation, t);
            playerCam.transform.rotation = newRot;

            yield return null;
        }
    }

    public void GameOver(bool success)
    {
        gameRunning = false;
        //set player ownership and shit
        FindObjectOfType<SimpleCharacterControlFree>().ReleaseCameraControl(this.gameObject);
    }

    private void OnMouseEnter()  => mouseOver = true;
    private void OnMouseExit() => mouseOver = false;
}
