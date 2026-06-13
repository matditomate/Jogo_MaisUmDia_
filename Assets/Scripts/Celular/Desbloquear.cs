using UnityEngine;

public class Desbloquear : MonoBehaviour
{
    private Animator anim;
    private float tTotalDesbloq = 1f;
    public float tAtualDesbloq = 0f;
    public bool desbloqueando = false;
    private Celular scriptCel;
    
    void Start()
    {
        scriptCel = Object.FindAnyObjectByType<Celular>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (tAtualDesbloq <= tTotalDesbloq)
        {
            if (desbloqueando)
            {
                tAtualDesbloq += Time.deltaTime;
                Debug.Log(tAtualDesbloq);
                anim.SetBool("desbloqueando", true);
            } 
            else
            {
                tAtualDesbloq = 0f;
                anim.SetBool("desbloqueando", false);
            }
        } 
        else
        {
            scriptCel.TelaDesbloqueada();
        }
    }

    public void OnPointerDownEffect()
    {
        desbloqueando = true;
    }

    public void OnPointerUpEffect()
    {
        desbloqueando = false;
    }
}