using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D spriteSheet;
    Rectangle sourceRect;

    Vector2 playerPosition = new Vector2(0, 0);
    Vector2 playerVelocity = Vector2.Zero;

    float gravity = 500f;
    float moveSpeed = 100f;
    bool isOnGround = false;

    int playerSize = 32;

    Rectangle[] platforms;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteSheet = Content.Load<Texture2D>("spritesheet");
        sourceRect = new Rectangle(0, 0, 8, 8);

        platforms =
        [
            new Rectangle(0, 450, 32, 32),
            new Rectangle(32, 450, 32, 32),
            new Rectangle(32*2, 450, 32, 32),
            new Rectangle(32*3, 450, 32, 32),
            new Rectangle(32*4, 450, 32, 32),
            new Rectangle(32*5, 450, 32, 32),
            new Rectangle(32*6, 450, 32, 32),
            new Rectangle(32*7, 450, 32, 32),
            new Rectangle(32*8, 450, 32, 32),
            new Rectangle(32*9, 450, 32, 32),
            new Rectangle(32*10, 450, 32, 32),
            new Rectangle(32*11, 450, 32, 32),
            new Rectangle(32*12, 450, 32, 32),
            new Rectangle(32*13, 450, 32, 32),
            new Rectangle(32*14, 450, 32, 32),
            new Rectangle(32*15, 450, 32, 32),
            new Rectangle(32*16, 450, 32, 32),
            new Rectangle(32*17, 450, 32, 32),
            new Rectangle(32*18, 450, 32, 32),
            new Rectangle(32*19, 450, 32, 32),
            new Rectangle(32*20, 450, 32, 32),
            new Rectangle(32*21, 450, 32, 32),
            new Rectangle(32*22, 450, 32, 32),
            new Rectangle(32*23, 450, 32, 32),
            new Rectangle(32*24, 450, 32, 32),
            
            new Rectangle(50, 400, 32, 32),
            new Rectangle(82, 400, 32, 32),
            new Rectangle(200, 360, 32, 32),
            new Rectangle(300, 360, 32, 32),
            new Rectangle(400, 360, 32, 32),
            new Rectangle(500, 360, 32, 32),
            new Rectangle(600, 360, 32, 32),
            
            new Rectangle(32*22, 300, 32, 32),
            new Rectangle(32*23, 300, 32, 32),
        ];
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState keyboard = Keyboard.GetState();

        Vector2 previousPosition = playerPosition;

        if (keyboard.IsKeyDown(Keys.A))
            playerPosition.X -= moveSpeed * deltaTime;

        if (keyboard.IsKeyDown(Keys.D))
            playerPosition.X += moveSpeed * deltaTime;

        if ((keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Space)) && isOnGround)
        {
            playerVelocity.Y = -250f;
            isOnGround = false;
        }

        playerVelocity.Y += gravity * deltaTime;
        playerPosition.Y += playerVelocity.Y * deltaTime;

        Rectangle playerRect = new Rectangle(
            (int)playerPosition.X,
            (int)playerPosition.Y,
            playerSize,
            playerSize
        );

        isOnGround = false;

        foreach (var platform in platforms)
        {
            if (playerRect.Intersects(platform))
            {
                Rectangle prevRect = new Rectangle(
                    (int)previousPosition.X,
                    (int)previousPosition.Y,
                    playerSize,
                    playerSize
                );
                
                if (prevRect.Bottom <= platform.Top && playerRect.Bottom >= platform.Top)
                {
                    playerPosition.Y = platform.Top - playerSize;
                    playerVelocity.Y = 0;
                    isOnGround = true;
                }

                else if (prevRect.Top >= platform.Bottom && playerRect.Top <= platform.Bottom)
                {
                    playerPosition.Y = platform.Bottom;
                    playerVelocity.Y = 0;
                }

                else if (prevRect.Right <= platform.Left && playerRect.Right >= platform.Left)
                {
                    playerPosition.X = platform.Left - playerSize;
                }

                else if (prevRect.Left >= platform.Right && playerRect.Left <= platform.Right)
                {
                    playerPosition.X = platform.Right;
                }

                playerRect = new Rectangle(
                    (int)playerPosition.X,
                    (int)playerPosition.Y,
                    playerSize,
                    playerSize
                );
            }
        }

        int windowWidth = GraphicsDevice.Viewport.Width;
        int windowHeight = GraphicsDevice.Viewport.Height;

        if (playerPosition.X < 0)
            playerPosition.X = 0;

        if (playerPosition.X + playerSize > windowWidth)
            playerPosition.X = windowWidth - playerSize;

        if (playerPosition.Y + playerSize >= windowHeight)
        {
            playerPosition.Y = windowHeight - playerSize;
            playerVelocity.Y = 0;
            isOnGround = true;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        _spriteBatch.Draw(
            spriteSheet,
            new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerSize, playerSize),
            sourceRect,
            Color.White
        );

        Rectangle platformMiddleTop = new Rectangle(8, 8, 8, 8);

        foreach (var platform in platforms)
        {
            _spriteBatch.Draw(spriteSheet, platform, platformMiddleTop, Color.White);
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
