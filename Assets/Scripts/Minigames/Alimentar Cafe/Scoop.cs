using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScoopController : MonoBehaviour
{
    public Image scoopImage;
    public Sprite scoopVazio;
    public Sprite scoopCheio;

    public bool cheio = false;
    public bool minigameAtivo = false;

    public RectTransform canvasRect;
    public RectTransform racaoArea;
    public RectTransform poteArea;

    public PoteController pote;

    public GameObject racaoDropPrefab;
    public Transform dropsContainer;
    public Transform spawnPoint;
    bool clicando = false;
    RectTransform rt;

    void Start()
    {
        scoopImage.sprite = scoopVazio;

        rt = GetComponent<RectTransform>();
        if (canvasRect == null)
            canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        Debug.Log("SCOOP INSTANCE: " + gameObject.GetInstanceID());
        Debug.Log("[SCOOP] Start OK");


    }

    void Update()
    {
        // Debug.Log("UPDATE SCOOP: " + gameObject.GetInstanceID());
        if (!minigameAtivo) return;

        Vector2 mousePos = Input.mousePosition;

        RectTransform rt = (RectTransform)transform;
        rt.position = mousePos;

        // Debug.Log("[SCOOP] Mouse: " + mousePos);

        if (!cheio &&
            RectTransformUtility.RectangleContainsScreenPoint(racaoArea, mousePos, null))
        {
            cheio = true;
            scoopImage.sprite = scoopCheio;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (clicando) return;

            Debug.Log("[SCOOP] CLICK DETECTADO ✔");
            TentarDropar();
        }
    }

    void TentarDropar()
    {
        if (!cheio)
        {
            Debug.Log("[SCOOP] Tentou dropar mas NÃO está cheio");
            clicando = false;
            return;
        }

        Debug.Log("[SCOOP] DROP RACAO ✔");

        GameObject drop = Instantiate(racaoDropPrefab, dropsContainer);

        RectTransform dropRect = drop.GetComponent<RectTransform>();

        if (dropRect == null)
        {
            Debug.LogError("[SCOOP] PREFAB SEM RECTTRANSFORM!");
            return;
        }

        // GARANTE QUE FICA NA UI CORRETA
        dropRect.SetParent(dropsContainer, false);
        dropRect.SetAsLastSibling();

        // FIX DE VISUAL (SE ALGUMA COISA VEIO QUEBRADA DO PREFAB)
        dropRect.localScale = Vector3.one;

        dropRect.anchorMin = new Vector2(0.5f, 0.5f);
        dropRect.anchorMax = new Vector2(0.5f, 0.5f);
        dropRect.pivot = new Vector2(0.5f, 0.5f);

        // SPAWN NO SCOOP (MOUSE)
        dropRect.position = transform.position;

        Debug.Log("[SCOOP] Drop spawn pos: " + dropRect.position);
        Debug.Log("[SCOOP] Drop scale: " + dropRect.localScale);

        RacaoDrop script = drop.GetComponent<RacaoDrop>();

        if (script == null)
        {
            Debug.LogError("[SCOOP] PREFAB SEM RACAO DROP SCRIPT!");
            return;
        }

        script.rect = dropRect;
        script.pote = pote;
        script.poteArea = poteArea;

        cheio = false;
        scoopImage.sprite = scoopVazio;

        clicando = false;
    }
}