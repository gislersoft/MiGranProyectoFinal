/**
 * Universidad Autónoma de Occidente - 2018
 *
 * Elevator redesigned using rigigbody.
 *
 * @autor Gisler Garces <ggarces@uao.edu.co>
 * @autor Cesar Salazar <cesar0572011@hotmail.com>
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Elevator : MonoBehaviour {

    public Texture txtup;
    public Texture txtdown;

    public float speedForce = 1000.0f;
    public float slowDownForce = 30.0f;

    private GameObject lift;
    private GameObject top;
    private GameObject bottom;
    private GameObject screen;
    private AudioSource au;
    private Renderer rd;
    private Animator animator;

    private Rigidbody rb;
    private bool elevatorMoving = false;
    private bool playerOnElevator = false;
    private bool playSoundOnce = true;

    private bool goingUp = true;

    // Use this for initialization
    void Start() {
        Transform parentComponent = this.transform.parent;
        lift = parentComponent.transform.Find("Platform").gameObject;
        rb = lift.GetComponent<Rigidbody>();
        rb.useGravity = true;
        top = parentComponent.transform.Find("top").gameObject;
        bottom = parentComponent.transform.Find("bottom").gameObject;
        au = lift.GetComponent<AudioSource>();
        GameObject scifiScreen = lift.transform.Find("ScifiScreen").gameObject;
        screen = scifiScreen.transform.Find("screen").gameObject;
        rd = screen.GetComponent<Renderer>();
        rd.material.mainTexture = txtup;
        animator = GameObject.FindWithTag("PlayerAnimator").GetComponent<Animator> ();
    }

    /**
     * Completely stops the platform
     */
    void stopPlatform(Texture directionTexture) {
        elevatorMoving = false;
        goingUp = !goingUp;
        playSoundOnce = true;
        rd.material.mainTexture = directionTexture;
        rb.position = top.transform.position;
        rb.linearVelocity = new Vector3(0,0,0);
        rb.angularVelocity = new Vector3(0,0,0);
        rb.constraints = RigidbodyConstraints.FreezeRotationX
        | RigidbodyConstraints.FreezeRotationY
        | RigidbodyConstraints.FreezeRotationZ
        | RigidbodyConstraints.FreezePositionY
        | RigidbodyConstraints.FreezePositionX
        | RigidbodyConstraints.FreezePositionZ;
    }

    /**
     * Release platform
     */
    void releasePlatform() {
        rb.constraints = RigidbodyConstraints.FreezeRotationX
        | RigidbodyConstraints.FreezeRotationY
        | RigidbodyConstraints.FreezeRotationZ
        | RigidbodyConstraints.FreezePositionX
        | RigidbodyConstraints.FreezePositionZ;
    }

    /**
     * When character is on elevator and the elevator is not moving
     */
    bool characterIsOnElevatorAndElevatorNotMoving() {
        return (playerOnElevator && !elevatorMoving);
    }

    /**
     * Plays the elevator sound once.
     */
    void playElevatorSoundOnce() {
        if (playSoundOnce == true)
        {
            au.Play();
            playSoundOnce = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (elevatorMoving == true)
        {
            if (goingUp == true) {
                rb.AddForce(0, speedForce , 0);
                if (lift.transform.position.y >= top.transform.position.y)
                {
                    this.stopPlatform(txtdown);
                    rb.position = top.transform.position;
                    lift.transform.position = top.transform.position;
                }
            } else {
                rb.AddForce(0, slowDownForce, 0);
                if (lift.transform.position.y <= bottom.transform.position.y)
                {
                    this.stopPlatform(txtup);
                    rb.position = bottom.transform.position;
                    lift.transform.position = bottom.transform.position;
                }
            }
        } else {
            if (Input.GetKeyDown(KeyCode.E) && this.characterIsOnElevatorAndElevatorNotMoving())
            {
                animator.SetTrigger("isTouching");
                elevatorMoving = true;
                this.releasePlatform();
                this.playElevatorSoundOnce();
            }
        }
    }

    // When player enters into the elevator
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("P1")) playerOnElevator = true;
    }

    // When player leaves the elevator.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("P1")) playerOnElevator = false;
    }
}
