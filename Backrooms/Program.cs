//                       INFO                      //
using Raylib_cs;
using System.Numerics;


Raylib.InitWindow(1280, 800, "The Backrooms");
Raylib.SetTargetFPS(60);


float speed = 3.5f;
float botSpeed = 2;


Texture2D PlayerSpriteF = Raylib.LoadTexture("MainCharacterFront.png");
Texture2D monsterSprite = Raylib.LoadTexture("monster2.png");
Texture2D backgroundImage = Raylib.LoadTexture("Background1.png");
Texture2D keySprite = Raylib.LoadTexture("key.png");


Rectangle keyRect = new Rectangle(100, 700, keySprite.width, keySprite.height);
Rectangle player = new Rectangle(415, 60, PlayerSpriteF.width, PlayerSpriteF.height);
Rectangle botRect = new Rectangle(1000, 1000, 48, 48);
Rectangle bot2Rect = new Rectangle(500, 500, 48, 48);
Rectangle sceneChangeRect = new Rectangle(1075, 60, 100, 100);
string currenctScene = "welcomescreen";


Color textcolor = new Color(225, 226, 187, 255);
Color backgroundcolor = new Color(195, 203, 110, 255);
Vector2 botMovement = new Vector2(1, 0); 


bool hasKey = false;

// ----------------------------------------------------------------------------------------------------->
//                  KEYBOARD-CONTROLS                  //



while (Raylib.WindowShouldClose() == false)
{
  if (currenctScene == "level1")   // [KOM IHÅG!] Lägg till för andra nivåer på Torsdag
  {
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
      player.x += speed;
    }


    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
    {
      player.x -= speed;
    }


    if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
    {
      player.y += speed;     
    }


    if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
  {
    player.y -= speed;
  }

// ----------------------------------------------------------------------------------------------------->
  //                      SETTINGS                     //

    Vector2 playerPos = new Vector2(player.x, player.y);
    Vector2 botPos = new Vector2(botRect.x, botRect.y);
    Vector2 diff = playerPos - botPos;
    Vector2 botDirection = Vector2.Normalize(diff);


    botMovement = botDirection * botSpeed;
    botRect.x += botMovement.X;
    botRect.y += botMovement.Y;

    Vector2 bot2Pos = new Vector2(bot2Rect.x, bot2Rect.y);
    Vector2 diff2 = playerPos - bot2Pos;
    Vector2 bot2Direction = Vector2.Normalize(diff2);

    botMovement = bot2Direction * botSpeed;
    bot2Rect.x += botMovement.X;
    bot2Rect.y += botMovement.Y;

if (Raylib.CheckCollisionRecs(player, keyRect))
{
    hasKey = true;
}


// ----------------------------------------------------------------------------------------------------->
//                      TRANSITIONS                     //

        // If you collide with bot, you loose!
  if (Raylib.CheckCollisionRecs(player, botRect))
  {
    currenctScene = "defeat";
  }
}
  
        // ENTER to transition from Welcome Screen to Game!
   else if (currenctScene == "welcomescreen")
  {
    if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
    {
      currenctScene = "level1";
    }
  }

if (Raylib.CheckCollisionRecs(player, sceneChangeRect))
{
    // If the player has the key, change the scene
    if (hasKey)
    {
        currenctScene = "newScene";
    }
}

if (Raylib.CheckCollisionRecs(player, keyRect))
{
    hasKey = true;
    keyRect.x = -10000;
    keyRect.y = -10000;
}


// ----------------------------------------------------------------------------------------------------->
//                  MAP-CUSTOMIZATION                  //


  Raylib.BeginDrawing();
  Raylib.ClearBackground(backgroundcolor);
  


  if (currenctScene == "level1")
  {
    Raylib.DrawTextureEx(backgroundImage, new Vector2(0, 0), 0, 3, Color.WHITE);
    Raylib.DrawTexture(monsterSprite, (int)botRect.x, (int)botRect.y, Color.WHITE);
    Raylib.DrawTexture(monsterSprite, (int)bot2Rect.x, (int)bot2Rect.y, Color.WHITE);
    Raylib.DrawTexture(PlayerSpriteF, (int) player.x, (int) player.y, backgroundcolor);
    Raylib.DrawRectangleRec(sceneChangeRect, Color.BLACK);
    Raylib.DrawTexture(keySprite, (int)keyRect.x, (int)keyRect.y, Color.WHITE);

  } 

  else if (currenctScene =="level2")
  {
    Raylib.DrawTexture(PlayerSpriteF, (int) player.x, (int) player.y, backgroundcolor);
    Raylib.DrawTexture(monsterSprite, (int)botRect.x, (int)botRect.y, Color.WHITE);
  }


  else if (currenctScene == "welcomescreen")
  {
    Raylib.DrawText("Welcome To The Backrooms", 280, 400, 50, textcolor);
    Raylib.DrawText("\nENTER to begin", 515, 420, 32, textcolor);
  }


  else if (currenctScene == "defeat")
  {   
    Raylib.DrawText("Fin.", 540, 300, 128, Color.WHITE);
  }







  Raylib.EndDrawing();
}








// ----------------------------------------------------------------------------------------------------->
//                  TO-DO-LIST                  //



// * Borders!!
