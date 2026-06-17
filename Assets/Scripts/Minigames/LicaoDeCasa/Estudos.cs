using UnityEngine;
using UnityEngine.UI;

public class Estudos : MonoBehaviour
{
    [Header("Configurações do Clicker")]
    [SerializeField] private float reducaoPorClique = 0.25f; // Quanto ela encolhe por clique (ex: 25% menor)
    [SerializeField] private float escalaMinima = 0.2f;      // Ponto em que ela some

    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Esta função deve ser associada ao botão ou componente de clique na UI da Bola
    public void ClicouNaBola()
    {
        // Reduz a escala nos eixos X e Y uniformemente
        rect.localScale -= new Vector3(reducaoPorClique, reducaoPorClique, 0f);

        // Se a bola encolheu além do limite, ela é concluída e destruída
        if (rect.localScale.x <= escalaMinima)
        {
            SucessoBola();
        }
    }

    private void SucessoBola()
    {
        // Aqui você pode somar pontos ou chamar ações no gerenciador do seu jogo
        Debug.Log("Bola destruída!");
        
        // Se quiser tocar o som global do seu AudioManager ao estourar:
        // if (AudioManager.instance != null) AudioManager.instance.TocarSFX(seuSomClip);

        Destroy(gameObject);
    }
}