using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;

    public Slider healthSlider;
    public Slider easeHealthSlider;

    public float maxHP = 100f;
    public float HP;
    private float lerpSpeed = 0.05f;
    private Rigidbody rbplayer;

    //public GameObject GameOverCanvas;
    public GameObject Player;
    public GameObject Enemy;

    void Start()
    {
        HP = maxHP;
        healthSlider.maxValue = maxHP;
        healthSlider.value = HP;
        easeHealthSlider.maxValue = maxHP;
        easeHealthSlider.value = HP;
        Rigidbody rbplayer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (healthSlider.value != HP)
        //{
        //    healthSlider.value = HP;
        //}
        //if (Input.GetKeyUp(KeyCode.K))
        //{
        //    PlayerTakeDamage(10);
        //}
        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, HP, lerpSpeed);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //        PlayerTakeDamage(10);
    //}

    public void TakeDamage(float damage)
    {
        Debug.Log("Player took damage: " + damage + ". Current HP: " + HP);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
        healthSlider.value = HP;
    }
    void Die()
    {
        Debug.Log("Player died!");
        //GameOverCanvas.SetActive(true);
        Player.SetActive(false);
        Enemy.SetActive(false);
        // rbplayer.isKinematic = true;

    }
}
