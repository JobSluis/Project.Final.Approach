using System;
using System.Drawing;
using GXPEngine.Core;
using GXPEngine.Custom.Collisions;
using TiledMapParser;

namespace GXPEngine.Custom.Level_components
{
    public class Stage : GameObject
    {
        private readonly Map stageData;
        public readonly int tileWidth;
        public readonly int tileHeight;
        public readonly int stageWidth;
        public readonly int stageHeight;

        public Stages stage { get;}


        
        // public Pivot climbableSurfaces;
        // public Pivot grappleSurfaces;
        // public Pivot surfaces;

        private MyGame myGame;

        public SpriteBatch spriteBatch;
        public Pivot backgroundSprites;

        public EasyDraw background;
        public Stage(Stages givenStage)
        {
            myGame = (MyGame) game;
            parent = myGame;
            
            
            stage = givenStage;
            string stagePath = "Debug/Tiled/" + stage + ".tmx";
            stageData = MapParser.ReadMap(stagePath);
            
            backgroundSprites = new Pivot();
            
            
            tileWidth = stageData.TileWidth;
            tileHeight = stageData.TileHeight;
            stageWidth = stageData.Width * tileWidth;
            stageHeight = stageData.Height * tileHeight;

            background = new EasyDraw(stageWidth, stageHeight, false);
            background.Clear(Color.LightCyan);
            AddChildAt(background,0);

            

            if (stageData.Layers is not {Length: > 0})
            {
                throw new Exception("Tile file " + stagePath + " does not contain a layer!");
            }

            LoadStage();

        }
        
        /// <summary>
        /// Loads in the stage from tiled
        /// </summary>
        private void LoadStage()
        {
            Layer mainLayer = stageData.Layers[0];
            MyGame myGame = (MyGame)game;
            short [,] tileNumbers = mainLayer.GetTileArray();

            for (int col = 0; col < mainLayer.Width; col++)
            for (int row = 0; row < mainLayer.Height; row++)
            {
                int pX = col * tileWidth;
                int pY = row * tileHeight;

                switch (tileNumbers[col, row])
                {
                    case 1:
                        Block block = new Block(new Vector2(pX, pY));
                        myGame.blocks.Add(block);
                        AddChild(block);
                        break;
                    
                    case 2:
                        Spike spike = new Spike(new Vector2(pX, pY));
                        myGame.blocks.Add(spike);
                        AddChild(spike);
                        break;
                    
                    case 3:
                        
                        break;
                    
                    case 4:
                        
                        break;
                    
                    case 5:
                        
                        break;
                    
                    case 6:
                        
                        break;
                    
                    case 7:
                        
                        break;
                    
                    case 8:
                        
                        break;
                }
            }
            
            AddChildAt(spriteBatch, 1);
            spriteBatch.Freeze();
            
            AddChildAt(backgroundSprites,2);


            foreach (ObjectGroup objectGroup in stageData.ObjectGroups)
            {
                if (objectGroup.Objects == null) continue;
                
                foreach (TiledObject tiledObject in objectGroup.Objects)
                {
                    

                    // void SetDimensionsCorrect(Hitbox pHitbox)
                    // {
                    //     pHitbox.SetXY(tiledObject.X,tiledObject.Y);
                    //     pHitbox.width = (int) tiledObject.Width;
                    //     pHitbox.height = (int) tiledObject.Height;
                    // }
                    
                    switch (objectGroup.Name)
                    {
                        case "normal":
                            // hitbox = new Hitbox();
                            // SetDimensionsCorrect(hitbox);
                            // surfaces.AddChild(hitbox);
                            break;
                    
                        case "climbable":
                            // hitbox = new Hitbox(climbable_: true);
                            // SetDimensionsCorrect(hitbox);
                            // climbableSurfaces.AddChild(hitbox);
                            break;
                    
                        case "grapple":
                            // hitbox = new Hitbox(canGrappleOnto_: true);
                            // SetDimensionsCorrect(hitbox);
                            // grappleSurfaces.AddChild(hitbox);
                            break;
                    }
                }
            }
            
            // AddChild(grappleSurfaces);
            // AddChild(climbableSurfaces);
            // AddChild(surfaces);
        }
    }
}