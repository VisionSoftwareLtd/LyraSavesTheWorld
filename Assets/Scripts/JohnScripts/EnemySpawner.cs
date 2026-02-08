using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private Enemy[] enemiesToSpawn;
  [SerializeField] private float spawnInterval = 2f;
  [SerializeField] private float spawnDistanceFromEdge = 1f;

  private float nextSpawnTime;
  private Camera mainCamera;
  private BoxCollider2D spawnArea;

  void Awake()
  {
    spawnArea = GetComponent<BoxCollider2D>();
  }

  void Start()
  {
    mainCamera = Camera.main;
    SetNextSpawnTime();
  }

  void Update()
  {
    if (Time.time >= nextSpawnTime)
    {
      SpawnEnemy();
      SetNextSpawnTime();
    }
  }

  void SetNextSpawnTime()
  {
    int currentMobCount = transform.childCount;
    if (currentMobCount < PersistentData.instance.superSpawnIfMobCountBelow)
    {
      nextSpawnTime = Time.time + spawnInterval / 5f;
    }
    else
    {
      nextSpawnTime = Time.time + spawnInterval;
    }
  }

  void SpawnEnemy()
  {
    if (enemiesToSpawn == null || enemiesToSpawn.Length == 0)
      return;

    int maxIndex = Mathf.Min(GameManager.instance.GameStage, enemiesToSpawn.Length - 1);
    int enemyIndex = Random.Range(0, maxIndex + 1);
    Enemy enemyPrefab = enemiesToSpawn[enemyIndex];

    Vector3 spawnPosition = GetRandomSpawnPosition();
    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
  }

  Vector3 GetRandomSpawnPosition()
  {
    // Get screen bounds in world space
    float camHeight = mainCamera.orthographicSize;
    float camWidth = camHeight * mainCamera.aspect;
    Vector3 worldSpawnPos = Vector3.zero;
    for (int attempts = 0; attempts < 10; attempts++)
    {
      // Choose a random edge: 0 = top, 1 = bottom, 2 = left, 3 = right
      int edge = Random.Range(0, 4);
      Vector3 spawnPos = Vector3.zero;

      switch (edge)
      {
        case 0: // Top
          spawnPos = new Vector3(
            Random.Range(-camWidth, camWidth),
            camHeight + spawnDistanceFromEdge,
            0f
          );
          break;
        case 1: // Bottom
          spawnPos = new Vector3(
            Random.Range(-camWidth, camWidth),
            -camHeight - spawnDistanceFromEdge,
            0f
          );
          break;
        case 2: // Left
          spawnPos = new Vector3(
            -camWidth - spawnDistanceFromEdge,
            Random.Range(-camHeight, camHeight),
            0f
          );
          break;
        case 3: // Right
          spawnPos = new Vector3(
            camWidth + spawnDistanceFromEdge,
            Random.Range(-camHeight, camHeight),
            0f
          );
          break;
      }

      worldSpawnPos = mainCamera.transform.position + spawnPos;
      worldSpawnPos.z = 0f;
      if (spawnArea == null || spawnArea.bounds.Contains(worldSpawnPos))
      {
        return worldSpawnPos;
      }
    }

    // Clamp position to be inside the spawn area bounds
    if (spawnArea != null)
    {
      Bounds bounds = spawnArea.bounds;
      worldSpawnPos.x = Mathf.Clamp(worldSpawnPos.x, bounds.min.x, bounds.max.x);
      worldSpawnPos.y = Mathf.Clamp(worldSpawnPos.y, bounds.min.y, bounds.max.y);
    }

    return worldSpawnPos;
  }
}
