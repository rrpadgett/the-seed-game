using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    public float MoveSpeed = 5;
    public float SteerSpeed = 10;
    int index = 0;
    Vector3 pos;
    public float speed = 0.001f;
    float angle;
    public float RootSpeed;


    // References
    public GameObject BodyPrefab;

    public bool isMoving = false;
    public bool boost = false;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
        RotateRoot();

        if(Input.GetKeyDown("space")){
            boost = true;
        } else {
            boost = false;
        }
    }

    void GrowRoot(){
        //if (index % 16 == 0){ we should put a delay here to reduce the amount of spheres
            GameObject body = Instantiate(BodyPrefab);
            Vector3 point = PositionsHistory[index];
            body.transform.position = transform.position;
            BodyParts.Add(body);
        //}
   
        index++;
    }

    void FollowMouse()
    {
        pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        if (!boost) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos.x, 0.0f, pos.z), speed * Time.deltaTime);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos.x, 0.0f, pos.z), 0.9f);
        }
        
        // Store position history
        PositionsHistory.Insert(0, transform.position);

        if (PositionsHistory[0] != PositionsHistory[1]){
            GrowRoot();
        }
    }

    void RotateRoot()
    {
        angle += Input.GetAxis("Mouse X") * RootSpeed * -Time.deltaTime;
        angle = Mathf.Clamp(-90, angle, 90);
        transform.localRotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

}
