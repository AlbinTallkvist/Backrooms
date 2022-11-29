//                       INFO                      //
using Raylib_cs;
using System.Numerics;


Raylib.InitWindow(1280, 800, "The Backrooms");
Raylib.SetTargetFPS(60);


float speed = 3.5f;
float botSpeed = 2;


Texture2D playerSprite = Raylib.LoadTexture("Biker1.png");
Rectangle player = new Rectangle(0, 60, playerSprite.width, playerSprite.height);
Rectangle botRect = new Rectangle(700, 500, 64, 64);
string currenctScene = "welcomescreen";


Color textcolor = new Color(225, 226, 187, 255);
Color backgroundcolor = new Color(195, 203, 110, 255);
Vector2 botMovement = new Vector2(1, 0); 



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




// ----------------------------------------------------------------------------------------------------->
//                  MAP-CUSTOMIZATION                  //


  Raylib.BeginDrawing();
  Raylib.ClearBackground(backgroundcolor);


  if (currenctScene == "level1")
  {
    Raylib.DrawTexture(playerSprite, (int) player.x, (int) player.y, backgroundcolor);

    Raylib.DrawRectangleRec(botRect, Color.WHITE);
  }

  else if (currenctScene =="level2")
  {
    Raylib.DrawTexture(playerSprite, (int) player.x, (int) player.y, backgroundcolor);

    Raylib.DrawRectangleRec(botRect, Color.WHITE);
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



// * Multiple Levels - 
// -> Movement on multiple levels
// Make bot have skin
// Multiple enemies on first map?
