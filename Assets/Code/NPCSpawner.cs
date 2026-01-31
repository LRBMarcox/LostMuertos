using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class NPCSpawner : MonoBehaviour {
    public GameObject spiritPrefab;
    public GameObject impostorPrefab;
    public int totalNPC = 10;
    public int impostorCount = 2;
    public Vector2 mapMin;
    public Vector2 mapMax;
    public float minDistance = 1.5f;

    private List<Vector2> usedPositions = new();

    void Start()
    {
        SpawnAll();
    }

    void SpawnAll()
    {
        for (int i = 0; i < totalNPC; i++) {
            bool isImpostor = i < impostorCount;
            GameObject prefab = isImpostor ? impostorPrefab : spiritPrefab;

            Vector2 pos2D = FindValidPosition();
            Vector3 pos3D = new Vector3(pos2D.x, pos2D.y, -2f);
            Instantiate(prefab, pos3D, Quaternion.identity);
            usedPositions.Add(pos3D);
        }
    }

    Vector2 FindValidPosition()
    {
        for (int i = 0; i < 50; i++) {
            Vector2 pos = new Vector2(
                Random.Range(mapMin.x, mapMax.x),
                Random.Range(mapMin.y, mapMax.y)
            );

            bool valid = true;
            foreach (var used in usedPositions) {
                if (Vector2.Distance(pos, used) < minDistance) {
                    valid = false;
                    break;
                }
            }

            if (valid) return pos;
        }

        return Vector2.zero;
    }
}
