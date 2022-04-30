using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(0, moveSpeed * Time.deltaTime,0);

        if (transform.position.y > 7f){
            Destroy(gameObject);
            // 取得FloorManager下面的Component(也就是floorManager的cs檔)
            transform.parent.GetComponent<FloorManager>().SpawnFloor(); 
        }
    }
}
