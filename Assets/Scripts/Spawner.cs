using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TimeOut 
{
    public float min;
    public float max;

    public float Value
    {
        get { return Random.Range(min, max); }
    }
}

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int numSpawns;
    public TimeOut timeOut;

    public List<GameObject> spawns = new List<GameObject>();

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(spawnCoroutine());
    }

    IEnumerator spawnCoroutine ()
    {
        while (isActiveAndEnabled)
        {
            yield return Spawn();
            yield return new WaitForSeconds(timeOut.Value);
        }
    }

    IEnumerator Spawn ()
    {
        for (int i = 0; i < numSpawns; i++)
        {
            var spawn = Instantiate(prefab, this.transform.position, this.transform.rotation);
            spawns.Add(spawn);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
