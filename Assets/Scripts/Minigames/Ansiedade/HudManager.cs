using UnityEngine;

public class HudManager : MonoBehaviour
{
    public static HudManager instance;
    [SerializeField] private GameObject efeitoAnsiedade;
    [SerializeField] private AnsiedadeAnim animacaoAnsiedade;

    public bool ProntoParaAnsiedade =>
        efeitoAnsiedade != null && animacaoAnsiedade != null && animacaoAnsiedade.minigame != null;

    void Awake()
    {
        if (instance != null)
        {
            if (instance.ProntoParaAnsiedade || !ProntoParaAnsiedade)
            {
                Destroy(gameObject);
                return;
            }

            instance.gameObject.SetActive(false);
            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AtualizarAnsiedade(bool emCrise, bool saiuDaCrise)
    {
        if ((!emCrise && !saiuDaCrise) || !ProntoParaAnsiedade)
            return;

        if (emCrise)
            efeitoAnsiedade.SetActive(true);

        animacaoAnsiedade.AtualizarEstadoAnsiedade();

        if (saiuDaCrise)
            efeitoAnsiedade.SetActive(false);
    }
}
