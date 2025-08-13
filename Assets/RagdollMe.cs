using System.Collections.Generic;
using UnityEngine;

public class RagdollMe : MonoBehaviour
{
    private Joint[] joints;
    private Dictionary<Joint, float> previousForce;

    public float totalScore;
    private void Awake()
    {
        joints = GetComponentsInChildren<Joint>();
        previousForce = new Dictionary<Joint, float>();
        foreach (var joint in joints)
        {
            previousForce.Add(joint, joint.currentForce.magnitude);
        }

        SetChildColliders(enabled: false);
        SetChildRigidBodies(enabled: false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetChildColliders(enabled: true);
            SetChildRigidBodies(enabled: true);
        }
    }

    public void FixedUpdate()
    {


        foreach (var joint in joints)
        {
            float lastForce = previousForce[joint];
            float difference = Mathf.Abs(f:lastForce - joint.currentForce.magnitude);
            previousForce[joint] = joint.currentForce.magnitude;
            if (difference > 1000)
            {
                totalScore += difference;
            }
            
        }

    }

    void SetChildRigidBodies(bool enabled)
    {
        var currentRB = GetComponent<Rigidbody>();
        currentRB.isKinematic = enabled;
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            if (rb != currentRB)
            {
                rb.isKinematic = !enabled;
            }
        }
    }
    void SetChildColliders(bool enabled)
    {

        Collider currentCol = GetComponent<Collider>();
        foreach (var collider in GetComponentsInChildren<Collider>())
        {
            if (collider != currentCol)
            {
                collider.enabled = enabled;
            }
        }
    }


}
