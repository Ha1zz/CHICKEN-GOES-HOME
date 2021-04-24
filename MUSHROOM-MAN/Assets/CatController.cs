using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    Animator animator;
    public Vector3 targetLocation;

    public int speed = 5;
    bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        targetLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        targetLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        ControllCat();
    }

    void ControllCat()
    {
        //RotateTowardTarget(targetLocation);
        if (isTriggered)
        {
            RotateTowardTarget(targetLocation);
            MoveToTarget(targetLocation);
        }
        else
        {
            animator.SetInteger("Walk", 0);
        }
    }

    void RotateTowardTarget(Vector3 target)
    {
        Vector3 targetDirection = target - transform.position;
        float singleStep = 10.0f * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target, singleStep, 0.0f);
        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void MoveToTarget(Vector3 target)
    {
        animator.SetInteger("Walk", 1);
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTriggered = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerManager>().playerStatus == PlayerStatus.NORMAL)
            {
                collision.gameObject.GetComponent<MovementController>().playerHealth--;
            }

            Destroy(this.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTriggered = false;
        }
    }

}
