using UnityEngine;

public class CameraSpawner : MonoBehaviour
{
    void Start()
    {
        if (GameManager.instance != null && !string.IsNullOrEmpty(DataManager.instance.portaDeDestino))
        {
            GameObject portaEntrada = GameObject.Find(DataManager.instance.portaDeDestino);

            if (portaEntrada != null)
            {
                Vector3 novaPosicao = transform.position;

                novaPosicao.x = portaEntrada.transform.position.x;
                
                transform.position = novaPosicao;
            }
        }
    }
}