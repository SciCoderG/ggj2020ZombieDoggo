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
    private Transform OnDeathCameraPos = null;
    [SerializeField]
    private string HighScoreLevelName = "Highscore";
    [SerializeField]
    private float timeToWaitBeforeSwitchToHighscore = 5.0f;
    private void Awake()
    {
        if (null == followDogCamera)
            followDogCamera = FindObjectOfType<FollowDogCameraMovement>();
    }
    
    public void OnDeath()
    {
        followDogCamera.enabled = false;
        CameraMovement cameraMovement = followDogCamera.GetComponent<CameraMovement>();
        cameraMovement.Target = OnDeathCameraPos.position;
        cameraMovement.LerpToRotation(OnDeathCameraPos.rotation);
        zombieManagementScript.PlayDeathAnimation();
        zombieManagementScript.enabled = false;
        zombieManagementScript.StopZombie();

        StartCoroutine(SwitchToHighscoreScene());
    }

    private IEnumerator SwitchToHighscoreScene()
    {
        yield return new WaitForSeconds(timeToWaitBeforeSwitchToHighscore);
        SceneManager.LoadScene(HighScoreLevelName);
    }
}
