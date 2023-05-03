using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string userName;

    public static GameManager instance;
    private void Awake() {
        if(instance != null){
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(instance);
    }
}