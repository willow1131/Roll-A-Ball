using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour

{
    public AudioClip pickupSound;
    public AudioClip winSound;

    AudioSource audioSource; 

    // Start is called before the first frame update
   private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PLayPickupSound()
    {
        PlaySound(pickupSound);
    }

    public void PLayWinSound()
    {
        PlaySound(winSound);
    }

    void PlaySound(AudioClip _newSound)
    {
        //set the audiosources adioclip to be the passed in sound
        audioSource.clip = _newSound;
        //play the audiosource
        audioSource.Play();
    }

    public void PlayCollisionSound(GameObject _go)
    {
        //check to see if the collided object has an audiosource.
        //This is a failsafe in case we forget to attach one to our wall
        if (_go.GetComponent<AudioSource>() != null)
        {
            //play the audio on the wall object
            _go.GetComponent<AudioSource>().Play();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
