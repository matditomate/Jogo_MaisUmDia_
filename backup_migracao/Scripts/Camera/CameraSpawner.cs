using UnityEngine;

public class CameraSpawner : MonoBehaviour
{
    void Start()
    {
        if (GameManager.instance != null && !string.IsNullOrEmpty(GameManager.instance.portaDeDestino))
        {
            GameObject portaEntrada = GameObject.Find(GameManager.instance.portaDeDestino);

            if (portaEntrada != null)
            {
                Vector3 novaPosicao = transform.position;

                novaPosicao.x = portaEntrada.transform.position.x;
                
                transform.position = novaPosicao;
            }
        }
    }
}