using UnityEngine;

public class Gol : MonoBehaviour
{
    public GameManager gameManager;
    public bool isGolEsquerda;

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Bola"))
        {
            gameManager.MarcarPonto(isGolEsquerda);
        }
    }
}