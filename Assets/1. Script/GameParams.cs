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

public enum GameState
{
    Play,
    Pause,
    Stop
}

public enum ActiveWeapons
{

}

public enum PassiveWeapons
{

}

public class Data
{
    public float HP;
    public float EXP;
    public float speed;
    public float power;
}

public class PlayerData : Data
{
    public float maxHP;
    public float maxEXP;
    public float findRange;
    public int killCount;
    public int level;
    public float fireSpeed;
}

public class MonsterData : Data
{
    public float hitDelayTime;
    public float attackRange;
    public float attackDelay;
}

public static class GameParams
{
    //UI
    public static GameState state = GameState.Stop;
    public static float hpBarX = 120f;
    public static float hpBarY = 30f;
    public static float expBarX = 3800f;
    public static float expBarY = 100f;

    //Camera
    public static float cameraX = 11f;
    public static float cameraY = 15f;
    public static float cameraZ = -10f;

    //Player
    public static int charSelect = 0;
    public static float playerX = 19f;
    public static float playerY = 19f;
    public static float playerStandDelay = 0.5f;
    public static float playerRunDelay = 0.2f;
    public static float passiveSpace = 3f;
    public static float passiveSpin = 3f;

    //Game
    public static int stage = 1;

    //Item
    public static float pickupDistance = 3f;
    public static float collectDistance = 0.3f;
    public static float itemSpeed = 5f;
}
