using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargeMushroomController : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            if (other.GetComponent<PlayerManager>().playerStatus == PlayerStatus.NORMAL)
            {
                Debug.Log("CHECKME");
                other.GetComponent<PlayerManager>().playerStatus = PlayerStatus.SUPERENLARGE;
                Destroy(this.gameObject);
            }
            if (other.GetComponent<PlayerManager>().playerStatus == PlayerStatus.SHRINK)
            {
                Debug.Log("CHECK");
                other.GetComponent<PlayerManager>().playerStatus = PlayerStatus.NORMAL;
                Destroy(this.gameObject);
            }
        }
    }
}
