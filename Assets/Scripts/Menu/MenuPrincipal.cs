using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Configurações de Cena")]
    [SerializeField] private string nomeDaCenaJogo; // Nome exato da cena que quer abrir

    [Header("Efeito de Transição")]
    [SerializeField] private Animator fadeAnimator; // Arraste o Animator do FadePainel aqui
    [SerializeField] private float tempoDoFade = 1f; // Tempo que a sua animação de fade demora (ex: 1 segundo)

    [Header("Aviso de Conteúdo Sensível")]
    [SerializeField] private GameObject ObjectAvisoSensivel;
    [SerializeField] private float tempoExibicaoAviso = 10f; // Tempo que o aviso fica na tela

    private void Start()
    {
        // Garante que o aviso comece desativado ao iniciar o menu
        if (ObjectAvisoSensivel != null)
        {
            ObjectAvisoSensivel.SetActive(false);
        }
    }

    // Função que o botão JOGAR vai chamar
    public void Jogar()
    {
        StartCoroutine(TransicaoComAviso());
    }

    // Função que o botão SAIR vai chamar
    public void SairDoJogo()
    {
        Debug.Log("O jogador clicou em Sair!");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // Nova co-rotina com o fluxo completo do aviso
    private IEnumerator TransicaoComAviso()
    {
        if (fadeAnimator != null)
        {
            // Escurece a tela 
            fadeAnimator.Play("FadeIn");
            yield return new WaitForSeconds(tempoDoFade);
            yield return new WaitForSeconds(tempoDoFade);

            // Ativa o aviso em background 
            if (ObjectAvisoSensivel != null)
            {
                ObjectAvisoSensivel.SetActive(true);
            }

            // Clareia a tela para mostrar o aviso
            fadeAnimator.Play("FadeOut");
            yield return new WaitForSeconds(tempoDoFade);
            yield return new WaitForSeconds(tempoDoFade);
        }
        else
        {
            // Se não houver animator, só ativa o aviso direto
            if (ObjectAvisoSensivel != null) ObjectAvisoSensivel.SetActive(true);
        }

        // Espera os 10 segundos para o jogador ler
        yield return new WaitForSeconds(tempoExibicaoAviso);
        yield return new WaitForSeconds(tempoExibicaoAviso);


        if (fadeAnimator != null)
        {
            // Escurece a tela de novo (FadeIn) para esconder o aviso
            fadeAnimator.Play("FadeIn");
            yield return new WaitForSeconds(tempoDoFade);
            yield return new WaitForSeconds(tempoDoFade);

        }

        // Carrega a nova fase
        SceneManager.LoadScene(nomeDaCenaJogo);
    }
}