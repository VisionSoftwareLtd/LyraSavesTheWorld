using UnityEngine;

public class WallSpawner : MonoBehaviour
{
  [SerializeField] private Wall[] wallPrefabs;
  private float wallSize = 3f;
  private BoxCollider2D spawnArea;

  void Awake()
  {
    spawnArea = GetComponent<BoxCollider2D>();
  }

  void Start()
  {
    SpawnCentralWalls();
    SpawnBoundaryWalls();
  }

  private void SpawnCentralWalls()
  {
    int widthCount = Mathf.CeilToInt(spawnArea.size.x / wallSize);
    int heightCount = Mathf.CeilToInt(spawnArea.size.y / wallSize);

    for (int i = 0; i < widthCount; i++)
    {
      float x = spawnArea.bounds.min.x + i * wallSize;
      {
        for (int j = 0; j < heightCount; j++)
        {
          if (IgnoreArea(i, j)) continue;
          float y = spawnArea.bounds.min.y + j * wallSize;
          {
            Wall prefab = wallPrefabs[Random.Range(0, wallPrefabs.Length)];
            Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity, transform);
          }
        }
      }
    }
  }

  private bool IgnoreArea(int i, int j)
  {
    // Ensure centre is clear
    if (i == 5 && j == 5) return true;

    // Ensure ritual zones are clear
    if (i == 1 && j >= 0 && j <= 2) return true;
    if (j == 1 && i >= 0 && i <= 2) return true;

    if (i == 1 && j >= 8 && j <= 10) return true;
    if (j == 9 && i >= 0 && i <= 2) return true;

    if (i == 9 && j >= 0 && j <= 2) return true;
    if (j == 1 && i >= 8 && i <= 10) return true;

    if (i == 9 && j >= 8 && j <= 10) return true;
    if (j == 9 && i >= 8 && i <= 10) return true;
    return false;
  }

  private void SpawnBoundaryWalls()
  {
    float halfBoundarySize = spawnArea.size.x / 2f;
    for (float x = -halfBoundarySize; x <= halfBoundarySize; x += wallSize)
    {
      Instantiate(wallPrefabs[1], new Vector3(x, halfBoundarySize, 0), Quaternion.identity, transform);
      Instantiate(wallPrefabs[1], new Vector3(x, -halfBoundarySize, 0), Quaternion.identity, transform);
    }
    for (float y = -halfBoundarySize; y <= halfBoundarySize; y += wallSize)
    {
      Instantiate(wallPrefabs[2], new Vector3(halfBoundarySize, y, 0), Quaternion.identity, transform);
      Instantiate(wallPrefabs[2], new Vector3(-halfBoundarySize, y, 0), Quaternion.identity, transform);
    }
  }
}