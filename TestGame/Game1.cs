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
    float groundY = 400f;
    float moveSpeed = 100f;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteSheet = Content.Load<Texture2D>("spritesheet");
        sourceRect = new Rectangle(0, 0, 8, 8);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState keyboard = Keyboard.GetState();
        
        if (keyboard.IsKeyDown(Keys.A))
        {
            playerPosition.X -= moveSpeed * deltaTime;
        }
        if (keyboard.IsKeyDown(Keys.D))
        {
            playerPosition.X += moveSpeed * deltaTime;
        }
        
        playerVelocity.Y += gravity * deltaTime;
        playerPosition += playerVelocity * deltaTime;
        
        int windowWidth = GraphicsDevice.Viewport.Width;
        int windowHeight = GraphicsDevice.Viewport.Height;
        
        if (playerPosition.Y + sourceRect.Height >= windowHeight)
        {
            playerPosition.Y = windowHeight - sourceRect.Height;
            playerVelocity.Y = 0;
        }
        
        if (playerPosition.X < 0)
            playerPosition.X = 0;

        if (playerPosition.X + sourceRect.Width > windowWidth)
            playerPosition.X = windowWidth - sourceRect.Width;
        
        if (playerPosition.Y + sourceRect.Height >= windowHeight)
        {
            playerPosition.Y = windowHeight - sourceRect.Height;
            playerVelocity.Y = 0;
        }


        
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(
            spriteSheet,       
            playerPosition,     
            sourceRect, 
            Color.White         
        );
        
        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
