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

	private int _goodBadProbability;
	private int _humanSparePartProbability;
	
	public ObjectSpawner(int goodBadProbability, int humanSparePartProbability, ObstacleSpawnConfig[] obstacles, ObstacleSpawnConfig[] pickups, ObstacleSpawnConfig[] people)
	{
		// create copy
		_obstacles = obstacles.ToArray();
		_pickups = pickups.ToArray();
		_people = people.ToArray();

		_goodBadProbability = goodBadProbability;
		_humanSparePartProbability = humanSparePartProbability;
	}

	public Obstacle NextObjectToSpawn()
	{
		var spawnGood = Random.Range(0, 100) < _goodBadProbability;
		// update probability for loser

		_goodBadProbability = adjustProbability(spawnGood, _goodBadProbability, 10);
		
		return spawnGood ? GetGoodItem() : GetBadItem();
	}

	private Obstacle GetBadItem()
	{
		return PickRandom(_obstacles);
	}
	
	private Obstacle GetGoodItem()
	{
		var spawnPeople = Random.Range(0, 100) < _humanSparePartProbability;
		
		_humanSparePartProbability = adjustProbability(spawnPeople, _humanSparePartProbability, 10);
		
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

	private int adjustProbability(bool left, int probability, int adjustment)
	{
		if (left)
		{
			probability -= adjustment;
		}
		else
		{
			probability += adjustment;
		}

		if (probability > 100) probability = 100;
		else if (probability < 0) probability = 0;

		return probability;
	}
}