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

    public Transform[] dogCamMounts;
    public Transform playerCamMount;
    public Canvas board;
    private bool mouseOver = false;

    public Camera playerCam;
    private bool gameRunning = false;
    public float camMoveTime;

    void Start()
    {
        dogCamMounts = GetComponentsInChildren<Transform>().Where(x => x.name.Contains("Mount")).ToArray();
        playerCamMount = FindObjectOfType<SimpleCharacterControlFree>().transform.Find("CamMount");
        board = FindObjectOfType<Canvas>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mouseOver && !gameRunning) {
            //lock game
            gameRunning = true;
            //get camera
            playerCam = FindObjectOfType<SimpleCharacterControlFree>().ClaimCameraControl(this.gameObject);
            //move camera to correct point
            StartCoroutine(MoveCamera(dogCamMounts[Random.Range(0, dogCamMounts.Length - 1)]));   
        }
    }

    public void GameOver(bool success)
    {
        StartCoroutine(MoveCamera(playerCamMount));
    }

    IEnumerator MoveCamera(Transform target)
    {
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

        if (dogCamMounts.Contains(target)) {
            //activate canvas
            board.GetComponent<TargetBoard>().StartGame(this);         
        } else {
            gameRunning = false;
            //set player ownership and shit
            FindObjectOfType<SimpleCharacterControlFree>().ReleaseCameraControl(this.gameObject);
        }
    }

    private void OnMouseEnter()  => mouseOver = true;
    private void OnMouseExit() => mouseOver = false;
}
