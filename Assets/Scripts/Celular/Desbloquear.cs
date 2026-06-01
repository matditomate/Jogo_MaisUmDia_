using UnityEngine;

public class Desbloquear : MonoBehaviour
{
    private Animator anim;
    private float tTotalDesbloq = 1.72f;
    private float tAtualDesbloq = 0f;
    private bool desbloqueando = false;
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
                Debug.Log("Celular não desbloqueado");
                anim.SetBool("desbloqueando", false);
            }
        } 
        else
        {
            scriptCel.telaDesbloqueada();
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