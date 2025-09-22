using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for UI elements
using TMPro; // for TextMeshPro


public class PlayerController : PhysicsBase
{
    Rigidbody2D rb;
    public int coinCount = 0;
    public TMP_Text coinText; // linked in Inspector

    public int maxHealth = 3;
    public int currentHealth;

    public Image[] hearts; // Assign in Inspector
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // public float knockbackForce = 2f;
    
    public float knockbackDuration = 2f;

    public float knockbackForceX;
    public float knockbackForceY;

    private bool isKnockedBack = false;
    

    public bool canMove = false; // controlled by GameMenuManager
    public GameMenuManager gameMenuManager;


    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinUI();
        currentHealth = maxHealth;
        UpdateHearts();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        desiredx = 0;

        if (Input.GetAxis("Horizontal") > 0) desiredx = 3;
        if (Input.GetAxis("Horizontal") < 0) desiredx = -3;
        if (Input.GetButton("Jump") && grounded) velocity.y = 6.5f;
    }

    public void Collide(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            Debug.Log("Level Complete!");
            gameMenuManager.ShowEndMenu();
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            coinCount++;
            UpdateCoinUI();
            Destroy(other.gameObject); // remove the coin
            Debug.Log("Coin Collected!");
        }

        if (other.gameObject.CompareTag("Food"))
        {
            currentHealth++;
            if (currentHealth > maxHealth) currentHealth = maxHealth; // cap health at three

            UpdateHearts();
            Destroy(other.gameObject); // remove the food
            Debug.Log("Power Up!");
        }
        
        if (other.gameObject.CompareTag("Lethal"))
        {
            Debug.Log("Player Hit a Hazard!");
            // Vector2 direction = (transform.position - other.transform.position).normalized;
            TakeDamage(1, other.transform);
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = $"<sprite=8> {coinCount}";
    }

    public void TakeDamage(int damage, Transform hazard)
    {
        if (isKnockedBack) return; // prevent stacking knockbacks

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHearts();

        // Apply knockback
        StartCoroutine(Knockback(hazard));
    }

    void UpdateHearts()
    {
        hearts[0].GetComponent<Image>().sprite = emptyHeart;
        Debug.Log(currentHealth);

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
                Debug.Log("Health Updated");
            }
            else
            {
                hearts[i].sprite = emptyHeart;
                Debug.Log("Health Updated");
            }
        }
        if (currentHealth <= 0)
        {
            FindObjectOfType<GameMenuManager>().ShowGameOverMenu();
        }
    }

    IEnumerator Knockback(Transform hazard)
    {
        isKnockedBack = true;

        // Direction: -1 = left, 1 = right
        float directionX = (transform.position.x - hazard.position.x > 0) ? 1 : -1; // get direction away from hazard

        // Apply knockback force directly
        rb.velocity = new Vector2(directionX * knockbackForceX, knockbackForceY);

        // rb.AddForce(new Vector2(directionX * knockbackForceX, knockbackForceY), ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration); // wait for duration

        rb.velocity = Vector2.zero; // stop movement after knockback

        isKnockedBack = false;
        Debug.Log(rb.velocity);
    }

    public override void CollideHorizontal(Collider2D other)
    {   
        Collide(other);
    }

    public override void CollideVertical(Collider2D other)
    {
        Collide(other);
    }
}
