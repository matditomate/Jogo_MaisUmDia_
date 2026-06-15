using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Componentes de Áudio")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource loopSource;

    [Header("Sons Globais de Transição")]
    [SerializeField] private AudioClip somPortaFechando;

    private bool deveTocarFecharNaProximaCena = false;

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
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += AoCarregarCena;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;
    }

    public void AgendarSomDeFechar()
    {
        deveTocarFecharNaProximaCena = true;
    }

    private void AoCarregarCena(Scene scene, LoadSceneMode mode)
    {
        if (deveTocarFecharNaProximaCena)
        {
            TocarSFX(somPortaFechando);
            deveTocarFecharNaProximaCena = false;
        }
    }

    public void TocarSFX(AudioClip som)
    {
        if (som != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(som);
        }
    }

    public void TocarLooping(AudioClip som)
    {
        if(som != null && loopSource != null)
        {
            if(loopSource.clip == som && loopSource.isPlaying) return;

            loopSource.clip = som;
            loopSource.loop = true;
            loopSource.Play();
        }
    }

    public void PararLooping()
    {
        if(loopSource != null && loopSource.isPlaying)
        {
            loopSource.Stop();
            loopSource.clip = null;
        }
    }
}