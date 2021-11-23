using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera TheCamera;        //! Камера;
    public GameObject ViewedTarget; //! цель, на которую мы смотрим.
    public GameObject ViewedTarget2;
    public float Speed = 1.0f;
    public float MaxDistance = 0.5f, MinDistance = 2.5f;  //
    public GameObject RotateX, RotateY;
    /*   input   */
    private Vector3 previousMousePosition;
    private Vector3 mousePos;


    private bool flMousePressed = false;
    void Start()
    {
        previousMousePosition = Input.mousePosition;
        TheCamera.transform.LookAt(ViewedTarget.transform);
    }

    void SetViewedTarget()
    {

    }
    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }
    void RotateeUpdate()
    {
        bool b = Input.GetMouseButton(0);
        Debug.Log("Up: " + ViewedTarget.transform.up.ToString() + " forward: " + ViewedTarget.transform.forward.ToString() + " right: " + ViewedTarget.transform.right.ToString());
        if (b)
        {
            if (!flMousePressed)
            {
                previousMousePosition = Input.mousePosition;
                flMousePressed = true;
            }
            else
            {
                mousePos = Input.mousePosition;

                float diffX = mousePos.x - previousMousePosition.x;
                float diffY = mousePos.y - previousMousePosition.y;

                if (Mathf.Abs(diffX) > 3)
                {
                    previousMousePosition.x = mousePos.x;
                    RotateX.transform.Rotate(new Vector3(0, 1, 0) * diffX * 12.1f * Time.deltaTime);
                    //TheCamera.transform.RotateAround(ViewedTarget.transform.position, new Vector3(0, 1, 0), /*(mousePos.x - previousMousePosition.x) **/ diffX * 12.1f * Time.deltaTime);
                    //ViewedTarget.transform.Rotate(new Vector3(0, 1, 0) * diffX * 12.1f * Time.deltaTime);

                }
                else if (Mathf.Abs(diffY) > 3)
                {
                    previousMousePosition.y = mousePos.y;
                    RotateY.transform.Rotate(new Vector3(1, 0, 0) * -diffY * 12.1f * Time.deltaTime);
                    //TheCamera.transform.RotateAround(ViewedTarget.transform.position, new Vector3(1, 0, 0) /*ViewedTarget.transform.right*/, /*(mousePos.x - previousMousePosition.x) **/ diffY * 12.1f * Time.deltaTime);
                    //ViewedTarget.transform.Rotate(new Vector3(1, 0, 0) * diffY * 12.1f * Time.deltaTime);
                }
            }
        }
        else
        {
            flMousePressed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update 1");
        if (!TheCamera || !ViewedTarget)
            return;

        //TheCamera.transform.RotateAround(ViewedTarget.transform.position, new Vector3(0, 1, 0), Time.deltaTime * Speed);


        RotateeUpdate();
        float diffWheel = Input.mouseScrollDelta.y;
        
        if (Mathf.Abs(diffWheel) > 0.05)
        {
            TheCamera.transform.Translate(Vector3.forward * diffWheel);
        }
    }
}
