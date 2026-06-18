using UnityEngine;

public class AnsiedadeAnim : MonoBehaviour
{
    private Animator anim;

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
        }else
        {
            anim.SetBool("emAtaque", false);
        }
    }
}
