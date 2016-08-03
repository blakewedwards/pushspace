using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
  [Serializable]
  public class Count {
    public int minimum;
    public int maximum;

    public Count(int min, int max) {
      maximum = max;
      minimum = min;
    }
  }

  public int columns = 8;
  public int rows = 8;
  public Count wallCount = new Count(5, 9);
  public GameObject targetA;
  public GameObject targetB;
  public GameObject black;
  public GameObject[] floors;
  public GameObject[] walls;

  private Transform boardHolder;
  private List<Vector3> positions = new List<Vector3>();

  void InitializeList() {
    positions.Clear();

    for (int x = 1; x < columns - 1; x++) {
      for (int y = 1; y < rows - 1; y++) {
        positions.Add(new Vector3(x, y, 0f));
      }
    }
  }

  void BoardSetup() {
    boardHolder = new GameObject("Board").transform;

    for (int x = -1; x < columns + 1; x++) {
      for (int y = -1; y < rows + 1; y++) {
        GameObject toInstantiate = floors[Random.Range(0, floors.Length)];
        if (x == -1 || x == columns || y == -1 || y == rows) {
          toInstantiate = black;
        }
        GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(boardHolder);
      }
    }
  }

  Vector3 RandomPosition() {
    int randomIndex = Random.Range(0, positions.Count);
    Vector3 randomPosition = positions[randomIndex];
    positions.RemoveAt(randomIndex);
    return randomPosition;
  }

  void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum) {
    int objectCount = Random.Range(minimum, maximum + 1);

    for (int i = 0; i < objectCount; i++) {
      Vector3 randomPosition = RandomPosition();
      GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
      Instantiate(tileChoice, randomPosition, Quaternion.identity);
    }
  }

	public void SetupScene(int level) {
    BoardSetup();
    InitializeList();
    LayoutObjectAtRandom(walls, wallCount.minimum, wallCount.maximum);
    Instantiate(targetA, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
  }
}
