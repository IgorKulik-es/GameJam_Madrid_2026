using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum AudioEffects
{
    SUCCESS,
    FAILURE,
    CLICK,
    ENGINE_START,
    DRIVING,
    DOOR,
    ANNOUNCEMENT,
    NUM_EFFECTS
};

[RequireComponent(typeof(AudioSource))]
public class AudioManager: MonoBehaviour
{
    [SerializeField] private AudioClip mainTheme;
    [SerializeField] private AudioClip gameplayTheme;
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip failure;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip engingeStart;
    [SerializeField] private AudioClip driving;
    [SerializeField] private AudioClip doors;

    [SerializeField] private AudioClip[] stopAnnouncements;
    
    [SerializeField] private AudioSource source;
    public static AudioManager instance;
    public InputAction input;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        source.loop = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
    }

    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
            OneShot(AudioEffects.CLICK);
        if (Mouse.current.leftButton.wasPressedThisFrame)
            OneShot(AudioEffects.CLICK);
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
            {
                source.clip = mainTheme;
                break;
            }
            case 1:
            {
                source.clip = gameplayTheme;
                OneShot(AudioEffects.ENGINE_START);
                break;
            }
            default:
                break;
        }
        source.Play();
    }

    public void OneShot(AudioEffects effect)
    {
        switch (effect)
        {
            case AudioEffects.SUCCESS:
                source.PlayOneShot(success);
                break;
            case AudioEffects.FAILURE:
                source.PlayOneShot(failure);
                break;
            case AudioEffects.CLICK:
                source.PlayOneShot(click);
                break;
            case AudioEffects.ENGINE_START:
                source.PlayOneShot(engingeStart);
                break;
            case AudioEffects.DRIVING:
                source.PlayOneShot(driving);
                break;
            case AudioEffects.DOOR:
                source.PlayOneShot(doors);
                break;
            case AudioEffects.ANNOUNCEMENT:
            {
                if (stopAnnouncements.Length > 0)
                    source.PlayOneShot(stopAnnouncements[Random.Range(0, stopAnnouncements.Length)]);
                break;
            }
            default:
                return;
        }
    }
}
