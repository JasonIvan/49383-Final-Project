using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMovements : MonoBehaviour
{

    public FloatingJoystick variableJoystick;

    [SerializeField] float speed;
    [SerializeField] float leftRightSpeed;
    [SerializeField] Animator[] animWheels;

    Rigidbody playerRigid;

    float h;
    float v;

    public AudioSource carAudioSource;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }


    void Rotate()
    {
        Vector3 xzDirection = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical);
        if (xzDirection.magnitude > 0 && xzDirection != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
          Quaternion.LookRotation(xzDirection), leftRightSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    public void Move()
    {
        playerRigid.velocity = (new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical) *
            speed * Time.fixedDeltaTime);

        if (playerRigid.velocity.normalized.magnitude > .1f)
        {
            foreach (var item in animWheels)
            {
                item.SetBool("IsRun",true);
            }
        }
        else
        {
            foreach (var item in animWheels)
            {
                item.SetBool("IsRun", false);
            }
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        carAudioSource.Play();

        if(col.gameObject.tag == "NPC")
        {
            col.gameObject.GetComponent<Animator>().SetBool("isHit", true);
        }

        if(col.GetComponent<CharacterManager>() != null)
        {
            col.GetComponent<CharacterManager>().isWalking = false;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "NPC")
        {
            col.gameObject.GetComponent<Animator>().SetBool("isHit", false);
        }

        if(col.GetComponent<CharacterManager>() != null)
        {
            col.GetComponent<CharacterManager>().isWalking = true;
        }
    }
}
