using UnityEngine;
using UnityEngine.Events;

public class TutorialPanel : MonoBehaviour
{
    public static bool Aberto { get; private set; }
    private static int frameFechamento = -1;
    public static bool Bloqueando => Aberto || Time.frameCount <= frameFechamento;

    [SerializeField] private string idTutorial;
    [SerializeField] private bool pausarTempo = true;
    [SerializeField] private bool travarCamera = true;
    [SerializeField] private bool sempreMostrar = false;   // só para testes
    [SerializeField] private GameObject painel;

    public UnityEvent aoFinalizar;

    private int frameAbertura;
    private bool abrindo;
    private bool minigameAtivoAnterior;

    private void Awake()
    {
        if (painel == null) painel = gameObject;
        if (!abrindo) painel.SetActive(false);
    }

    public void Mostrar()
    {
        if (Aberto) return;   // ignora chamada dupla

        if (string.IsNullOrEmpty(idTutorial))
            Debug.LogWarning("[TUTORIAL] idTutorial vazio em " + name);

        string chave = "tutorial_" + idTutorial;
        bool jaViu = PlayerPrefs.GetInt(chave, 0) == 1;

        Debug.Log($"[TUTORIAL] Mostrar | id='{idTutorial}' | sempreMostrar={sempreMostrar} | visto={jaViu}");

        if (!sempreMostrar && jaViu)
        {
            aoFinalizar.Invoke();
            return;
        }

        PlayerPrefs.SetInt(chave, 1);
        PlayerPrefs.Save();

        frameAbertura = Time.frameCount;
        abrindo = true;
        Aberto = true;

        if (painel == null) painel = gameObject;
        painel.SetActive(true);

        if (travarCamera)
        {
            minigameAtivoAnterior = CameraPanLateral.minigameAtivo;
            CameraPanLateral.minigameAtivo = true;
        }

        if (pausarTempo) GameManager.instance.tempo.Pausar();
    }

    private void Update()
    {
        if (!Aberto || !painel.activeSelf) return;
        if (Time.frameCount == frameAbertura) return;

        if (Input.GetMouseButtonDown(0))
            Fechar();
    }

    private void Fechar()
    {
        frameFechamento = Time.frameCount;
        Restaurar();
        painel.SetActive(false);
        aoFinalizar.Invoke();
    }

    private void OnDisable()
    {
        Restaurar();
    }

    private void Restaurar()
    {
        if (!Aberto) return;

        Aberto = false;
        abrindo = false;

        if (travarCamera)
            CameraPanLateral.minigameAtivo = minigameAtivoAnterior;

        if (pausarTempo) GameManager.instance.tempo.Retomar();
    }
}