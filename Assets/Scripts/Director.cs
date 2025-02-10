using UnityEngine;

public static class Director
{

    private static Vector2 playerPosition = new Vector2(0, 0);

    public static void UpdatePlayerPosition(Vector2 newPosition){
        playerPosition = newPosition;
    }

    public static Vector2 GetPlayerPosition(){
        return playerPosition;
    }

}
