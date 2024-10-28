using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float Speed = 10f;
    private Vector3 rotationValue;
    private float myMousePositionX;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
            myMousePositionX = Input.GetAxis("Mouse Y");

            rotationValue = new Vector3(myMousePositionX, 0, 0);
            transform.eulerAngles = transform.eulerAngles - rotationValue;
    }
}
