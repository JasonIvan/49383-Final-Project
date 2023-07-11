using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{
    public Vector3 translationSpeed;

    public Vector3 rotationSpeed;

    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float translationX = translationSpeed.x * Time.deltaTime;
        float translationY = translationSpeed.y * Time.deltaTime;
        float translationZ = translationSpeed.z * Time.deltaTime;

        transform.Translate(translationX, translationY, translationZ);


        float rotationX = rotationSpeed.x * Time.deltaTime;
        float rotationY = rotationSpeed.y * Time.deltaTime;
        float rotationZ = rotationSpeed.z * Time.deltaTime;

        transform.Rotate(rotationX, rotationY, rotationZ);



        if(Input.GetKeyDown(KeyCode.X) && targetObject != null)
        {
            transform.LookAt(targetObject.transform);
        }
        
    }
}
