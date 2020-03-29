using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject targetPrefab;
    private StrokeTarget currTarget;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTarget();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NotifyPointScored(){
        score++;
        Destroy(currTarget.gameObject);
        currTarget = null;
        SpawnTarget();
    }

    private void SpawnTarget(){
        Vector3[] corners = new Vector3[4];
        ((RectTransform) transform).GetWorldCorners(corners);

        //Get random point
        float x = Mathf.Lerp(corners[0].x, corners[3].x, Random.Range(0f,1f));
        float y = Mathf.Lerp(corners[0].y, corners[1].y, Random.Range(0f,1f));

        var newGo = Instantiate(targetPrefab);
        (newGo.transform as RectTransform).position = new Vector2(x, y);
        currTarget = newGo.GetComponent<StrokeTarget>();
        currTarget.TargetBoard = this;
    }
}
