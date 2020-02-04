using System.Collections.Generic;
using System.Linq;
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
	private readonly ObstacleSpawnConfig[] _people;
	
	public ObjectSpawner(ObstacleSpawnConfig[] obstacles, ObstacleSpawnConfig[] pickups, ObstacleSpawnConfig[] people)
	{
		// create copy
		_obstacles = obstacles.ToArray();
		_pickups = pickups.ToArray();
		_people = people.ToArray();
	}

	public Obstacle NextObjectToSpawn()
	{
		var spawnGood = Random.Range(0, 100) < Config.goodBadProbability;
		// update probability for loser
		return spawnGood ? GetGoodItem() : GetBadItem();
	}

	private Obstacle GetBadItem()
	{
		return PickRandom(_obstacles);
	}
	
	private Obstacle GetGoodItem()
	{
		var spawnPeople = Random.Range(0, 100) < Config.humanSparePartProbability;
		return spawnPeople ? GetPerson() : GetSparePart();
	}
	
	private Obstacle GetPerson()
	{
		return PickRandom(_people);
	}
	
	private Obstacle GetSparePart()
	{
		return PickRandom(_pickups);
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