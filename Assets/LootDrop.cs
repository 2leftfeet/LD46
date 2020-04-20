using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LootTable{
    public GameObject loot;
    public int weight;
}

public class LootDrop : MonoBehaviour
{
    public List<LootTable> lootTable;
    [Range(1,100)]
    public int chanceToDropAnything = 30;


    void Start(){
        /*int range = 0;
        for(int i = 0; i < lootTable.Count;i++){
            range += lootTable[i].weight;
        }
        range *= 100/chanceToDropAnything;
        int rand = Random.Range(0, range);
        int top = 0;
        for(int i = 0; i < lootTable.Count;i++){
            top += lootTable[i].weight;
            if(rand < top){
                //dunno
            }
        }*/
    }
    public void DropLoot(){
        int range = 0;
        for(int i = 0; i < lootTable.Count;i++){
            range += lootTable[i].weight;
        }
        range *= 100/chanceToDropAnything;
        int rand = Random.Range(0, range);
        int top = 0;
        for(int i = 0; i < lootTable.Count;i++){
            top += lootTable[i].weight;
            if(rand < top){
                Instantiate(lootTable[i].loot, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
