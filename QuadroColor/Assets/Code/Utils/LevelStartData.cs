using System;

[Serializable]
public class LevelStartData
{
    public LevelSettings level;

    public bool isIncludeDiagonale;
    public string[] playerNames = new string[2];

    public LevelStartData()
    {
        isIncludeDiagonale = true;
    }
}
