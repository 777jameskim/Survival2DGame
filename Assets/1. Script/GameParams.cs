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
public class Data
{
    public float HP;
    public float EXP;
    public float speed;
}

public class PlayerData: Data
{
    public float maxEXP;
    public float findRange;
    public int killCount;
    public int level;
}
    
public class MonsterData : Data
{
    public float hitDelayTime;
    public float attackRange;
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


    public static int stage;
}
