using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PostSlot : MonoBehaviour
{
    [Header("Elementos Visuais deste Slot")]
    public Image imagemDoPost;
    public TextMeshProUGUI textoUsuario;
    public TextMeshProUGUI textoLegenda;

    public void PreencherDados(DadosPost dados)
    {
        if(textoUsuario != null) textoUsuario.text = dados.usuario;
        if(textoLegenda != null) textoLegenda.text = dados.legenda;

        if(imagemDoPost != null)
        {
            Sprite spriteCarregado = Resources.Load<Sprite>(dados.nomeDaImagem);
            if(spriteCarregado != null)
            {
                imagemDoPost.sprite = spriteCarregado;
            }
        }
    }
}
