using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DestoryFloor : MonoBehaviour
{
    Renderer renderer;
    public bool isPlayer = false;
    public float maxTime;
    float itime;
    public float destoryTime;
    bool isDestory = false;

    Color startColor;
    public Color destoryColor;

    public PhotonView pv;
    private void Awake() {
        
    }


    private void Start() {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
    }
    private void Update() {
        ColorChangeFloor();
        RemoveFloor();
    }

    private void OnCollision(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            isPlayer = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            isPlayer = false;
        }
    }

    void ColorChangeFloor(){
        if(isPlayer){
            if (itime < maxTime)
            {
                itime += (float)(Time.deltaTime);
                float t = itime / maxTime;
                renderer.material.color = Color.Lerp(startColor, destoryColor, t);
            }
            else
            {
                renderer.material.color = destoryColor;
                transform.gameObject.AddComponent<Rigidbody>();
                isDestory = true;
            }
        }
    }

    void RemoveFloor(){
        if(isDestory == true){
            if(destoryTime > 0){
                destoryTime -= Time.deltaTime;
            } 
            else{
                pv.RPC("Die", RpcTarget.AllBuffered);  
                Debug.Log("Map Destory");            
            }
        } 
    }

    [PunRPC]
    void Die() => Destroy(transform.gameObject);
    
}
