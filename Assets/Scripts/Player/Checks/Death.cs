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
    public GameObject deathLocation;
    public GameObject winScreen;
    public TextMeshProUGUI deathText;
    public static int death = 0;
    public static List<Vector3> deathSpots = new List<Vector3>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        deathText.text = death.ToString();
    }

    private void Update () {
        if (winScreen.activeSelf){
            deathSpots.Clear();
            death = 0;
        }
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

            deathSpots.Add(GameObject.FindGameObjectsWithTag("Player")[0].transform.position);
            
            foreach( var x in deathSpots){
                GameObject duplicate = Instantiate(deathLocation);
                duplicate.transform.position = x;
                duplicate.SetActive(true);
            }
            

        }
    }

    public void ResetLevel()
    {
        Timer.currentTime = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
