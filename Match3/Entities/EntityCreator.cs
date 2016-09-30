using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Match3.Components;
using Microsoft.Xna.Framework.Graphics;
using Match3.views;
using Match3.Views;
using Match3.Core;
using Match3.Animations;

namespace Match3.Entities
{
    class EntityCreator
    {
        public Engine engine;

        public EntityCreator(Engine e){
            engine = e;
        }
        public Entity createDelay(int duration){
            Entity e = new Entity();
            Delay delay = new Delay(duration, e);
            e.addComponent<IDelay>(delay);
            e.addComponent<Delay>(delay);
            engine.addEntity(e);
            return e;
        }

        public Entity createDestroyer(Point pos, int width, int height, Destroyer.Direction direction){
            Entity e = new Entity();
            e.addComponent<View>(new DestroyerView(width, height, direction));
            e.addComponent<Destroyer>(new Destroyer(direction));
            e.addComponent<PositionComponent>(new PositionComponent(pos.X, pos.Y));
            engine.addEntity(e);
            return e;
        }

        public Entity createGem(Point pos, int width, int height, GemComponent.TYPE gemType, int ipos, int jpos){
            Entity e = new Entity();
            
            EntityStateManager sm = new EntityStateManager(e);
            StatesComponent stateComponent = new StatesComponent(sm);
            MouseInteractionComponent mic = new MouseInteractionComponent(width, height, e);
            mic.onCLickListeners += (s) => {
                
                var gem = s.getComponent<StatesComponent>();
                if (gem.getState().name == "alive"){
                    gem.changeState("focused");
                }else if (gem.getState().name == "focused"){
                    gem.changeState("alive");
                } 
            };
            sm.createState("alive")
                .add<View>(new GemView(gemType, width, height));
            sm.createState("focused")
                .add<View>(new GemFocusedView(gemType, width, height));
            GemDestoroyedView destroyedView = new GemDestoroyedView(gemType, width, height, 1);
            sm.createState("destroyed")
                .add<View>(destroyedView)
                .add<IAnimation>(destroyedView);
            e.addComponent<GameFieldPosition>(new GameFieldPosition(ipos, jpos));
            e.addComponent<Gem>(new Gem());
            e.addComponent<StatesComponent>(stateComponent);
            e.addComponent<GemComponent>(new GemComponent(gemType));
            e.addComponent<PositionComponent>( new PositionComponent(pos.X, pos.Y));
            e.addComponent<MouseInteractionComponent>(mic);
            sm.changeState("alive");
            engine.addEntity(e);
            return e;
        }

        public Entity createBombBonus(Point pos, int width, int height, GemComponent.TYPE gemType, int ipos, int jpos){
            Entity e = new Entity();

            EntityStateManager sm = new EntityStateManager(e);
            StatesComponent stateComponent = new StatesComponent(sm);
            MouseInteractionComponent mic = new MouseInteractionComponent(width, height, e);
            mic.onCLickListeners += (s) => {

                var gem = s.getComponent<StatesComponent>();
                if (gem.getState().name == "alive")
                {
                    gem.changeState("focused");
                }
                else if (gem.getState().name == "focused")
                {
                    gem.changeState("alive");
                }
            };
            sm.createState("alive")
                .add<View>(new BonusBombView(gemType, width, height));
            sm.createState("focused")
                .add<View>(new BonusBombViewFocused(gemType, width, height));
            BonusBombDestroyedView destroyedView = new BonusBombDestroyedView(gemType, width, height, GameConstants.BOMB_DELAY);
            sm.createState("destroyed")
                .add<View>(destroyedView)
                .add<IAnimation>(destroyedView);
            e.addComponent<GameFieldPosition>(new GameFieldPosition(ipos, jpos));
            e.addComponent<BonusBomb>(new BonusBomb());
            e.addComponent<StatesComponent>(stateComponent);
            e.addComponent<GemComponent>(new GemComponent(gemType));
            e.addComponent<PositionComponent>(new PositionComponent(pos.X, pos.Y));
            e.addComponent<MouseInteractionComponent>(mic);
            sm.changeState("alive");
            engine.addEntity(e);
            return e;
        }

        public Entity createGameOverTitle(Point pos){
            Entity e = new Entity();
            string text = "GAME OVER";
            e.addComponent<View>(new TextView(text, "MainFont"));
            Vector2 v = ResourceManager.Instance.getResource<SpriteFont>("MainFont").MeasureString(text);
            int width = (int)v.X;
            int height = (int)v.Y;
            e.addComponent<PositionComponent>(new PositionComponent(pos.X - width / 2, pos.Y - height / 2));
            engine.addEntity(e);
            return e;
        }

