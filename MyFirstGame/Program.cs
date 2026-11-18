using Raylib_cs;
using System.Globalization;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

class FinishLine
{
    private int x;
    private int y;
    private int width = 30;
    private int height = 600;



    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public int Width
    {
        get { return width; }
        set { width = value; }
    }

    public int Height
    {
        get { return height; }
        set { height = value; }
    }



    public FinishLine(int startX, int startY)
    {
        x = startX;
        y = startY;
    }


    public void Draw()
    {
        Raylib.DrawRectangle(x, y, width, height, Color.Blue);
    }

}







class Coin
{
    private int x;
    private int y;
    private int width = 10;
    private int height = 15;
    public bool isCollected = true;


    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public int Width
    {
        get { return width; }
        set { width = value; }
    }

    public int Height
    {
        get { return height; }
        set { height = value; }
    }

    public Coin(int startX, int startY)
    {
        x = startX;
        y = startY;
    }

    public void Draw()
    {
        if(isCollected == true)
        {
            Raylib.DrawEllipse(x, y, width, height, Color.Orange);

        }
    }



}








class Wall
{
    private int x;
    private int y;
    private int width = 40;
    private int height = 490;

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }


    public int Width
    {
        get { return width; } 
    }

    public int Height
    {
        get { return height; }
    }




    public Wall(int StartX, int StartY)
    {
        x = StartX;
        y = StartY;
    }


    public void Draw()
    {
        Raylib.DrawRectangle(x, y, width, height, Color.Gray);
    }

}



class Square
{
    private int x;
    private int y;
    private int size = 25;

    public int X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }

    public Square(int startX, int startY) 
    {
        x = startX;
        y = startY;
    }

    public void Draw()
    {
        Raylib.DrawRectangle(x, y, size, size, Color.Red);
    }
}


class Program
{
    static void Main()
    {

        Raylib.InitWindow(800, 600, "My First Game");
        Raylib.SetTargetFPS(60);

        //player ანუ კვადრატი
        Square player = new Square(50, 50);

        //wall ანუ ლაბირინთის კედელი
        List<Wall> walls = new List<Wall>();
        walls.Add(new Wall(150, 0));
        walls.Add(new Wall(300, 110));
        walls.Add(new Wall(450, 0));
        walls.Add(new Wall(600, 110));

        //Coin ქოინი
        List<Coin> coins = new List<Coin>();
        coins.Add(new Coin(100, 155));
        coins.Add(new Coin(100, 400));
        coins.Add(new Coin(170, 550));
        coins.Add(new Coin(250, 370));
        coins.Add(new Coin(250, 170));
        coins.Add(new Coin(320, 50));
        coins.Add(new Coin(395, 200));
        coins.Add(new Coin(395, 400));
        coins.Add(new Coin(470, 550));
        coins.Add(new Coin(550, 360));
        coins.Add(new Coin(550, 180));
        coins.Add(new Coin(620, 50));
        coins.Add(new Coin(700, 180));


        FinishLine finishLine = new FinishLine(770, 0);

        bool gameWon = false;

        while (!Raylib.WindowShouldClose())
        {

            // მარჯვნივ მოძრაობა
            if (Raylib.IsKeyDown(KeyboardKey.Right) && !gameWon && player.X < 775)
            {
                bool hittingAnyWall = false;

                foreach (Wall currentWall in walls)
                {
                    if ((player.X + 5 + 25 > currentWall.X) &&
                        (player.X + 5 < currentWall.X + currentWall.Width) &&
                        (player.Y + 25 > currentWall.Y) &&
                        (player.Y < currentWall.Y + currentWall.Height))
                    {
                        hittingAnyWall = true;
                    }
                }

                // ციკლის გარეთ
                if (!hittingAnyWall)
                {
                    player.X += 5;
                }
            }

            // მარცხნივ მოძრაობა (გამოკლება X ღერძზე)
            if (Raylib.IsKeyDown(KeyboardKey.Left) && !gameWon && player.X > 0)
            {
                bool hittingAnyWall = false;

                foreach (Wall currentWall in walls)
                {
                    if ((player.X - 5 + 25 > currentWall.X) &&
                        (player.X - 5 < currentWall.X + currentWall.Width) &&
                        (player.Y + 25 > currentWall.Y) &&
                        (player.Y < currentWall.Y + currentWall.Height))
                    {
                        hittingAnyWall = true;
                    }
                }

                if (!hittingAnyWall)
                {
                    player.X -= 5;
                }
            }

            // ზემოთ მოძრაობა (გამოკლება Y ღერძზე, X რჩება უცვლელი)
            if (Raylib.IsKeyDown(KeyboardKey.Up) && !gameWon && player.Y > 0)
            {
                bool hittingAnyWall = false;

                foreach (Wall currentWall in walls)
                {
                    if ((player.X + 25 > currentWall.X) &&
                        (player.X < currentWall.X + currentWall.Width) &&
                        (player.Y - 5 + 25 > currentWall.Y) &&
                        (player.Y - 5 < currentWall.Y + currentWall.Height))
                    {
                        hittingAnyWall = true;
                    }
                }

                if (!hittingAnyWall)
                {
                    player.Y -= 5;
                }
            }

            // ქვემოთ მოძრაობა (მიმატება Y ღერძზე, X რჩება უცვლელი)
            if (Raylib.IsKeyDown(KeyboardKey.Down) && !gameWon && player.Y < 575)
            {
                bool hittingAnyWall = false;

                foreach (Wall currentWall in walls)
                {
                    if ((player.X + 25 > currentWall.X) &&
                        (player.X < currentWall.X + currentWall.Width) &&
                        (player.Y + 5 + 25 > currentWall.Y) &&
                        (player.Y + 5 < currentWall.Y + currentWall.Height))
                    {
                        hittingAnyWall = true;
                    }
                }

                if (!hittingAnyWall)
                {
                    player.Y += 5;
                }
            }


            foreach (Coin currentCoin in coins)
            {
                bool touchingCoin = (player.X + 25 > currentCoin.X) &&
                                    (player.X < currentCoin.X + currentCoin.Width) &&
                                    (player.Y + 25 > currentCoin.Y) &&
                                    (player.Y < currentCoin.Y + currentCoin.Height);

                if (touchingCoin)
                {
                    currentCoin.isCollected = false;
                }
            }


            bool hittingFinishLine = (player.X + 25 > finishLine.X) &&
                                       (player.X < finishLine.X + finishLine.Width);

            if (hittingFinishLine)
            {
                gameWon = true;
            }




            // ეკრანზე დახატვა ყველაფრის
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.RayWhite);


            foreach (Coin currentCoin in coins)
            {
                currentCoin.Draw();
            }

            finishLine.Draw();

            foreach(Wall currentWall in walls)
            {
                currentWall.Draw();
            }

            player.Draw();

            if (gameWon)
            {
                Raylib.DrawText("YOU WIN!", 250, 250, 60, Color.Green);
            }

            Raylib.EndDrawing();

        }



        Raylib.CloseWindow();

    }
}
