using UnityEngine;
using UnityEngine.InputSystem; // Adicionamos a biblioteca moderna de controles

public class Paddle : MonoBehaviour
{
    public float velocidade = 10f;
    public bool isPaddleEsquerda; // Vamos marcar essa caixinha apenas na raquete da esquerda

    void Update()
    {
        // Se não houver teclado conectado, não faz nada para evitar erros
        if (Keyboard.current == null) return;

        // Se for o Paddle da Esquerda, usamos as teclas W e S
        if (isPaddleEsquerda)
        {
            if (Keyboard.current.wKey.isPressed)
            {
                transform.Translate(Vector2.up * (velocidade * Time.deltaTime));
            }
            else if (Keyboard.current.sKey.isPressed)
            {
                transform.Translate(Vector2.down * (velocidade * Time.deltaTime));
            }
        }
        // Se não for o da esquerda (ou seja, for o da direita), usamos as Setas
        else
        {
            if (Keyboard.current.upArrowKey.isPressed)
            {
                transform.Translate(Vector2.up * (velocidade * Time.deltaTime));
            }
            else if (Keyboard.current.downArrowKey.isPressed)
            {
                transform.Translate(Vector2.down * (velocidade * Time.deltaTime));
            }
        }
    }
}