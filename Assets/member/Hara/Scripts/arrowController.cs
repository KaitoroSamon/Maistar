using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowController : MonoBehaviour
{
    public float maxStretch = 3.0f;
    public LineRenderer bowLineTop;
    public LineRenderer bowLineBottom;

    private SpringJoint2D spring;
    private Transform bow;
    private Ray rayToMouse;
    private Ray bottomBowToProjectile;
    private float maxStretchSqr;
    //private float circleRadius; 
    private bool clickedOn;
    private Vector2 prevVelocity;
    
    void Awake()
    {
        spring = GetComponent <SpringJoint2D> ();
        bow = spring.connectedBody.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        LineRendererSetup();
        rayToMouse = new Ray(bow.position, Vector3.zero);
        bottomBowToProjectile = new Ray(bowLineBottom.transform.position, Vector3.zero);
        maxStretchSqr = maxStretch * maxStretch;
        CircleCollider2D circle = GetComponent<Collider2D>() as CircleCollider2D;
        //circleRadius = circle.radius;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Dragging();
        }
        if(spring != null){
            if(!GetComponent<Rigidbody2D>().isKinematic && prevVelocity.sqrMagnitude > GetComponent<Rigidbody2D>().velocity.sqrMagnitude){
                Destroy(spring);
                GetComponent<Rigidbody2D>().velocity = prevVelocity;
            }
            if(!Input.GetMouseButtonDown(0)){
                prevVelocity = GetComponent<Rigidbody2D>().velocity;
            }
            LineRendererUpdate();
        }else{
            bowLineBottom.enabled = false;
            bowLineTop.enabled = false;
        }
    }

    void LineRendererSetup()
    {
        bowLineBottom.SetPosition(0, bowLineBottom.transform.position);
        bowLineTop.SetPosition(0, bowLineTop.transform.position);
    }

    void OnMouseDown()
    {
        spring.enabled = false;
        clickedOn = true;
    }

    void OnMouseUp()
    {
        spring.enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        clickedOn = false;
    }

    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bowToMouse = mouseWorldPoint - bow.position;
        if(bowToMouse.sqrMagnitude > maxStretchSqr){
            rayToMouse.direction = bowToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }
        mouseWorldPoint.z = 0f;
        transform.position = mouseWorldPoint;
    }

    void LineRendererUpdate()
    {
        Vector2 bowToProjectile = transform.position - bowLineBottom.transform.position;
        bottomBowToProjectile.direction = bowToProjectile;
        Vector3 holdPoint = bottomBowToProjectile.GetPoint(bowToProjectile.magnitude);
        bowLineBottom.SetPosition(1, holdPoint);
        bowLineTop.SetPosition(1, holdPoint);
    }
}
