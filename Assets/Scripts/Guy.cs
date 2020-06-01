using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guy : MonoBehaviour
{
    [SerializeField] private GameObject facemaskPrefab;
    [SerializeField] private List<Transform> facemaskSlots;
    [SerializeField] private List<AudioClip> deathSoundClips;

    private AudioSource audioSource;
    private NavMeshAgent agent;
    private Animator animator;
    private GuyRagdoll ragdoll;

    private bool isDead = false;
    private int randomSlot;

    void Awake()
    {
        ragdoll = transform.GetComponent<GuyRagdoll>();
        animator = transform.GetComponent<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
        audioSource = transform.GetComponent<AudioSource>();
    }

    void Start()
    {
        agent.speed = 3;

        randomSlot = Random.Range(0, facemaskSlots.Count);
        Instantiate(facemaskPrefab, facemaskSlots[randomSlot]);
        GameManager.instance.AddGuy();

        int clipIndex = Random.Range(0, deathSoundClips.Count);
        audioSource.clip = deathSoundClips[clipIndex];
    }

    
    void Update()
    {
        if (isDead) return;
        if (agent.destination == null) return;

        float distance = (agent.destination - transform.position).magnitude;
        if (distance <= 1f)
        {
            GameManager.instance.RemoveGuy();
            Destroy(gameObject);
        }

    }

    public void SetDestination(Vector3 destination) {
        agent.SetDestination(destination);
    }
    public void Die()
    {
        if (isDead) return;
        audioSource.Play();
        isDead = true;
        Destroy(animator);
        Destroy(agent);
        ragdoll.Enable();
        StartCoroutine(Remove());
    }

    public bool IsCorrect()
    {
        return randomSlot == 0;
    }

    public bool IsDead()
    {
        return isDead;
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
        GameManager.instance.RemoveGuy();
        Destroy(gameObject);
    }
}
