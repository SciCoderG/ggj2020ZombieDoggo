using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieDeathBehavior : MonoBehaviour
{
    [SerializeField]
    private FollowDogCameraMovement followDogCamera = null;
    [SerializeField]
    private ZombieManagementScript zombieManagementScript = null;
    [SerializeField]
    private DropItemArea dropItemArea = null;
    [SerializeField]
    private Transform OnDeathCameraPos = null;
    [SerializeField]
    private string HighScoreLevelName = "Highscore";
    [SerializeField]
    private float timeToWaitBeforeSwitchToHighscore = 5.0f;

    [SerializeField]
    private AudioSource audioSource = null;

    private bool isAlreadyDead = false;

    private void Awake()
    {
        if (null == followDogCamera)
            followDogCamera = FindObjectOfType<FollowDogCameraMovement>();

        audioSource = GetComponent<AudioSource>();
    }

    public void OnDeath()
    {
        if (!isAlreadyDead)
        {
            followDogCamera.enabled = false;
            CameraMovement cameraMovement = followDogCamera.GetComponent<CameraMovement>();
            cameraMovement.Target = OnDeathCameraPos.position;
            cameraMovement.LerpToRotation(OnDeathCameraPos.rotation);
            zombieManagementScript.PlayDeathAnimation();
            zombieManagementScript.enabled = false;
            zombieManagementScript.StopZombie();
            dropItemArea.GetComponent<Collider>().enabled = false;
            audioSource.PlayOneShot(audioSource.clip, 2.0f);
            isAlreadyDead = true;
            StartCoroutine(SwitchToHighscoreScene());
        }

    }

    private IEnumerator SwitchToHighscoreScene()
    {
        yield return new WaitForSeconds(timeToWaitBeforeSwitchToHighscore);
        SceneManager.LoadScene(HighScoreLevelName);
    }
}
