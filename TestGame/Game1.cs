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
            new Rectangle(250, 450, 32, 32),
            new Rectangle(350, 430, 32, 32),
            new Rectangle(450, 450, 32, 32),

            new Rectangle(250, 200, 32, 32),
            new Rectangle(350, 200, 32, 32),
            new Rectangle(450, 200, 32, 32)
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
