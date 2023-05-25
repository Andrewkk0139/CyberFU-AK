using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 200.0f;
    public float turnSmoothTime = 0.2f;
    public float speed;
    public Rigidbody rb;

    float turnSmoothVelocity;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if(inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        bool running = Input.GetKey(KeyCode.Z);
        speed = ((running) ? walkSpeed : runSpeed) * inputDir.magnitude;

        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        float animationSpeedPercent = ((running) ? 1 : 1f) * inputDir.magnitude;
        anim.SetFloat("Speed", animationSpeedPercent);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.5f, 23.5f), transform.position.y, Mathf.Clamp(transform.position.z, -2.2f, 2.2f));
    }
}
