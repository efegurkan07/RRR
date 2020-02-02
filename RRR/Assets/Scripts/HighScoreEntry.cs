
using System;
using System.Collections.Generic;

[Serializable]
public class HighScoreEntry
{
	public long Score;
}

public class HighscoreStore
{
	public List<HighScoreEntry> scores;
}