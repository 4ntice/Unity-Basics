using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    public void OnDestroy(GameObject go)
    {
        if(go == null)
        {
            go = this.gameObject;
        }

        Destroy(go);
    }
}
