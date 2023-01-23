//                       INFO                      //
using Raylib_cs;
using System.Numerics;

// Standard inställningar, hur stor ska resolutionen vara och fps
Raylib.InitWindow(1280, 800, "The Backrooms");
Raylib.SetTargetFPS(60);

// Spelbara karaktären och bot's rörelse hastighet
float speed = 3.5f;
float botSpeed = 2;

// Laddar in texturerna för alla karktärer osv
Texture2D PlayerSpriteF = Raylib.LoadTexture("MainCharacterFront.png");
Texture2D monsterSprite = Raylib.LoadTexture("monster2.png");
Texture2D backgroundImage = Raylib.LoadTexture("Background1.png");
Texture2D keySprite = Raylib.LoadTexture("key.png");

// Skapar kontrollerbara reklatangar med sprites/bilderna som laddades in ^ 
Rectangle keyRect = new Rectangle(100, 700, keySprite.width, keySprite.height);
Rectangle player = new Rectangle(415, 60, PlayerSpriteF.width, PlayerSpriteF.height);
Rectangle botRect = new Rectangle(1000, 1000, 48, 48);
Rectangle bot2Rect = new Rectangle(500, 500, 48, 48);
Rectangle sceneChangeRect = new Rectangle(1075, 60, 100, 100);

// Sätter "welcomescreen" till den scenen som visas när man startar spelet
string currenctScene = "welcomescreen";

// Skapar två unika färger som sedan implementeras
Color textcolor = new Color(225, 226, 187, 255);
Color backgroundcolor = new Color(195, 203, 110, 255);
Vector2 botMovement = new Vector2(1, 0); 

// Sätter så att man i början inte har nyckeln
bool hasKey = false;

// ----------------------------------------------------------------------------------------------------->
//                  KEYBOARD-CONTROLS                  //

// Gör att karaktären kan röra på sig runt spelytan på scen "level1" bara

while (Raylib.WindowShouldClose() == false)
{
  if (currenctScene == "level1")  
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
  //                      SETTINGS                     //  ENDA KVAR

      // Två Vector-2 variablar skapas för spelaren och botens position, den hittar skillnaden mellan dem för att avögra riktningen som boten ska röra sig mot spelaren och normaliserar den riktningsvektorn.
    Vector2 playerPos = new Vector2(player.x, player.y);
    Vector2 botPos = new Vector2(botRect.x, botRect.y);
    Vector2 diff = playerPos - botPos;
    Vector2 botDirection = Vector2.Normalize(diff);


      // Beräknar rörelsen för boten genom att multiplicera dess riktningsvektor med dess hastighet, och uppdaterar sedan botens position genom att lägga till rörelsevärdena till dess x- och y-koordinater.
    botMovement = botDirection * botSpeed;
    botRect.x += botMovement.X;
    botRect.y += botMovement.Y;


      // Desamma som översta, men för Bot2
    Vector2 bot2Pos = new Vector2(bot2Rect.x, bot2Rect.y);
    Vector2 diff2 = playerPos - bot2Pos;
    Vector2 bot2Direction = Vector2.Normalize(diff2);


      // Desamma som översta, men för Bot2
    botMovement = bot2Direction * botSpeed;
    bot2Rect.x += botMovement.X;
    bot2Rect.y += botMovement.Y;


      // Om spelaren kolliderar med nyckeln så ändras "haskey" till "true" (spelaren får nyckeln)
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

  if (Raylib.CheckCollisionRecs(player, bot2Rect))
  {
    currenctScene = "defeat";
  }




  
        // ENTER to transition from Welcome Screen to Game!
   else if (currenctScene == "welcomescreen")
  {
    if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
    {
      currenctScene = "level1";
    }
  }




       // ENTER to transition from End Screen to Shut Off!
    if (currenctScene == "defeat")
  {
    if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
    {
      System.Environment.Exit(1);
    }
  }




       // ENTER to transition from Win Screen to Shut Off!
    if (currenctScene == "newScene")
  {
    if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
    {
      System.Environment.Exit(1);
    }
  }

 



if (Raylib.CheckCollisionRecs(player, sceneChangeRect))
{
    // Om spelaren har nyckeln, byt scenen
    if (hasKey)
    {
        currenctScene = "newScene";
    }
}

    // Om man kolliderar med nyckeln så flyttas nyckelns plats/kordinater till..
if (Raylib.CheckCollisionRecs(player, keyRect))
{
    hasKey = true;
    keyRect.x = -10000;
    keyRect.y = -10000;
} 


// ----------------------------------------------------------------------------------------------------->
//                  MAP-CUSTOMIZATION                


  Raylib.BeginDrawing();
  Raylib.ClearBackground(backgroundcolor);
  

// Ritar ut dom skapade reklatlagarna (bots spelbara, nyckel, dörr) med dess inladdade texturer, kordinater och färger
  if (currenctScene == "level1")
  {
    Raylib.DrawTextureEx(backgroundImage, new Vector2(0, 0), 0, 3, Color.WHITE);
    Raylib.DrawTexture(monsterSprite, (int)botRect.x, (int)botRect.y, Color.WHITE);
    Raylib.DrawTexture(monsterSprite, (int)bot2Rect.x, (int)bot2Rect.y, Color.WHITE);
    Raylib.DrawTexture(PlayerSpriteF, (int) player.x, (int) player.y, backgroundcolor);
    Raylib.DrawRectangleRec(sceneChangeRect, Color.BLACK);
    Raylib.DrawTexture(keySprite, (int)keyRect.x, (int)keyRect.y, Color.WHITE);

  } 

// Ritar text som säger man start, när programmet startas
  else if (currenctScene == "welcomescreen")
  {
    Raylib.DrawText("Welcome To The Backrooms", 280, 380, 50, textcolor);
    Raylib.DrawText("Escape by collecting the key and unlocking the door!", 200, 440, 32, textcolor);
    Raylib.DrawText("\nENTER to begin", 515, 420, 32, textcolor);
  }

// Ritar text som säger man förlorar  om man byter till "defeat" scenen
  else if (currenctScene == "defeat")
  {   
    Raylib.DrawText("YOU'VE BEEN CAUGHT!", 110, 300, 90, Color.WHITE);
    Raylib.DrawText("\nENTER to exit", 515, 420, 32, textcolor);
  }

// Ritar text som säger man vinner om man byter till "newscene" scenen
  else if (currenctScene == "newScene")
  {   
    Raylib.DrawText("YOU'VE ESCAPED!", 250, 300, 90, Color.WHITE);
    Raylib.DrawText("\nENTER to exit", 515, 420, 32, textcolor);
  }






  Raylib.EndDrawing();
}




// ----------------------------------------------------------------------------------------------------->
//                  PSEUDO-KOD                  //

// Ladda in texturer för alla karaktärer, skapa rektanglerna och färgerna som ska användas
// Gör att spelaren inte har en "key"

// Spelaren rör sig med WASD
// Fiender följer efter spelaren

// Om man kolliderar med fiende 
// så förlorar man
// Byt "scene" till förlorar skärm

// Om man plockar up "key" så försvinner den
// Gå in i dörr med key så vinner man
// Byt "scene" till vinst skärm

// Ladda in fienderna i nivå 
