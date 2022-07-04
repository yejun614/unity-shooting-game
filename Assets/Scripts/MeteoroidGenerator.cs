using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoroidGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    public List<GameObject> MeteoPrefabs = new List<GameObject>();

    [Header("Generator")]
    public int LimitLength = 100;
    public Vector3 GeneratePosition = Vector3.zero;

    [Header("Remove Conditions")]
    public float LimitDistance = 100;

    void Update()
    {
        int count = CheckAndRemoveMeteoes();
        if (count < LimitLength) GenerateRandomMeteo();
    }

    void GenerateRandomMeteo()
    {
        if (MeteoPrefabs.Count == 0) return;

        int randomPrefab = Random.Range(0, MeteoPrefabs.Count - 1);
        Vector3 position = transform.position + GeneratePosition;
        position.x *= Random.Range(0, 2) == 0 ? -1 : 1;
        position.y *= Random.Range(0, 2) == 0 ? -1 : 1;

        GameObject meteo = Instantiate(MeteoPrefabs[randomPrefab], position, Quaternion.identity);
        meteo.GetComponent<Meteoroid>().SetRandomParams();
        meteo.tag = "Meteoroid";
    }

    int CheckAndRemoveMeteoes()
    {
        GameObject[] meteoes = GameObject.FindGameObjectsWithTag("Meteoroid");
        List<GameObject> remove = new List<GameObject>();

        for (int i = 0; i < meteoes.Length; i ++)
        {
            float distance = Vector3.Distance(transform.position, meteoes[i].transform.position);
            if (distance >= LimitDistance) remove.Add(meteoes[i]);
        }

        for (int i = 0; i < remove.Count; i ++)
        {
            Destroy(remove[i]);
        }

        return meteoes.Length - remove.Count;
    }

    public void RemoveAll()
    {
        GameObject[] meteoes = GameObject.FindGameObjectsWithTag("Meteoroid");
        for (int i = 0; i < meteoes.Length; i ++) Destroy(meteoes[i]);
    }
}
