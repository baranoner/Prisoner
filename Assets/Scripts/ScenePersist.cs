using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if(numScenePersist > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }

   public void ResetPersist(){
        Destroy(gameObject);
    }
}
