using UnityEngine;

public class Global
{
    public static float OUT_OF_FIELD = -30.0f;
    public static float LEFT_WALL_X = -15.0f;
    public static float RIGHT_WALL_X = 15.0f;

    public enum PanelAttribute
    {
        Fire,
        Water,
        Earth,
    }
    public static int PANEL_MAX = 3;
    public static Vector3 DISPLAY_ELEMENT_POSITION = new Vector3(20f, 1f, -10f);
}
