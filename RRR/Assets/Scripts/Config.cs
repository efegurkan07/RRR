public static class Config
{
	public const float rightLineLimit = 20;
	public const float secondsPerLevel = 100f;
	public const float levelRunSpeed = 3f;
	public const int inventoryCapacity = 5;
	//used depending on lane texture tiling
	public const float laneTilingMagicNr = 13.5f; //yes, this is a proper balancing calculation :P
	public const float robotRunSpeed = 10f * (levelRunSpeed / 7.75f); //this too!
}