        public Entity createOkButton(Point pos){
            Entity e = new Entity();
            string text = "Ok";
            e.addComponent<View>(new TextView(text, "OkButtonFont"));
            Vector2 v = ResourceManager.Instance.getResource<SpriteFont>("OkButtonFont").MeasureString(text);
            int width = (int)v.X;
            int height = (int)v.Y;
            e.addComponent<MouseInteractionComponent>(new MouseInteractionComponent(width, height,e));
            e.addComponent<PositionComponent>(new PositionComponent(pos.X - width / 2, pos.Y - height / 2));
            engine.addEntity(e);
            return e;
        }


        public Entity createBackground(string textureName, int width, int height){
            Entity e = new Entity();
            e.addComponent<View>(new BackgroundView(textureName, width, height));
            e.addComponent<PositionComponent>(new PositionComponent(0, 0));
            engine.addEntity(e);
            return e;
        }

        public Entity createScore(Point pos){
            Entity e = new Entity();
            Score score = new Score();
            TiteledTextView scoreView = new TiteledTextView("score", "0", "MainFont", "ScoreFont");
            score.onScoreChanged += scoreView.textChange;
            e.addComponent<Score>(score);
            e.addComponent<View>(scoreView);
            e.addComponent<PositionComponent>(new PositionComponent(pos.X, pos.Y));
            engine.addEntity(e);
            return e;
        }

        public Entity createMainMenu(Point pos){
            Entity e = new Entity();
            e.addComponent<View>(new TextView("start", "OkButtonFont"));
            Vector2 v = ResourceManager.Instance.getResource<SpriteFont>("OkButtonFont").MeasureString("start");
            int width = (int)v.X;
            int height = (int)v.Y;
            e.addComponent<MouseInteractionComponent>(new MouseInteractionComponent(width, height, e));
            e.addComponent<PositionComponent>(new PositionComponent(pos.X - width / 2, pos.Y - height / 2));
            engine.addEntity(e);
            return e;
        }

        public Entity createTimer(Point pos){
            Entity e = new Entity();
            Timer timer = new Timer(GameConstants.GAMETIME_DURATION);
            TiteledTextView textView = new TiteledTextView("time", "60", "MainFont", "ScoreFont");
            timer.onUpdate += textView.textChange;
            e.addComponent<Timer>(timer);
            e.addComponent<View>(textView);
            e.addComponent<IDelay>(timer);
            e.addComponent<PositionComponent>(new PositionComponent(pos.X, pos.Y));
            engine.addEntity(e);
            return e;
        }

        public Entity createLineBonus(Point pos, int width, int height, GemComponent.TYPE gemType, int ipos, int jpos){

            Entity e = new Entity();
            Random rnd = new Random();
            BonusLine.Orientation orientation = (BonusLine.Orientation)rnd.Next(0, 2);
            EntityStateManager sm = new EntityStateManager(e);
            StatesComponent stateComponent = new StatesComponent(sm);
            MouseInteractionComponent mic = new MouseInteractionComponent(width, height, e);
            mic.onCLickListeners += (s) => {

                var gem = s.getComponent<StatesComponent>();
                if (gem.getState().name == "alive"){
                    gem.changeState("focused");
                }
                else if (gem.getState().name == "focused"){
                    gem.changeState("alive");
                }
            };
            sm.createState("alive")
                .add<View>(new BonusLineView(gemType, width, height, orientation));
            sm.createState("focused")
                .add<View>(new BonusLineViewFocused(gemType, width, height, orientation));
            GemDestoroyedView destroyedView = new GemDestoroyedView(gemType, width, height, 1);
            sm.createState("destroyed")
                .add<View>(destroyedView)
                .add<IAnimation>(destroyedView);
            e.addComponent<GameFieldPosition>(new GameFieldPosition(ipos, jpos));
            e.addComponent<BonusLine>(new BonusLine(orientation));
            e.addComponent<GemComponent>(new GemComponent(gemType));
            e.addComponent<StatesComponent>(stateComponent);
            e.addComponent<PositionComponent>(new PositionComponent(pos.X, pos.Y));
            e.addComponent<MouseInteractionComponent>(mic);
            sm.changeState("alive");
            engine.addEntity(e);
            return e;
        }
    }



}
