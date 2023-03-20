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

    GameObject myCam,playerAnim;
    float yr;

    GameController gc;

    [SerializeField] GameObject Hand,connected;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        myCam = transform.GetChild(0).gameObject;
        playerAnim = transform.GetChild(1).gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        if (gc.GameMode == "Game")
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (connected)
            {
                connected.transform.position = Hand.transform.position;
                connected.transform.rotation = Hand.transform.rotation;
                Hand.GetComponent<FixedJoint>().connectedBody = connected.GetComponent<Rigidbody>();
                if (Input.GetMouseButtonDown(1))
                {
                    connected = null;
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    UsableObject o = connected.GetComponent<UsableObject>();
                    o.force = myCam.transform.forward * 20;
                    connected = null;
                }
            }
            else
            {
                Hand.GetComponent<FixedJoint>().connectedBody = null;
            }
            if (controller.isGrounded)
            {

                RaycastHit h;
                if (Physics.Raycast(transform.position,-transform.up,out h, controller.height))
                {
                    if (h.transform.tag == "Snow") gc.GetComponent<SteamSystem>().snowAward();
                }

                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }


            if (Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked) gc.GameMode = "Pause";

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);


            yr -= Input.GetAxis("Mouse Y");
            yr = Mathf.Clamp(yr, -60, 60);

            myCam.transform.localRotation = Quaternion.Euler(yr, 0, 0);
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);


            Debug.DrawRay(myCam.transform.position, myCam.transform.forward * 10, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(myCam.transform.position, myCam.transform.TransformDirection(Vector3.forward), out hit, 10) && Input.GetMouseButtonDown(1))
            {
                Transform obj = hit.transform;
                foreach (ObjectData g in gc.objectdata)
                {
                    if (obj.tag == g.tag)
                    {
                        connected = obj.gameObject;
                    }
                }
            }
            playerAnim.GetComponent<Animator>().SetFloat(Animator.StringToHash("vertical"), new Vector2(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical"))).magnitude, 0.1f, Time.deltaTime);
        }
        else if (gc.GameMode == "Pause")
        {
            Cursor.lockState = CursorLockMode.None;
            //if (Input.GetMouseButtonDown(0) && Cursor.lockState != CursorLockMode.Locked)
        }
    }
}