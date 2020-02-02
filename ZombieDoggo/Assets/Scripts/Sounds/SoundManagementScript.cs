using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagementScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      //  playSound(this.GetComponent<AudioSource>());
    }

    public void playSound(AudioSource sound)
    {
        sound.Play();
    }
}
