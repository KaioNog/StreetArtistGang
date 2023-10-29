using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;
    private bool isDead = false;
    public GameObject gameOverScreen; 
    public GameObject winScreenCanvas; 
    private bool hasWon = false;

    private Rigidbody2D rb;
    public Animator animator;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        winScreenCanvas.SetActive(false); // Disable the win screen canvas at the start

    }

    void Update()
    {
        if (isDead) return; // Não permitir movimentação se o jogador estiver morto

        // Pular
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            animator.SetBool("Jump", true); 
        }

        if (Input.GetButtonDown("Fire1") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            animator.SetBool("Jump", true); 
        }
        // Atualizar o parâmetro de animação "Speed" com base na velocidade horizontal
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void FixedUpdate()
    {
        if (isDead) return; // Não permitir movimentação se o jogador estiver morto

        // Movimentação automática para a direita
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar se o personagem está tocando no chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("Jump", false); 
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector2 contactPoint = collision.contacts[0].normal;
            if (contactPoint.y < 0.5f)
            {
                Die();
            }
            else
            {
                isJumping = false;
            }
        }

        if (collision.gameObject.CompareTag("Damage"))
        {
            Die();
        }

        if(collision.gameObject.CompareTag("autoJump"))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("Jump", false); 
        }

        if (collision.gameObject.CompareTag("Finish") && !hasWon)
        {
            hasWon = true;
            winScreenCanvas.SetActive(true); // Ativa o canvas da tela de vitória
            Time.timeScale = 0f; // Pausa o jogo
        }
    }

    public void Flip()
    {
        animator.SetTrigger("Flip"); 
        Debug.Log("Flip");
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        Debug.Log("Game over");
        gameOverScreen.SetActive(true); // Ativa a tela de Game Over
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia a cena atual
    }
}