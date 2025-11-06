using UnityEngine;
using System;
using UnityEditor.Callbacks;
using System.Collections;

public class SpriteMovement : MonoBehaviour
{

    [SerializeField]
    public Animator anim;
    private float count = 0;
    public float moveSpeed = 10f;
    private Vector3 input;
    private bool isMoving = false;
    private bool isSprinting = false;
    public bool isFrozen = false;
    private float i = 0;
    private float j = 0;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        SprintInput();
        Move(horizontal, vertical);
        Animate();
    }
    void Move(float x, float y)
    {
        if (!isFrozen)
        {
            i = x; //math equations done in double datatype but vectors are done in floats
            j = y; //i and j convert double a and b into float datatype
            input = new Vector3(x, y, 0);
            if (input.magnitude > 0.1f)
            {
                input = input.normalized;
            }
            float moveSpeed_new = isSprinting ? 2f * moveSpeed : moveSpeed;
            rb.transform.position += input * moveSpeed_new * Time.deltaTime;
        }
    }
    private void walkingSound()
    {
        if (!isSprinting)
        {
            if (!SoundManager.audioSource.isPlaying)
            {
                SoundManager.audioSource.pitch = 1.0f;
                SoundManager.playSound(SoundType.WildWestWalking);
            }
        }
        else
        {
            if (count > 0)
            {
                count -= Time.deltaTime;
            }
            else
            {
                SoundManager.playSound(SoundType.WildWestWalking);
                count += 0.2f;
            }
        }
    }
    private void Animate()
    {
        if (!isFrozen)
        {
            if (input.magnitude > 0.1f)
            {
                isMoving = true;
                walkingSound();
            }
            else
            {
                isMoving = false;
            }
            if (isMoving)
            {
                anim.SetFloat("x", i);
                anim.SetFloat("y", j);
            }
            anim.SetBool("isMoving", isMoving);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
    private void SprintInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        { isSprinting = true; }
        else
        { isSprinting = false; }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Waypoint"))
        {
            isFrozen = true;
            StartCoroutine(Delay());
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        isFrozen = false;
    }
}
