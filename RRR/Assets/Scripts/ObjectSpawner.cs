﻿using System.Collections.Generic;
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
	private readonly int _probabilityShift;

	public ObjectSpawner(int goodBadProbability, int humanSparePartProbability, int probabilityShift, ObstacleSpawnConfig[] obstacles, ObstacleSpawnConfig[] pickups, ObstacleSpawnConfig[] people)
	{
		// create copy
		_obstacles = obstacles.ToArray();
		_pickups = pickups.ToArray();
		_people = people.ToArray();

		_goodBadProbability = goodBadProbability;
		_humanSparePartProbability = humanSparePartProbability;
		_probabilityShift = probabilityShift;
	}

	public Obstacle NextObjectToSpawn()
	{
		var spawnGood = Random.Range(0, 100) < _goodBadProbability;
		_goodBadProbability = AdjustProbability(spawnGood, _goodBadProbability, _probabilityShift);
		return spawnGood ? GetGoodItem() : GetBadItem();
	}

	private Obstacle GetBadItem()
	{
		return PickRandom(_obstacles);
	}
	
	private Obstacle GetGoodItem()
	{
		var spawnPeople = Random.Range(0, 100) < _humanSparePartProbability;
		_humanSparePartProbability = AdjustProbability(spawnPeople, _humanSparePartProbability, _probabilityShift);
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

		var objectToSpawn = objectsToChooseFrom[indexToSpawn];
		objectToSpawn.Probability -= _probabilityShift;

		if (objectToSpawn.Probability < 0) objectToSpawn.Probability = 0;

		var numObjectsToAdjust = objectsToChooseFrom.Length - 1;
		float adjustmentPerObject = (_probabilityShift * 1f) / numObjectsToAdjust;
		for (int i = 0; i < objectsToChooseFrom.Length; i++)
		{
			if (i != indexToSpawn)
			{
				objectsToChooseFrom[i].Probability += Mathf.FloorToInt(adjustmentPerObject);
				if (objectsToChooseFrom[i].Probability > 100) objectsToChooseFrom[i].Probability = 100;
			}
		}

		return objectToSpawn.obstaclePrefab;
	}

	private int AdjustProbability(bool left, int probability, int adjustment)
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