using UnityEngine;

public class BackgroundHandler : MonoBehaviour
{
	[SerializeField] private Material _skylineMaterial;

	private void Update()
	{
		_skylineMaterial.mainTextureOffset =
			new Vector2((_skylineMaterial.mainTextureOffset.x + Time.deltaTime * Config.skylineRunSpeed) % 1, 1);
	}

	private void OnDestroy()
	{
		_skylineMaterial.mainTextureOffset = Vector2.one;
	}
}