using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagementScript : MonoBehaviour
{
    private static MusicManagementScript instance = null;
    public static MusicManagementScript Instance { get { return instance; } }

     
    void Awake()
    {

        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().name == "DavidsTest")
        {
            Destroy(this.gameObject);
        }
    }
}
