using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 paralaxEffectM;


    private Transform cameraTransform;
    private Vector3 lastPositionCamera;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastPositionCamera = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastPositionCamera;
        transform.position += new Vector3(deltaMovement.x * paralaxEffectM.x, deltaMovement.y * paralaxEffectM.y);
        lastPositionCamera = cameraTransform.position;
    }
}
