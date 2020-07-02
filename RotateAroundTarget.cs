using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrRotateAroundTarget : MonoBehaviour
{
 
    public Transform target;
    public float speed = 5;

    [Header("Camera")]
    public float cameraSpeed = 5;
    public Vector2 horizontalLimit, verticalLimit;
    public float cameraPan = 3;
    private int location = 0;
    private float lastPosition;
    private float currentPosition;
    
    //Dont chance it in inspector. Only inspect it!
    [Header("Inspect")]
    [SerializeField]private float diff = 0;
    void Update()
    {
        if(target==null){
          print("Target is missing");
          return;
        }

        CalculateLocation();

        if (Input.GetMouseButton(0))
        {
            HandleMouse();
        }
        if (Input.GetMouseButtonUp(0))
        {
            diff = 0;
        }
    }

    //Handle horizontal movements of the mouse
    private void HandleMouse()
    {

        if (Input.GetMouseButtonDown(0))
        {
            lastPosition = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            currentPosition = Input.mousePosition.x;
        }
        if (currentPosition > lastPosition)
        {
            diff = currentPosition - lastPosition;
        }
        else if (currentPosition < lastPosition)
        {
            diff = currentPosition - lastPosition;
        }

        float diffPos = Mathf.Abs(diff);
        diffPos = Mathf.Clamp(diffPos, 0, 200);
        diffPos = diffPos / 200;

        if (diff > 0)
        {
            Movement(diffPos);
        }
        else if (diff < 0)
        {
            Movement(-diffPos);
        }
    }

    //Find the location of the object compare to target
    private void CalculateLocation()
    {
        Vector3 _camPos = Vector3.zero;
        _camPos.z = -10;

        if (transform.position.x > horizontalLimit.x)
        {
            _camPos.x = cameraPan;
        }
        else if (transform.position.x < horizontalLimit.y)
        {
            _camPos.x = -cameraPan;
        }
        else
        {
            _camPos.x = 0;
        }


        if (transform.position.y > verticalLimit.x)
        {
            _camPos.y = cameraPan;
        }
        else if (transform.position.y < verticalLimit.y)
        {
            _camPos.y = -cameraPan;
        }
        else
        {
            _camPos.y = 0;
        }

        HandleCamera(_camPos);
    }

    //Transform the camera according to player/object location compare to target
    private void HandleCamera(Vector3 _newPos)
    {
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, _newPos, Time.deltaTime * cameraSpeed);
    }

    //Rotate object around target
    private void Movement(float _hor)
    {
        transform.RotateAround(target.position, Vector3.forward, speed * _hor * Time.deltaTime);
    }
}
