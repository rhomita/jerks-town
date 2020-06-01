using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyRagdoll : MonoBehaviour
{
    [SerializeField] private Transform root;
    
    void Start()
    {
        Disable();
    }

    public void Enable()
    {
        Toggle(true);
    }

    public void Disable()
    {
        Toggle(false);
    }

    void Toggle(bool disable)
    {
        Rigidbody[] rbs = root.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = !disable;
        }

        Collider[] colliders = root.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = !disable;
        }
    }
}
