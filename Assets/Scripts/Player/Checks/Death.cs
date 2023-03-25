using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Death : MonoBehaviour
{
    public bool isDead = false;
    private Rigidbody2D rb;
    private Animator animator; 

    public TextMeshProUGUI deathText;
    public static int death = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        deathText.text = death.ToString();
    }


    private void LateUpdate()
    {
        if (isDead)
        {
            GetComponent<Move>().enabled = false;
            GetComponent<Jump>().enabled = false;
            GetComponent<WallMovement>().enabled = false;
            GetComponent<CollisionData>().enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
            animator.SetBool("isJumping", false);
            animator.SetBool("onWall", false);
            animator.SetBool("dead", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Death")
        {
            isDead = true;
            death = death +1;
            PlayerPrefs.SetInt("death", death);
            

        }
    }

    public void ResetLevel()
    {
        Timer.currentTime = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
