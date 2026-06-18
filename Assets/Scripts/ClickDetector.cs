using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickDetector : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private bool bloquearCliqueDuranteMinigame = true;
    [SerializeField] private bool bloquearCliqueQuandoMouseEstaSobreUI = false;
    [SerializeField] private bool mostrarDebug = true;

    [Header("Camada dos objetos clicáveis")]
    [SerializeField] private LayerMask camadaInterativos = ~0;

    [Header("Camera")]
    [SerializeField] private Camera cameraPrincipal;

    private void Awake()
    {
        AtualizarCameraPrincipal();
    }

    private void Start()
    {
        AtualizarCameraPrincipal();
    }

    private void Update()
    {
        bool clicou = false;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            clicou = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            clicou = true;
        }

        if (!clicou)
            return;

        if (mostrarDebug)
            Debug.Log("Clique detectado pelo ClickDetector.");

        if (bloquearCliqueDuranteMinigame && CameraPanLateral.minigameAtivo)
        {
            if (mostrarDebug)
                Debug.Log("Clique ignorado: CameraPanLateral.minigameAtivo está TRUE.");

            return;
        }

        if (bloquearCliqueQuandoMouseEstaSobreUI && MouseEstaSobreUI())
        {
            if (mostrarDebug)
                Debug.Log("Clique ignorado: mouse está sobre UI.");

            return;
        }

        DetectarCliqueNoMundo();
    }

    private void AtualizarCameraPrincipal()
    {
        if (cameraPrincipal == null)
            cameraPrincipal = Camera.main;

        if (cameraPrincipal == null)
        {
            Debug.LogWarning("ClickDetector: nenhuma câmera com tag MainCamera foi encontrada.");
        }
    }

    private void DetectarCliqueNoMundo()
    {
        if (cameraPrincipal == null)
            AtualizarCameraPrincipal();

        if (cameraPrincipal == null)
            return;

        Vector2 mousePosTela;

        if (Mouse.current != null)
            mousePosTela = Mouse.current.position.ReadValue();
        else
            mousePosTela = Input.mousePosition;

        Vector2 mousePosMundo = cameraPrincipal.ScreenToWorldPoint(mousePosTela);

        if (mostrarDebug)
        {
            Debug.Log("Mouse tela: " + mousePosTela);
            Debug.Log("Mouse mundo: " + mousePosMundo);
        }

        Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosMundo, camadaInterativos);

        if (colliders.Length == 0)
        {
            if (mostrarDebug)
                Debug.Log("Clique no mundo, mas nenhum Collider2D foi encontrado.");

            return;
        }

        if (mostrarDebug)
        {
            Debug.Log("Colliders encontrados:");

            foreach (Collider2D collider in colliders)
            {
                Debug.Log(
                    "- " + collider.name +
                    " | Layer: " + LayerMask.LayerToName(collider.gameObject.layer)
                );
            }
        }

        foreach (Collider2D collider in colliders)
        {
            ObjetoInterativo interativo = collider.GetComponent<ObjetoInterativo>();

            if (interativo == null)
                interativo = collider.GetComponentInParent<ObjetoInterativo>();

            if (interativo == null)
                interativo = collider.GetComponentInChildren<ObjetoInterativo>();

            if (interativo != null)
            {
                if (mostrarDebug)
                {
                    Debug.Log(
                        "ObjetoInterativo encontrado: " +
                        interativo.gameObject.name +
                        " | Collider clicado: " +
                        collider.name
                    );
                }

                interativo.AoClicar();
                return;
            }
        }

        if (mostrarDebug)
        {
            Debug.Log("Collider encontrado, mas nenhum tinha ObjetoInterativo.");
        }
    }

    private bool MouseEstaSobreUI()
    {
        if (EventSystem.current == null)
        {
            if (mostrarDebug)
                Debug.Log("Não existe EventSystem.current na cena.");

            return false;
        }

        Vector2 mousePosTela;

        if (Mouse.current != null)
            mousePosTela = Mouse.current.position.ReadValue();
        else
            mousePosTela = Input.mousePosition;

        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = mousePosTela;

        List<RaycastResult> resultados = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, resultados);

        if (resultados.Count == 0)
            return false;

        if (mostrarDebug)
        {
            Debug.Log("UI encontrada embaixo do mouse:");

            foreach (RaycastResult resultado in resultados)
            {
                Debug.Log("- " + resultado.gameObject.name);
            }
        }

        return true;
    }
}