using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

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
        if (!ClicouEsteFrame())
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

    //  INPUT: usa o sistema que estiver ativo no projeto.
    //  Evita InvalidOperationException ao chamar Input.* com o
    //  Input System novo como único ativo (Player Settings > Active Input Handling).
    // obs: Não entendi direito mas pelo que vi é melhor

    // ----- Explicação 1: O problema do script original era que ele usava os dois ao mesmo tempo. Se o projeto estivesse só no sistema novo, 
    // qualquer chamada Input.GetMouseButtonDown lançava InvalidOperationException em todo frame. 
    // Se estivesse só no legado, nem compilava, por causa do using UnityEngine.InputSystem.

    // ----- Explicação 2:
    // #if ENABLE_INPUT_SYSTEM é uma diretiva de compilação, não um if normal. 
    // O Unity define o símbolo ENABLE_INPUT_SYSTEM automaticamente quando o Input System novo está ativo. 
    // Na hora de compilar, o código do bloco errado é descartado, como se nem existisse no arquivo.

    // ----- Explicação 3:
    // No original, essa lógica de “qual input usar” aparecia repetida em três lugares: no Update, 
    // em DetectarCliqueNoMundo e em MouseEstaSobreUI. Agora o resto do script só chama ClicouEsteFrame() e PosicaoMouseTela() 
    // e não precisa saber qual sistema está ativo. Se um dia nos trocarmos de sistema, só esses dois métodos importam.

    // ta mais centralizado resumidamente!

    private bool ClicouEsteFrame()
    {
#if ENABLE_INPUT_SYSTEM 
        return Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
#else
        return Input.GetMouseButtonDown(0);
#endif
    }

    private Vector2 PosicaoMouseTela()
    {
#if ENABLE_INPUT_SYSTEM
        return Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;
#else
        return Input.mousePosition;
#endif
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

        Vector2 mousePosTela = PosicaoMouseTela();
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

        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = PosicaoMouseTela();

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