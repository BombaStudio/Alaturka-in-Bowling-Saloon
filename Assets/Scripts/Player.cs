using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    GameObject myCam;
    float yr;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCam = transform.GetChild(0).gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        if (Input.GetMouseButtonDown(0) && Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;

        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


        yr -= Input.GetAxis("Mouse Y");
        yr = Mathf.Clamp(yr, -60, 60);

        myCam.transform.localRotation = Quaternion.Euler(yr, 0, 0);
        transform.Rotate(0,Input.GetAxis("Mouse X"),0);
    }
}
