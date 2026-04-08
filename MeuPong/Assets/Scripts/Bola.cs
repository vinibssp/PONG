using UnityEngine;

public class Bola : MonoBehaviour
{
    public float velocidade = 8f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void LancarBola()
    {
        // Escolhe -1 ou 1 aleatoriamente para direção
        float x = Random.Range(0, 2) == 0 ? -1f : 1f;
        float y = Random.Range(0, 2) == 0 ? -1f : 1f;

        // linearVelocity é a propriedade correta no Unity 6.3 (substitui o antigo .velocity)
        rb.linearVelocity = new Vector2(velocidade * x, velocidade * y);
    }

    public void ResetarPosicao()
    {
        transform.position = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        Invoke(nameof(LancarBola), 1f); // Espera 1 segundo e lança
    }
}