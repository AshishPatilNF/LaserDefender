using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player Laser")]
    [SerializeField]
    private GameObject laserPrefab;

    Coroutine firingCoroutine;

    private float firerate = 0.2f;

    [Header("Movement")]
    [SerializeField]
    private float speed = 5f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 0.5f;

    [Header("Health")]
    [SerializeField]
    private int health = 1000;

    [SerializeField]
    private TextMeshProUGUI text;

    [Header("Audio")]
    [SerializeField]
    AudioClip laserAudioClip;

    [SerializeField]
    AudioClip playerDeathAudioClip;

    private Level level;

    private EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<Level>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        playerBounds();
        health = 1000;
        text.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    private void Shooting()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(Refire());
        }

        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator Refire()
    {
        while(true)
        {
            GameObject newLaser = Instantiate(laserPrefab, transform.position + new Vector3(0, 0.65f, 0), Quaternion.identity);
            newLaser.transform.parent = enemySpawner.CleanUpContainer();
            AudioSource.PlayClipAtPoint(laserAudioClip, Camera.main.transform.position, 0.1f);
            yield return new WaitForSeconds(firerate);
        }
    }

    private void Movement()
    { 
        float xPosi = transform.position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float yPosi = transform.position.y + Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position = new Vector2(Mathf.Clamp(xPosi, xMin, xMax), Mathf.Clamp(yPosi, yMin, yMax));
    }

    private void playerBounds()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damaging = other.GetComponent<DamageDealer>();

        if (damaging)
        {
            health -= damaging.Damage();
            if(health < 0)
            {
                health = 0;
            }
            text.text = health.ToString();

            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(playerDeathAudioClip, Camera.main.transform.position);
                level.LoadGameOver();
                Destroy(this.gameObject);
            }
        }
    }
}
