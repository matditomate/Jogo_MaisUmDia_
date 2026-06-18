using UnityEngine;

public class AnsiedadeAnim : MonoBehaviour
{
    private Animator anim;
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
            anim.SetBool("emAtaque", true);
            minigame.SetActive(true);
            celular.SetActive(false);

        }else
        {
            anim.SetBool("emAtaque", false);
            minigame.SetActive(false);
            celular.SetActive(true);
        }
    }
}
