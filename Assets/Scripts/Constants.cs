using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants 
{
    public static string POINTERS_POSITION = "PointersPosition";

    public const string MAIN_SCENE = "MainScene";

    public const string SCENES_MANAGER = "ScenesManager";
    public const string STATE_MANAGER = "StateManager";
    public const string USER_MANAGER = "UserManager";
    public const string UI_MANAGER = "UIManager";
    
    public const string MOVE_BUTTON_ACTION = "MoveButtonAction";
    public const string START_POINTERS_SETTINGS_ACTION = "StartPointersSettingsAction";
    public const string COMPLETE_POINTERS_SETTINGS_ACTION = "CompletePointersSettingsAction";
    public static object START_ACT_CUBE_ACTION = "StartActAction";
    public static object COMPLETE_ACT_CUBE_ACTION = "CompleteActAction";
    public static object SWITCH_BUTTON_ACTION = "SwitchButtonAction";

    public const string MOVING_SCENE_PATH = "Prefabs/Scenes/MovingScene";
    public const string ROTATING_SCENE_PATH = "Prefabs/Scenes/RotatingScene";
    
    public const string MOVE_WINDOW_PATH = "Prefabs/UI/MoveWindow";
    public const string ROTATE_WINDOW_PATH = "Prefabs/UI/RotateWindow";
    
    public const string DEFAULT_CUBE_SPEED = "4";
    public const string DEFAULT_CUBE_SPEED_ROTATION = "100";
    public const string DEFAULT_CUBE_RADIUS = "2";
    public const string DEFAULT_CUBE_AMOUNT_ROTATION = "2";
}
