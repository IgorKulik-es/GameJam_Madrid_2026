using UnityEngine;

public enum AudioEffects
{
    SUCCESS,
    FAILURE,
    CLICK,
    ENGINE_START,
    DRIVING,
    ANNOUNCEMENT,
    NUM_EFFECTS
};

[RequireComponent(typeof(AudioSource))]
public class AudioManager: Monobehaviour
{
    [SerializeField] private AudioClip mainTheme;
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip failure;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip engingeStart;
    [SerializeField] private AudioClip driving;
    [SerializeField] private AudioClip[] stopAnnouncements;
    
    [SerializeField] private AudioSource source;
    public static AudioManager instance;
    public InputAction[] input;
    
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
    }
    void Start()
    {
        source.clip =  mainTheme;
        source.loop = true;
        source.Play();
    }

    void Update()
    {
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
