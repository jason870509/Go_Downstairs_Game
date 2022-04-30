using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float health;
    [SerializeField] GameObject HpBar;
    [SerializeField] Text scoreText;
    GameObject currentFloor;
    int score;
    float scoreTime;
    Animator animate;
    SpriteRenderer render;

    AudioSource deathSound;

    [SerializeField] GameObject replayButton;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        score = 0;
        scoreTime = 0;
        animate =  GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        deathSound = GetComponent<AudioSource>();
    } 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)){
            transform.Translate(moveSpeed*Time.deltaTime, 0, 0);

            render.flipX = false;
            animate.SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.A)){
            transform.Translate(-moveSpeed*Time.deltaTime, 0, 0);

            render.flipX = true;
            animate.SetBool("run", true);
        }
        else {
            animate.SetBool("run", false);
        }
        
        UpdateScore();
    }

    void OnCollisionEnter2D(Collision2D other) // 判斷是否碰撞
    {
        if (other.gameObject.tag == "Normal_Floor"){
            if (other.contacts[0].normal == new Vector2(0f, 1f)){
                // Debug.Log(other.contacts[0].normal); // 碰撞的法線 (往上就是(0,1))
                // Debug.Log(other.contacts[1].normal);
                Debug.Log("撞到 Normal_Floor !!");
                currentFloor = other.gameObject;
                 
                ModifyHealth(1);
                other.gameObject.GetComponent<AudioSource>().Play();

            }
        }
        else if (other.gameObject.tag == "Nails_Floor"){
            if (other.contacts[0].normal == new Vector2(0f, 1f)){
                // Debug.Log(other.contacts[0].normal);
                // Debug.Log(other.contacts[1].normal);
                Debug.Log("撞到 Nails_Floor !!");
                currentFloor = other.gameObject;
                
                ModifyHealth(-3);
                animate.SetTrigger("hurt");
                other.gameObject.GetComponent<AudioSource>().Play();
                
            }
        }
        else if (other.gameObject.tag == "Ceiling"){
            Debug.Log("撞到天花板了 !!");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;

            ModifyHealth(-3);
            animate.SetTrigger("hurt");
            other.gameObject.GetComponent<AudioSource>().Play();
            
        }
        
        
        
    }

    void OnTriggerEnter2D(Collider2D other) { // 判斷是否經過
        if (other.gameObject.tag == "DeathLine"){
            Debug.Log("你輸了!!");
            Die();
        }
    }

    void ModifyHealth(int num) {
        health += num;
        if (health > 10) {
            health = 10;
        }
        else if (health <= 0) {
            health = 0;
            Die();
        }

        UpdateHpBar();
    }

    void UpdateHpBar() {
         for (int i = 0; i < HpBar.transform.childCount; i++){
             if (health > i){
                 HpBar.transform.GetChild(i).gameObject.SetActive(true);
             }
             else {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
             }
         }
    }

    void UpdateScore() {
        scoreTime += Time.deltaTime;
        if (scoreTime > 5f) {
            score += 1;
            scoreTime = 0f;
            scoreText.text = "地下" + score.ToString() + "層";
        } 
    }

    void Die() {
        deathSound.Play();
        Time.timeScale = 0f;
        replayButton.SetActive(true);
    }

    public void Replay() {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}
