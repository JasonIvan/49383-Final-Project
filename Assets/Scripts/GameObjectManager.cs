using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public GameObject objectToModify;

    public BoxCollider boxColliderToModify;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleObjectActivation()
    {
        if(objectToModify != null)
        {
            objectToModify.SetActive(!objectToModify.activeSelf);
        }
    }

    public void ToggleBoxCollider()
    {
        if(boxColliderToModify != null)
        {
            boxColliderToModify.enabled = !boxColliderToModify.enabled;
        }
    }
}
