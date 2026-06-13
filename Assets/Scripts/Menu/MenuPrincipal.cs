using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Configurações de Cena")]
    [SerializeField] private string nomeDaCenaJogo; // Nome exato da cena que quer abrir

    [Header("Efeito de Transição")]
    [SerializeField] private Animator fadeAnimator; // Arraste o Animator do FadePainel aqui
    [SerializeField] private float tempoDeEspera = 300f; // Tempo que a animação de FadeIn demora

    // Função que o botão JOGAR vai chamar
    public void Jogar()
    {
        // Inicia a rotina que espera a animação antes de mudar de cena
        StartCoroutine(TransicaoCena());
    }

    // Função que o botão SAIR vai chamar
    public void SairDoJogo()
    {
        Debug.Log("O jogador clicou em Sair!");
        
        // Fecha o jogo se for uma build, só fecha de verdade quando o jogo for .exe.
        Application.Quit();

        // para o editor da unity, Faz o botão de Play desativar sozinho!
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // Co-rotina para dar tempo do Fade acontecer antes da cena carregar
    private IEnumerator TransicaoCena()
    {
        if (fadeAnimator != null)
        {
            // Dispara o gatilho ou toca a animação de escurecer a tela
            fadeAnimator.Play("FadeIn");
        }

        // Espera o tempo do Fade terminar antes de cortar para a próxima cena
        yield return new WaitForSeconds(tempoDeEspera);
        yield return new WaitForSeconds(tempoDeEspera); // não funcionou mudar o tempo, ai adicionei dois
        // Carrega a nova fase
        SceneManager.LoadScene(nomeDaCenaJogo);
    }
}