using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteWeight{
    public List<Sprite> sprites;
    public int weight;
    public bool alwaysRenderBelow;
    public bool hasCollider;
    public bool spawnsSimilarNear;
    public Vector2 colliderOffset;
    public Vector2 colliderSize;
    public Material defaultMat;
}

[ExecuteInEditMode]
public class SpawnScenery : MonoBehaviour
{
    public List<SpriteWeight> spriteWeights;

    public int propCount;
    public float radius;
    public float nearRadius;
    public Transform parent;

    [ContextMenu("Spawn Props")]
    public void SpawnProps()
    {
        int weightSum = 0;
        foreach(var prop in spriteWeights)
        {
            weightSum += prop.weight;
        }

        

        for(int i = 0; i < propCount; i++)
        {
             //Choose random prop type based on weights
            int index = 0;
            int randomWeight = Random.Range(0, weightSum);
            int lastIndex = spriteWeights.Count - 1;
            while(index < lastIndex)
            {
                if(randomWeight < spriteWeights[index].weight)
                {
                    break;
                }
                randomWeight -= spriteWeights[index].weight;
                index++;
            }
            
            //Choose random sprite from the prop
            Sprite toSpawn = spriteWeights[index].sprites[Random.Range(0, spriteWeights[index].sprites.Count)];

            var newProp = new GameObject();
            var spriteRenderer = newProp.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = toSpawn;
            spriteRenderer.material = spriteWeights[index].defaultMat;
            if(spriteWeights[index].alwaysRenderBelow)
            {
                spriteRenderer.sortingOrder = -1;
            }
            newProp.name = toSpawn.name;

            newProp.AddComponent<Unclipper>();
            
            if(spriteWeights[index].hasCollider)
            {
                var col = newProp.AddComponent<BoxCollider2D>();
                col.offset = spriteWeights[index].colliderOffset;
                col.size = spriteWeights[index].colliderSize;
            }

            var spawnPos = Random.insideUnitCircle * radius;
            newProp.transform.position = spawnPos;
            newProp.transform.SetParent(parent);
            newProp.isStatic = true;

            if(spriteWeights[index].spawnsSimilarNear)
            {
                for(int j = 0; j < 3; ++j)
                {
                    var newPos = Random.insideUnitCircle * nearRadius;
                    var similarProp = Instantiate(newProp, spawnPos + newPos, Quaternion.identity);
                    Sprite similarSprite = spriteWeights[index].sprites[Random.Range(0, spriteWeights[index].sprites.Count)];

                    similarProp.GetComponent<SpriteRenderer>().sprite = similarSprite;
                    similarProp.name = similarSprite.name;
                    similarProp.transform.SetParent(parent);
                    similarProp.isStatic = true;
                
                }
            }

        }
    }
}
