using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    private GameObject Disposable;

    private float firerate = 0.2f;

    Coroutine firingCoroutine;

    private float speed = 5f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerBounds();
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
            newLaser.transform.parent = Disposable.transform;
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
}
