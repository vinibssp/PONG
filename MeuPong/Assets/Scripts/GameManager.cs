using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Telas e Objetos")]
    public GameObject painelMenu;
    public GameObject painelFimDeJogo;
    public GameObject elementosDoJogo;

    [Header("UI do Menu")]
    public TMP_InputField inputNomeEsquerda;
    public TMP_InputField inputNomeDireita;
    public TMP_Dropdown dropCorEsquerda;
    public TMP_Dropdown dropCorDireita;
    public TextMeshProUGUI textoRecorde;

    [Header("UI do Jogo e Fim")]
    public TextMeshProUGUI textoPlacar;
    public TextMeshProUGUI textoVencedor;

    [Header("Referências do Jogo")]
    public Bola scriptBola;
    public SpriteRenderer paddleEsquerda;
    public SpriteRenderer paddleDireita;

    private int pontuacaoEsquerda = 0;
    private int pontuacaoDireita = 0;
    private int pontuacaoMaxima = 5; // O jogo acaba em 5 pontos

    void Start()
    {
        MostrarMenu();
    }

    public void MostrarMenu()
    {
        painelMenu.SetActive(true);
        painelFimDeJogo.SetActive(false);
        elementosDoJogo.SetActive(false); // Esconde o jogo
        CarregarDados();
    }

    public void IniciarJogo()
    {
        // Salva os nomes no computador
        PlayerPrefs.SetString("NomeEsq", inputNomeEsquerda.text);
        PlayerPrefs.SetString("NomeDir", inputNomeDireita.text);
        PlayerPrefs.Save();

        // Aplica as cores escolhidas
        AplicarCor(paddleEsquerda, dropCorEsquerda.value);
        AplicarCor(paddleDireita, dropCorDireita.value);

        // Prepara a tela para jogar
        painelMenu.SetActive(false);
        painelFimDeJogo.SetActive(false);
        elementosDoJogo.SetActive(true);

        // Zera o placar
        pontuacaoEsquerda = 0;
        pontuacaoDireita = 0;
        textoPlacar.text = pontuacaoEsquerda + " - " + pontuacaoDireita;

        scriptBola.ResetarPosicao();
    }

    void AplicarCor(SpriteRenderer paddle, int corEscolhida)
    {
        switch (corEscolhida)
        {
            case 0: paddle.color = Color.white; break;
            case 1: paddle.color = Color.red; break;
            case 2: paddle.color = Color.blue; break;
            case 3: paddle.color = Color.green; break;
        }
    }

    public void MarcarPonto(bool golNaEsquerda)
    {
        if (golNaEsquerda) pontuacaoDireita++;
        else pontuacaoEsquerda++;

        textoPlacar.text = pontuacaoEsquerda + " - " + pontuacaoDireita;

        // Checa se alguém ganhou
        if (pontuacaoEsquerda >= pontuacaoMaxima || pontuacaoDireita >= pontuacaoMaxima)
        {
            FimDeJogo();
        }
        else
        {
            scriptBola.ResetarPosicao();
        }
    }

    void FimDeJogo()
    {
        elementosDoJogo.SetActive(false);
        painelFimDeJogo.SetActive(true);

        string vencedor = pontuacaoEsquerda > pontuacaoDireita ? inputNomeEsquerda.text : inputNomeDireita.text;
        int pontosDoVencedor = Mathf.Max(pontuacaoEsquerda, pontuacaoDireita);

        textoVencedor.text = vencedor + " VENCEU!";

        // Verifica e salva o recorde
        int recordeAtual = PlayerPrefs.GetInt("RecordePontos", 0);
        if (pontosDoVencedor > recordeAtual)
        {
            PlayerPrefs.SetInt("RecordePontos", pontosDoVencedor);
            PlayerPrefs.SetString("Recordista", vencedor);
            PlayerPrefs.Save();
        }
    }

    public void ResetarDados()
    {
        PlayerPrefs.DeleteAll(); // Apaga tudo
        CarregarDados();
    }

    void CarregarDados()
    {
        inputNomeEsquerda.text = PlayerPrefs.GetString("NomeEsq", "Jogador 1");
        inputNomeDireita.text = PlayerPrefs.GetString("NomeDir", "Jogador 2");

        int recorde = PlayerPrefs.GetInt("RecordePontos", 0);
        string recordista = PlayerPrefs.GetString("Recordista", "Ninguém");

        textoRecorde.text = "Recorde: " + recorde + " pts (" + recordista + ")";
    }

    void Update()
    {
        // Só permite usar o 'R' se estiver jogando
        if (elementosDoJogo.activeSelf && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            pontuacaoEsquerda = 0;
            pontuacaoDireita = 0;
            textoPlacar.text = "0 - 0";
            scriptBola.ResetarPosicao();
        }
    }
}