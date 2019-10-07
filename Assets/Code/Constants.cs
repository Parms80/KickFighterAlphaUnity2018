using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {

	public const int RUNNING = 0;
	public const int JUMPING = 1;
	public const int TAKE_HIT = 2;
	public const int ATTACKING = 3;
	public const int WALK_TO_START_POSITION = 4;
	public const int FALLING = 5;
	public const int KNOCKED_BACK = 6;
	public const int GET_UP = 7;
	public const int IDLE = 8;
	public const int DASHING = 9;

	public const float ATTACK_DISTANCE = 1.2f;

	public const int POOLOBJECT_ENEMY = 0;
	public const int POOLOBJECT_FASTENEMY = 1;
	public const int POOLOBJECT_SLOWENEMY = 2;
	public const int POOLOBJECT_REDDRAGON = 3;

	public const int MAX_OBJECTS_PER_TYPE = 10;
	public const int POINTS_FOR_ENEMY_KILL = 1;
	public const int PIXELS_PER_UNIT = 100;
	public const float INITIATE_PUNCH_DISTANCE = 1.0f;
	public const int LINECAST_END_Y = -50;
	public const int SCORE_FOR_NEXT_LEVEL = 10;
	public const int WAVES_PER_LEVEL = 3;
	public const float SPAWN_Y_POSITION = 5.0f;
	public const float HORIZONTAL_JUMP_STRENGTH = 0.0f;
	public const float ENEMY_SPRITE_WIDTH = 1.2f;
	public const float READY_X_POS = -4.0f;
	public const int FRAMES_TO_CHANGE_POWERUP_ICON = 30;
	
	public const string STRING_ATTACK = "Attack";
	public const string STRING_COMBOBAR = "Combo bar";
	public const string STRING_DASH = "Dash";
	public const string STRING_ENEMY = "Enemy";
	public const string STRING_ENEMYHITCOLLISION = "EnemyHitCollision";
	public const string STRING_ERRORSPAWNINGOBJECT = "Error spawing object: ";
	public const string STRING_FALLING = "Falling";
	public const string STRING_GROUND = "Ground";
	public const string STRING_GROUNDCHECK = "GroundCheck";
	public const string STRING_GETUP = "Get up";
	public const string STRING_HANDLEOBJECTOFFSCREEN = "HandleObjectOffScreen";
	public const string STRING_KICK = "Kick";
	public const string STRING_JUMP = "Jump";
	public const string STRING_MAKEHUMANFALL = "MakeHumanFall";
	public const string STRING_LEVEL = "Level: ";
	public const string STRING_PLAYER = "Player";
	public const string STRING_PLAYERHITCOLLISION = "PlayerHitCollision";
	public const string STRING_RUN = "Run";
	public const string STRING_RESET = "reset";
	public const string STRING_SCORE = "Score";
	public const string STRING_HUD_SCORE = "Score: ";
	public const string STRING_IDLE = "Idle";

	public const int GAMESTATE_MENU = 0;
	public const int GAMESTATE_STARTGAME = 1;
	public const int GAMESTATE_PLAY = 2;
	public const int GAMESTATE_GETREADY = 3;
	public const int GAMESTATE_GAMEOVER = 4;
	public const int GAMESTATE_TITLE = 5;
	public const int GAMESTATE_PAUSED = 6;
	public const int GAMESTATE_RESUME = 7;
	public const int GAMESTATE_NEW_TOP_SCORE = 8;
	
	public const string PLAYERPREFS_BESTSCORE_KEY = "best_score";
}
