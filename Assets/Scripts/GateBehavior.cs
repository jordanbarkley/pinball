using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehavior : MonoBehaviour
{

    public Material activeMaterial;
    public Material inactiveMaterial;
    public MeshRenderer meshRenderer;
    public BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        // gate should be inactive to start
        SetInactive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other) {
        // make sure the collider is the ball's
        if (other.gameObject.name == "Ball") {
            // enable gate
            SetActive();
        }
    }

    public void SetInactive() {
        // visual update
        meshRenderer.material = inactiveMaterial;

        // disable collsion
        boxCollider.isTrigger = true;
    }

    public void SetActive() {
        // visual update
        meshRenderer.material = activeMaterial;

        // disable collsion
        boxCollider.isTrigger = false;
    }
}
