using System.Collections.Generic;

public enum PassiveWeapons
{
    Pitchfork,
    Scythe,
    Shovel
}

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

public class Data
{
    public float HP;
    public int EXP;
    public float speed;
}

public class PlayerData : Data
{
    public float maxHP;
    public int maxEXP;
    public float findRange;
    public int killCount;
    public int level;
}

public class MonsterData : Data
{
    public float power;
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
    public static int charSelect = 3;
    public static float playerX = 19f;
    public static float playerY = 19f;
    public static float playerStandDelay = 0.5f;
    public static float playerRunDelay = 0.2f;

    //Active Weapon
    public static float activeX = 0.2f;
    public static float activeY = -0.2f;
    public static float fireposX = 0.8f;
    public static float fireposY = 0.11f;

    //Passive Weapon
    public static float passiveSpace = 2f;
    public static float passiveSpin = 3f;
    public static float passiveYtravel = 0.5f;

    //Game
    public static int stage = 1;

    //Bullet
    public static float bulletRange = 20f;

    //Item
    public static float pickupDistance = 3f;
    public static float collectDistance = 0.3f;
    public static float itemSpeed = 5f;

    //EXP
    public static readonly Dictionary<EXPtype, int> EXPvalue
        = new Dictionary<EXPtype, int>
        {
            {EXPtype.BRONZE, 1},
            {EXPtype.SILVER, 5},
            {EXPtype.GOLD, 25}
        };
    public static float expSpawnRange = 1.0f;
}
