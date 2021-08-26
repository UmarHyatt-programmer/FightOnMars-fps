using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float fireDely=0.1f;
    public GameObject pistol;
    public Rigidbody bulletrb;
    public Vector3 direction;
    public float bulletforce;
    public static PlayerController instance;
    public Animator animator;
    public bool isJump, isFire;
    public CharacterController characterController;
    public float speed, sensitivity, gravity = -9.8f, jumpHeight = 10, moveYDebug;
    float mouseY, mouseX;
    Vector3 velocity;
    bool isGround;
    public GameObject fpsCam;

    public void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(firing());
    }
    private void Update()
    {
        CharacterMovement();
        CamMovement();
        //Shooting();
    }

    public void CharacterMovement()
    {
        isGround = characterController.isGrounded;
        if (isGround)
        {
            if (velocity.y < 0)
            {
                Idle();
                velocity.y = -2f;
            }
            isJump = false;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGround)
        {
            Jump();
            isJump = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
        if (move != Vector3.zero && !isJump)
        {
            Run();
        }
        else if (isGround && !isFire && !isJump)
        {
            Idle();
        }
    }
    void Jump()
    {
        animator.SetBool("isJump", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRun", false);
        animator.SetBool("isShoot", false);
    }
    void Run()
    {
        animator.SetBool("isJump", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRun", true);
        animator.SetBool("isShoot", false);
    }
    void Idle()
    {
        animator.SetBool("isJump", false);
        animator.SetBool("isIdle", true);
        animator.SetBool("isRun", false);
        animator.SetBool("isShoot", false);
    }
    void Shoot()
    {
        animator.SetBool("isJump", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRun", false);
        animator.SetBool("isShoot", true);
    }
    public void CamMovement()
    {
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);
        transform.Rotate(Vector3.up * mouseX);
        fpsCam.transform.localRotation = Quaternion.Euler(mouseY, 0, 0);
    }
    public void Shooting()
    {
        if (Input.GetButton("Fire1"))
        {
            isFire = true;
            Instantiate(bulletrb.gameObject,pistol.transform.position,Quaternion.identity);
            direction = transform.forward;
            bulletrb.AddForce(direction * bulletforce);

            speed = 0;
            Shoot();
        }
        else
        {
        speed = 10;
        isFire = false;
        }
        
    }
    public IEnumerator firing()
    {
        while(true)
        {
        Shooting();
        yield return new WaitForSeconds(fireDely);
        }
    }

}
