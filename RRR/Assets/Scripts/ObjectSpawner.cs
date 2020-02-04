using System.Collections.Generic;
using UnityEngine;

class SpawnMapping {
	public int Index;
	public int Min;
	public int Max;
}

public class ObjectSpawner
{
	private readonly ObstacleSpawnConfig[] _obstacles;
	private readonly ObstacleSpawnConfig[] _pickups;

	public ObjectSpawner(ObstacleSpawnConfig[] obstacles, ObstacleSpawnConfig[] pickups)
	{
		_obstacles = obstacles;
		_pickups = pickups;
	}

	public Obstacle GetNextObject()
	{
		var spawnObstacle = Random.Range(0, 100) < Config.obstacleProbability; // 0 - 99
		var objectsToChooseFrom = spawnObstacle ? _obstacles : _pickups;
		
		return PickRandom(objectsToChooseFrom);
	}

	private Obstacle PickRandom(ObstacleSpawnConfig[] objectsToChooseFrom)
	{
		List<SpawnMapping> probabilityMapping = new List<SpawnMapping>();
		int maxProbability = 0;
	
		for (int i = 0; i < objectsToChooseFrom.Length; i++)
		{
			probabilityMapping.Add(new SpawnMapping()
			{
				Index = i,
				Min = maxProbability,
				Max = maxProbability + objectsToChooseFrom[i].Probability - 1
			});
			maxProbability += objectsToChooseFrom[i].Probability;
		}
	
		int random = Random.Range(0, maxProbability);

		int indexToSpawn = -1;
		foreach(SpawnMapping m in probabilityMapping)
		{
			if (m.Min <= random && m.Max >= random)
			{
				indexToSpawn = m.Index;
			}
		}

		return objectsToChooseFrom[indexToSpawn].obstaclePrefab;
	}
}