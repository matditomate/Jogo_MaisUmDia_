using UnityEngine;

public class AnsiedadeAnim : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private AudioClip somAnsiedade;
    private bool emLooping = false;
    public GameObject minigame;
    public GameObject celular;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        NivelAnsiedade();
    }

    private void NivelAnsiedade()
    {
        if (Robin.ansiedade == 10)
        {
            if (!emLooping)
            {
                emLooping = true;
                anim.SetBool("emAtaque", true);
                if (somAnsiedade != null && emLooping)
                {
                    AudioManager.instance.TocarLooping(somAnsiedade);
                }
            }

            minigame.SetActive(true);
            celular.SetActive(false);

        }
        else
        {
            if (emLooping)
            {
                emLooping = false;
                anim.SetBool("emAtaque", false);
                if (somAnsiedade != null)
                {
                    AudioManager.instance.PararLooping();
                }
            }
            minigame.SetActive(false);
            celular.SetActive(true);
        }
    }
}