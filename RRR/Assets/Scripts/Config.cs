using UnityEngine;

public static class Config
{
	public const float rightLineLimit = 20;
	public const float levelRunSpeed = 3f;
	public static float skylineRunSpeed = 0.05f;
	public static float maximumHealth = 110f;
	public static int yellowHealthIndicatorValue = 70;
	public static int redHealthIndicatorValue = 30;
	public static int damageOverTime = 1;
	public const int initialBodyPartHealth = 100;
	public const int inventoryCapacity = 6;
	//used depending on lane texture tiling
	public const float laneTilingMagicNr = 13.5f; //yes, this is a proper balancing calculation :P
	public const float robotRunSpeed = 10f * (levelRunSpeed / 7.75f); //this too!
	public static WaitForSeconds damageOverTimePeriod = new WaitForSeconds(1f);
	public const int obstacleProbability = 70;
	public const int healAmount = 20;
	
	public const int goodBadProbability = 40; // 40% good, 60% bad
	public const int humanSparePartProbability = 10; // 10% people, 90% spare parts
	public const int probabilityShift = 10; // divided by amount of other items
}