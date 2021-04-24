using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float jumpForce = 300;

    Animator animator;
    Rigidbody rigidBody;
    Transform playerTransform;
    PlayerManager playerManager;

    private Vector2 inputVector = Vector2.zero;
    private Vector2 lookVector = Vector2.zero;
    private Vector3 moveDirection = Vector3.zero;

    bool isJumping;

    public int playerHealth = 3;
    public string playerStatus = "NORMAL";

    public GameObject pausePanel;
    public GameObject instructionPanel;

    int statusTimer = 5;
    bool isInstatus = false;
    public TMP_Text statusTimerText; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        playerManager = GetComponent<PlayerManager>();
        //SetGlobalScale(playerTransform, new Vector3(1.0f, 1.0f, 1.0f));
    }

    // Update is called once per frame
    private void Update()
    {
        CheckStatus();
        ControllPlayer();
    }

    private void LateUpdate()
    {
        Vector3 movementDirection = moveDirection * (movementSpeed * Time.deltaTime);

        playerTransform.position += movementDirection;

        GetComponent<Transform>().Rotate(Vector3.up * lookVector.x * .2f);
    }

    public void OnMovement(InputValue value)
    {
        //Debug.Log("CHECKME" + value.Get<Vector2>());
        inputVector = value.Get<Vector2>();
        animator.SetInteger("Walk", 1);
    }

    public void OnLook(InputValue value)
    {
        lookVector = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (isJumping) return;

        Invoke(nameof(Jump), 0.1f);
    }

    public void Jump()
    {
        animator.SetTrigger("jump");
        rigidBody.AddForce((playerTransform.up + moveDirection * Time.deltaTime) * jumpForce, ForceMode.Impulse);
    }


    public void OnPause(InputValue value)
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void OnInstruction(InputValue value)
    {
        Time.timeScale = 0;
        instructionPanel.SetActive(true);
    }



    void ControllPlayer()
    {
        if (!(inputVector.magnitude > 0))
        {
            animator.SetInteger("Walk", 0);
            moveDirection = Vector3.zero;
        }

        moveDirection = playerTransform.forward * inputVector.y + playerTransform.right * inputVector.x;

    }

    void CheckStatus()
    {
        //if (isInstatus)
        //{
        //    switch (playerManager.playerStatus)
        //    {
        //        case PlayerStatus.NORMAL:
        //            movementSpeed = 15.0f;
        //            jumpForce = 200;
        //            SetGlobalScale(playerTransform, new Vector3(3.0f, 3.0f, 3.0f));
        //            playerStatus = "NORMAL";
        //            break;
        //    }
        //}
        if (isInstatus == false)
        {
            switch (playerManager.playerStatus)
            {
                case PlayerStatus.NORMAL:
                    movementSpeed = 15.0f;
                    jumpForce = 200;
                    SetGlobalScale(playerTransform, new Vector3(3.0f, 3.0f, 3.0f));
                    playerStatus = "NORMAL";
                    break;
                case PlayerStatus.SHRINK:
                    movementSpeed = 15.0f;
                    jumpForce = 200;
                    SetGlobalScale(playerTransform, new Vector3(1.0f, 1.0f, 1.0f));
                    playerStatus = "SMALL";
                    break;
                case PlayerStatus.ENLARGE:
                    movementSpeed = 15.0f;
                    jumpForce = 200;
                    SetGlobalScale(playerTransform, new Vector3(6.0f, 6.0f, 6.0f));
                    playerStatus = "LARGE";
                    isInstatus = true;
                    isJumping = true;
                    //StartCoroutine(CountDownOne());
                    break;
                case PlayerStatus.SPEED:
                    movementSpeed = 40.0f;
                    jumpForce = 200;
                    playerStatus = "SPEED";
                    SetGlobalScale(playerTransform, new Vector3(3.0f, 3.0f, 3.0f));
                    //StartCoroutine(CountDownOne());
                    break;
                case PlayerStatus.JUMP:
                    movementSpeed = 15.0f;
                    jumpForce = 280;
                    playerStatus = "JUMP";
                    SetGlobalScale(playerTransform, new Vector3(3.0f, 3.0f, 3.0f));
                    //StartCoroutine(CountDownOne());
                    break;
                case PlayerStatus.SUPERENLARGE:
                    movementSpeed = 15.0f;
                    jumpForce = 200;
                    SetGlobalScale(playerTransform, new Vector3(6.0f, 6.0f, 6.0f));
                    isJumping = true;
                    playerStatus = "SUPERLARGE";
                    //StartCoroutine(CountDownOne());
                    break;
                case PlayerStatus.DEAD:
                    movementSpeed = 0.0f;
                    jumpForce = 0;
                    SetGlobalScale(playerTransform, new Vector3(3.0f, 3.0f, 3.0f));
                    playerStatus = "DEAD";
                    break;
                case PlayerStatus.WIN:
                    movementSpeed = 0.0f;
                    jumpForce = 0;
                    SetGlobalScale(playerTransform, new Vector3(3.0f, 3.0f, 3.0f));
                    playerStatus = "WIN";
                    break;
            }
        }
    }

    public void SetGlobalScale(Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }

    IEnumerator CountDownOne()
    {

        if (statusTimer > 0 ) statusTimer--;
        if (statusTimer <= 0)
        {
            isInstatus = false;
            playerManager.playerStatus = PlayerStatus.NORMAL;
        }
        statusTimerText.text = statusTimer.ToString();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(CountDownOne());
    }

}

