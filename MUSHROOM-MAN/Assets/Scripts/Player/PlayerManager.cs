using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatus
{
    NORMAL = 0,
    SHRINK,
    ENLARGE,
    SPEED,
    JUMP,
    SUPERENLARGE,
    DEAD,
    WIN
};

public class PlayerManager : MonoBehaviour
{
    public PlayerStatus playerStatus = PlayerStatus.NORMAL;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerStatus = PlayerStatus.NORMAL;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
