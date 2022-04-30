using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField]  GameObject[] floorPrefabs;
    public void SpawnFloor(){
        int random = Random.Range(0, floorPrefabs.Length);

        // 創建物件: Instantiate(物件, 創建為FloorManager的子物件)
        GameObject floor = Instantiate(floorPrefabs[random], transform);
        
        floor.transform.position = new Vector3(Random.Range(-5f, 5f), -6f, 0f);
    }
}
