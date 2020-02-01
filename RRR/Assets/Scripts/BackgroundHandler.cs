using System.Collections.Generic;
using UnityEngine;

public class BackgroundHandler : MonoBehaviour
{
	[SerializeField] private Material _skylineMaterial;
	[SerializeField] private GameObject[] _buildingPrefabs;
	[SerializeField] private Transform _buildingsContainer;
	private float _secondsToNextBuilding = 0f;
	private List<Transform> _spawnedBuildings = new List<Transform>();

	private void Update()
	{
		_skylineMaterial.mainTextureOffset =
			new Vector2((_skylineMaterial.mainTextureOffset.x + Time.deltaTime * Config.skylineRunSpeed) % 1, 1);

		_spawnedBuildings.ForEach(b => b.localPosition += Vector3.left * Config.levelRunSpeed * Time.deltaTime);

		_secondsToNextBuilding -= Time.deltaTime;
		if (_secondsToNextBuilding <= 0)
		{
			_spawnedBuildings.Add(Instantiate(_buildingPrefabs[Random.Range(0, _buildingPrefabs.Length)], _buildingsContainer).transform);
			_secondsToNextBuilding = Random.Range(0.3f, 1.5f) * Config.levelRunSpeed;
		}
	}

	private void OnDestroy()
	{
		_skylineMaterial.mainTextureOffset = Vector2.one;
	}
}