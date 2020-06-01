using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask guyMask;

    [SerializeField] private Transform shotPoint;
    [SerializeField] private Transform bulletPoint;

    [SerializeField] private AudioSource shotAudio;
    [SerializeField] private AudioSource headShotAudio;

    [Header("Prefabs")]
    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject bulletPrefab;

    private GameUI ui;
    private Transform cam;
    // TODO: recargar.

    private float cooldown = 0f;


    void Start()
    {
        cam = GameManager.instance.GetCamera();
        ui = GameManager.instance.GetUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
    }

    void Shot()
    {
        if (cooldown > 0) return;
        cooldown = .2f;
        animator.SetTrigger("shot");

        shotAudio.Play();
        Instantiate(shotPrefab, shotPoint);
        Instantiate(bulletPrefab, bulletPoint);

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, Mathf.Infinity, guyMask))
        {
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            Guy guy = hit.transform.GetComponentInParent<Guy>();
            if (rb != null && guy != null)
            {
                if (!guy.IsDead())
                {
                    int score;
                    if (hit.collider.CompareTag("Head"))
                    {
                        headShotAudio.Play();
                        score = 200;
                    }
                    else
                    {
                        score = 100;
                    }

                    if (guy.IsCorrect())
                    {
                        score = -1000;
                    }
                    ui.AddScore(score);
                    guy.Die();
                }
                
                Instantiate(bloodPrefab, hit.point, Quaternion.identity);
                Vector3 force = ((hit.point + cam.forward) - hit.point).normalized * 180;
                rb.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}

