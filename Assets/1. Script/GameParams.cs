public enum PlayerState
{
    Stand,
    Run,
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
    public static float playerSpeed = 3f;
}
