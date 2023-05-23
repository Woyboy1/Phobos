using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class JumpscareManager : MonoBehaviour
{
    /// <summary>
    /// This is NOT a singleton pattern. Will handle the jumpscare animation and
    /// post proccessing effects, etc.
    /// </summary>
    /// 

    [Header("Jumpscare objects")]
    [SerializeField] private GameObject ghoulJumpscareObject;
    [SerializeField] private GameObject zombieJumpscareObject;

    [Header("Offsets for Ghoul Jumpscare")]
    [SerializeField] private float zGhoulPlayerOffset = 1.4f;
    [SerializeField] private float zGhoulCameraOffset = 60;
    [SerializeField] private float yGhoulCameraOffset = 30.0f;

    [Header("Offsets for Zombie Jumpscare")]
    [SerializeField] private float zZombiePlayerOffset = 1.4f;
    [SerializeField] private float zZombieCameraOffset = 25f;
    [SerializeField] private float yZombieCameraOffset = 25;

    [Space(20)]

    [Header("Audio")]
    [SerializeField] private float timeTilAudio = 0.2f;
    [SerializeField] private string ghoulJumpscareAudio = "PlayerJumpscare";
    [SerializeField] private string zombieJumpScareAudio = "PlayerZombieJumpscare";
    [SerializeField] private string bloodAudio = "Blood";

    [Header("Etc")]
    [SerializeField] private float timeTilRestart = 2.4f;

    [Header("Text Jumpscares")]
    [SerializeField] private TextMeshProUGUI scareText;
    [SerializeField] private string[] randomText;
    [SerializeField] private float timeIntervalMin = 60f;
    [SerializeField] private float timeIntervalMax = 80f;

    Camera cam;
    PlayerMovement playerMovement;
    MouseLook playerLook;
    Player player;

    private void Start()
    {
        cam = Camera.main;
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        player = FindAnyObjectByType<Player>();
        playerLook = FindAnyObjectByType<MouseLook>();

        StartCoroutine(StartTextSpawns());
    }

    public void GhoulJumpscare() // for Ghoul
    {
        PlayerControlsDisabled();

        // Instantiating a prefab of the jumpscare
        Vector3 playerOffset = new Vector3(player.transform.position.x, 0, player.transform.position.z - zGhoulPlayerOffset);
        GameObject jumpscareClone = Instantiate(ghoulJumpscareObject, playerOffset, Quaternion.identity);

        // Audio:
        StartCoroutine(PlayGhoulAudio());
        StartCoroutine(PlayBloodAudio());

        // Moving the camera to face the jumpscare
        Vector3 cameraOffset = new Vector3(jumpscareClone.transform.position.x, zGhoulCameraOffset, jumpscareClone.transform.position.z - yGhoulCameraOffset);
        cam.gameObject.transform.LookAt(cameraOffset);

        StartCoroutine(EndGame());
    }

    public void ZombieJumpscare() // for zombie
    {
        PlayerControlsDisabled();

        // Instantiating a prefab of the jumpscare
        Vector3 playerOffset = new Vector3(player.transform.position.x, player.transform.position.y - 1.2f, player.transform.position.z - zZombiePlayerOffset);
        GameObject jumpscareClone = Instantiate(zombieJumpscareObject, playerOffset, Quaternion.identity);

        // Audio:
        AudioManager.instance.Play(zombieJumpScareAudio);
        StartCoroutine(PlayBloodAudio());

        // Moving the camera to face the jumpscare
        Vector3 cameraOffset = new Vector3(jumpscareClone.transform.position.x, zZombieCameraOffset, jumpscareClone.transform.position.z - yZombieCameraOffset);
        cam.gameObject.transform.LookAt(cameraOffset);

        StartCoroutine(EndGame());
    }

    public void PlayerControlsDisabled()
    {
        playerLook.StopBobbing();
        playerMovement.CanMove = false;
        playerLook.CanLook = false;
        player.StopBreathingSFX();
    }

    IEnumerator PlayGhoulAudio()
    {
        yield return new WaitForSeconds(timeTilAudio);
        AudioManager.instance.Play(ghoulJumpscareAudio);
    }

    IEnumerator PlayBloodAudio()
    {
        yield return new WaitForSeconds(timeTilAudio);
        AudioManager.instance.Play(bloodAudio);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(timeTilRestart);
        SceneManager.LoadScene("LoadingScreen");
    }

    IEnumerator ScareText()
    {
        float cooldown = Random.Range(timeIntervalMin, timeIntervalMax);
        int randomIndex = Random.Range(0, randomText.Length);

        // Unhide text and change it. make it scary
        AudioManager.instance.Play("Anvil");

        // Color Transparency:
        Color colorWhite = new Color(255, 255, 255, 0.32f); // hard coded value of 0.32 for the transparency. 
        scareText.color = colorWhite;

        scareText.gameObject.SetActive(true);
        scareText.text = randomText[randomIndex];
        yield return new WaitForSeconds(1.5f);
        scareText.gameObject.SetActive(false);

        yield return new WaitForSeconds(cooldown);
        StartCoroutine(ScareText());
    }

    IEnumerator StartTextSpawns()
    {
        yield return new WaitForSeconds(120);
        StartCoroutine(ScareText());
    }
}
