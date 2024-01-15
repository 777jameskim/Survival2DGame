public enum PlayerState
{
    Stand,
    Run,
    Dead
}

public enum MonsterState
{
    Run,
    Hit,
    Dead
}

public static class GameParams
{
    //Camera
    public static float cameraX = 11f;
    public static float cameraY = 15f;
    public static float cameraZ = -10f;

    //Player
    public static float playerX = 19f;
    public static float playerY = 19f;
    public static float playerStandDelay = 0.5f;
    public static float playerRunDelay = 0.2f;
    public static float playerSpeed = 5f;
    public static float playerHP;
    public static float playerMaxEXP;
    public static float playerEXP;
    public static float playerFindDistance = 3f;
    public static int killCount;
    public static int level;


    // Monster
    public static float monsterSpeed = 2f;
    public static float monsterHP;
    public static float monsterEXP;
    public static float monsterHitDelayTime;

    public static int stage;
}
