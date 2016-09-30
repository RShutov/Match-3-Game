using Match3.Animations;
using Match3.Components;
using Match3.Core;
using Match3.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Match3.Systems{
    class GameManager : ISystem
    {
        private Rectangle viewport;
        private Engine engine;
        private EntityCreator creator;
        private Entity focusedGem;
        private Entity swappedGem1;
        private Entity swappedGem2;
        private Entity[,] gems;
        private GemComponent.TYPE[,] previousState;

        private List<Entity> effectStack;
        private List<Entity> fallingStack;
        private const int SCORE_FONT_SIZE = 65;

        private int GEM_SIDE;
        private int GAME_FIELD_POS_X;
        private int GAME_FIELD_POS_Y;


        public GameManager(Rectangle viewport, Engine e, EntityCreator creator){
            effectStack = new List<Entity>();
            this.viewport = viewport;
            engine = e;
            this.creator = creator;
            gems = new Entity[GameConstants.ROWS_COUNT, GameConstants.COLS_COUNT];
        }

        private int[] findEmpty(){
            int[] emtpies = new int[GameConstants.COLS_COUNT];
            for (int i = 0; i < GameConstants.ROWS_COUNT; i++)
                for (int j = 0; j < GameConstants.COLS_COUNT; j++){
                    if (gems[i, j] == null){
                        emtpies[j]++;
                    }
                }
            return emtpies;
        }

        public GemComponent.TYPE[,] calcGemMask(){
            GemComponent.TYPE[,] mask = new GemComponent.TYPE[GameConstants.ROWS_COUNT, GameConstants.COLS_COUNT];
            for (int i = 0; i < GameConstants.ROWS_COUNT; i++)
                for (int j = 0; j < GameConstants.COLS_COUNT; j++){
                    mask[i, j] = gems[i, j] == null ? GemComponent.TYPE.NONE : gems[i, j].getComponent<GemComponent>().type;
                }
            return mask;
        }

        public List<Tuple<Entity, int, Point>> gemsForShift(){
            List<Tuple<Entity, int, Point>> lst = new List<Tuple<Entity, int, Point>>();
            for (int i = 0; i < GameConstants.ROWS_COUNT; i++){
                for (int j = 0; j < GameConstants.COLS_COUNT; j++){
                    if (gems[i, j] != null){
                        int p = 0;
                        for (int k = i + 1; k < GameConstants.ROWS_COUNT; k++)
                            if (gems[k, j] == null)
                                p++;
                        if (p != 0)
                            lst.Add(new Tuple<Entity, int, Point>(gems[i, j], p, new Point(i, j)));
                    }
                }
            }
            return lst;
        }

        public void onGemCome(Entity e){
            fallingStack.Remove(e);
            if (fallingStack.Count == 0){
                destroyGems();
            }
        }

        public void onDestroyGems(){
            Random rnd = new Random();
            fallingStack = new List<Entity>();
            int shift = GEM_SIDE;
            int[] emptyCount = findEmpty();

            List<Tuple<Entity, int, Point>> shiftOrder = gemsForShift();
            foreach (Tuple<Entity, int, Point> t in shiftOrder){
                Entity g = t.Item1;
                PositionComponent position = g.getComponent<PositionComponent>();
                GameFieldPosition gameFieldPos = g.getComponent<GameFieldPosition>();
                PositionComponent target = new PositionComponent(position.x, position.y + GEM_SIDE * t.Item2);
                MotionComponent m1 = new MotionComponent(g, 0, GameConstants.GEM_FALLING_SPEED, 0, target);
                g.addComponent<MotionComponent>(m1);
                m1.onEndListeners += onGemCome;
                gameFieldPos.x = t.Item3.X + t.Item2;
                gameFieldPos.y = t.Item3.Y;
                gems[t.Item3.X + t.Item2, t.Item3.Y] = g;
                fallingStack.Add(g);
            }
            for (int i = 0; i < GameConstants.COLS_COUNT; i++){
                for (int j = 0; j < emptyCount[i]; j++){
                    int number = rnd.Next(1, Enum.GetValues(typeof(GemComponent.TYPE)).Length);
                    Entity entity = creator.createGem(new Point(GAME_FIELD_POS_X + i * GEM_SIDE, GAME_FIELD_POS_Y - (emptyCount[i] - j + 1) * GEM_SIDE), GEM_SIDE, GEM_SIDE, (GemComponent.TYPE)number, j, i);
                    entity.getComponent<MouseInteractionComponent>().onCLickListeners += onGemClicked;
                    MotionComponent m = new MotionComponent(entity, 0, GameConstants.GEM_FALLING_SPEED, 0, new PositionComponent(GAME_FIELD_POS_X + i * GEM_SIDE, GAME_FIELD_POS_Y + j * GEM_SIDE));
                    entity.addComponent<MotionComponent>(m);
                    m.onEndListeners += onGemCome;
                    gems[j, i] = entity;
                    fallingStack.Add(entity);
                }
            }
        }

        public void activateBomb(Entity e){
            e.getComponent<StatesComponent>().changeState("destroyed");
            var delay = new Delay(250, e);
            delay.onEndListeners += onBombExplose;
            e.addComponent<IDelay>(delay);
            effectStack.Add(e);
        }


        public void activateLine(Entity e){
            engine.removeEntity(e);
            updateScores(GameConstants.SCORES_STEP);
            var pos = e.getComponent<PositionComponent>();
            var t = e.getComponent<BonusLine>().orientation;
            Entity des1;
            Entity des2;
            MotionComponent m1;
            MotionComponent m2;
            if (t == BonusLine.Orientation.VERTICAL){
                des1 = creator.createDestroyer(
                    new Point(pos.x, pos.y + GEM_SIDE / 2),
                    GEM_SIDE,
                    GEM_SIDE,
                    Destroyer.Direction.BOTTOM
                    );
                des2 = creator.createDestroyer(
                    new Point(pos.x, pos.y - GEM_SIDE / 2),
                    GEM_SIDE,
                    GEM_SIDE,
                    Destroyer.Direction.TOP
                    );
                m1 = new MotionComponent(
                    des1,
                    0,
                    GameConstants.DESTROYER_SPEED,
                    0,
                    new PositionComponent(pos.x, GAME_FIELD_POS_Y + GameConstants.ROWS_COUNT * GEM_SIDE)
                );
                m2 = new MotionComponent(
                    des2,
                    0,
                    -GameConstants.DESTROYER_SPEED,
                    0,
                    new PositionComponent(pos.x, GAME_FIELD_POS_Y)
                );
            }else{
                des1 = creator.createDestroyer(
                    new Point(pos.x + GEM_SIDE / 2, pos.y),
                    GEM_SIDE,
                    GEM_SIDE,
                    Destroyer.Direction.RIGHT
                    );
                des2 = creator.createDestroyer(
                    new Point(pos.x - GEM_SIDE / 2, pos.y),
                    GEM_SIDE,
                    GEM_SIDE,
                    Destroyer.Direction.LEFT
                    );
                m1 = new MotionComponent(
                     des1,
                     GameConstants.DESTROYER_SPEED,
                     0,
                     0,
                     new PositionComponent(GAME_FIELD_POS_X + GameConstants.COLS_COUNT * GEM_SIDE, pos.y)
                 );
                m2 = new MotionComponent(
                    des2,
                    -GameConstants.DESTROYER_SPEED,
                    0,
                    0,
                    new PositionComponent(GAME_FIELD_POS_X - GEM_SIDE, pos.y)
                );
            }
            des1.addComponent<MotionComponent>(m1);
            des2.addComponent<MotionComponent>(m2);
            m1.onEndListeners += onEffectEnd;
            m2.onEndListeners += onEffectEnd;
            effectStack.Add(des1);
            effectStack.Add(des2);
        }

        public void onEffectEnd(Entity e){
            effectStack.Remove(e);
            engine.removeEntity(e);

            if (effectStack.Count == 0){
                onDestroyGems();
            }
        }

        public void updateScores(int value){
            var scoreNodes = engine.getNode(ScoreNode.components);
            Score scores = (Score)scoreNodes.First()[typeof(Score)];
            scores.score += value;
        }

        public void destroyGems(){
            GemComponent.TYPE[,] gemMask = calcGemMask();
            var finder = new MatchFinder<GemComponent.TYPE>(gemMask, previousState, GameConstants.ROWS_COUNT, GameConstants.COLS_COUNT, 3, GemComponent.TYPE.NONE);
            var founded = finder.find();
            previousState = gemMask;
            updateScores(GameConstants.SCORES_STEP * founded.Count);
            if (founded.Count == 0){
                if (swappedGem1 == null){
                    engine.ImputIsLocked = false;
                    return;
                }else{
                    PositionComponent pos1 = swappedGem1.getComponent<PositionComponent>();
                    PositionComponent pos2 = swappedGem2.getComponent<PositionComponent>();
                    PositionComponent target1 = new PositionComponent(pos2.x, pos2.y);
                    PositionComponent target2 = new PositionComponent(pos1.x, pos1.y);

                    GameFieldPosition gpos1 = swappedGem2.getComponent<GameFieldPosition>();
                    GameFieldPosition gpos2 = swappedGem1.getComponent<GameFieldPosition>();

                    Point buf = new Point(gpos2.x, gpos2.y);
                    gems[gpos1.x, gpos1.y] = swappedGem1;
                    gems[gpos2.x, gpos2.y] = swappedGem2;
                    gpos2.x = gpos1.x;
                    gpos2.y = gpos1.y;
                    gpos1.x = buf.X;
                    gpos1.y = buf.Y;

                    PositionComponent center = new PositionComponent((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2);
                    MotionComponent m1 = new MotionComponent(swappedGem1, 0, 0, GameConstants.GEM_FALLING_SPEED, MotionComponent.RotationDirection.COUNTERCLOCK_WISE, center, target1);
                    m1.onEndListeners += onSwapFail;
                    MotionComponent m2 = new MotionComponent(swappedGem2, 0, 0, GameConstants.GEM_FALLING_SPEED, MotionComponent.RotationDirection.COUNTERCLOCK_WISE, center, target2);
                    m2.onEndListeners += onSwapFail;
                    swappedGem1.addComponent(typeof(MotionComponent), m1);
                    swappedGem2.addComponent(typeof(MotionComponent), m2);
                    swappedGem1 = null;
                    swappedGem2 = null;
                    return;
                }
            }else{
                swappedGem1 = null;
                swappedGem2 = null;
            }

            foreach (var p in founded){
                Entity elem = gems[p.Item1.X, p.Item1.Y];
                var pos = elem.getComponent<PositionComponent>();

                if (gems[p.Item1.X, p.Item1.Y].haveComponent(typeof(BonusBomb))){
                    activateBomb(gems[p.Item1.X, p.Item1.Y]);
                }
                else if (gems[p.Item1.X, p.Item1.Y].haveComponent(typeof(BonusLine))){
                    activateLine(gems[p.Item1.X, p.Item1.Y]);
                }
                else if (gems[p.Item1.X, p.Item1.Y].haveComponent(typeof(Gem))){
                    destroyGem(gems[p.Item1.X, p.Item1.Y]);
                }

                gems[p.Item1.X, p.Item1.Y] = null;
                if (p.Item2 != Finder.ComboType.SIMPLE){
                    Entity e = new Entity();
                    var t = elem.getComponent<GemComponent>().type;
                    if (p.Item2 == Finder.ComboType.LINE){
                        e = creator.createLineBonus(new Point(pos.x, pos.y), GEM_SIDE, GEM_SIDE, t, p.Item1.X, p.Item1.Y);
                    }
                    else if (p.Item2 == Finder.ComboType.BOMB){
                        e = creator.createBombBonus(new Point(pos.x, pos.y), GEM_SIDE, GEM_SIDE, t, p.Item1.X, p.Item1.Y);
                    }
                    e.getComponent<MouseInteractionComponent>().onCLickListeners += onGemClicked;
                    gems[p.Item1.X, p.Item1.Y] = e;
                }
            }
        }

        public void destroyGem(Entity e){
            updateScores(GameConstants.SCORES_STEP);
            e.getComponent<StatesComponent>().changeState("destroyed");
            effectStack.Add(e);
            var d = new Delay(GameConstants.GEM_FADEOUT_DURATION, e);
            d.onEndListeners += onEffectEnd;
            e.addComponent<IDelay>(d);
        }

        public void onBombExplose(Entity e){
            var bombNode = e.getComponent<BonusBomb>();
            var stateComponent = e.getComponent<StatesComponent>();
            var position = e.getComponent<GameFieldPosition>();

            gems[position.x, position.y] = null;
            int posMinX = (position.x - 1) < 0 ? 0 : (position.x - 1);
            int posMinY = (position.y - 1) < 0 ? 0 : (position.y - 1);
            int posMaxX = (position.x + 1) >= GameConstants.ROWS_COUNT ? 0 : (position.x + 1);
            int posMaxY = (position.y - 1) >= GameConstants.COLS_COUNT ? 0 : (position.y + 1);

            for (int i = posMinX; i <= posMaxX; i++){
                for (int j = posMinY; j <= posMaxY; j++){
                    if (gems[i, j] != null){
                        Entity entity = gems[i, j];
                        if (entity.haveComponent(typeof(Gem))){
                            destroyGem(entity);
                            gems[i, j] = null;
                        }
                        else if (entity.haveComponent(typeof(BonusBomb))){
                            if (entity.getComponent<StatesComponent>().getState().name == "alive"){
                                activateBomb(entity);
                            }
                        }
                        else if (entity.haveComponent(typeof(BonusLine))){
                            activateLine(entity);
                            gems[i, j] = null;
                        }
                    }
                }
            }
            updateScores(GameConstants.SCORES_STEP);
            onEffectEnd(e);
        }

        public void loadLevel(Tuple<GemComponent.TYPE[,], TestField.TYPE[,]> data){
            for (int i = 0; i < GameConstants.ROWS_COUNT; i++)
                for (int j = 0; j < GameConstants.COLS_COUNT; j++){
                    Entity e = new Entity();
                    switch (data.Item2[i, j]){
                        case TestField.TYPE.PRIMAL:
                            e = creator.createGem(new Point(GAME_FIELD_POS_X + j * GEM_SIDE, GAME_FIELD_POS_Y + i * GEM_SIDE), GEM_SIDE, GEM_SIDE, data.Item1[i, j], i, j);
                            break;
                        case TestField.TYPE.BOMB:
                            e = creator.createBombBonus(new Point(GAME_FIELD_POS_X + j * GEM_SIDE, GAME_FIELD_POS_Y + i * GEM_SIDE), GEM_SIDE, GEM_SIDE, data.Item1[i, j], i, j);
                            break;
                        case TestField.TYPE.LINE:
                            e = creator.createLineBonus(new Point(GAME_FIELD_POS_X + j * GEM_SIDE, GAME_FIELD_POS_Y + i * GEM_SIDE), GEM_SIDE, GEM_SIDE, data.Item1[i, j], i, j);
                            break;
                    }
                    e.getComponent<MouseInteractionComponent>().onCLickListeners += onGemClicked;
                    gems[i, j] = e;
                }
        }

        public void onSwapFail(Entity e){
            engine.ImputIsLocked = false;
        }

        public void onGemClicked(Entity e){
            if (focusedGem == null)
                focusedGem = e;
            else if (focusedGem == e){
                focusedGem = null;
            }else{
                e.getComponent<StatesComponent>().changeState("alive");
                focusedGem.getComponent<StatesComponent>().changeState("alive");
                GameFieldPosition gameFieldPos1 = e.getComponent<GameFieldPosition>();
                GameFieldPosition gameFieldPos2 = focusedGem.getComponent<GameFieldPosition>();
                if (!(gameFieldPos1.x == gameFieldPos2.x && Math.Abs(gameFieldPos1.y - gameFieldPos2.y) == 1) &&
                    !(gameFieldPos1.y == gameFieldPos2.y && Math.Abs(gameFieldPos1.x - gameFieldPos2.x) == 1))
                {
                    focusedGem = null;
                    return;
                }
                PositionComponent pos1 = e.getComponent<PositionComponent>();
                PositionComponent pos2 = focusedGem.getComponent<PositionComponent>();
                PositionComponent center = new PositionComponent((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2);
                PositionComponent target1 = new PositionComponent(pos2.x, pos2.y);
                PositionComponent target2 = new PositionComponent(pos1.x, pos1.y);
                MotionComponent m1 = new MotionComponent(e, 0, 0, GameConstants.GEM_FALLING_SPEED, MotionComponent.RotationDirection.CLOCKWISE, center, target1);
                m1.onEndListeners += onGemSwapped;
                MotionComponent m2 = new MotionComponent(focusedGem, 0, 0, GameConstants.GEM_FALLING_SPEED, MotionComponent.RotationDirection.CLOCKWISE, center, target2);
                m2.onEndListeners += onGemSwapped;
                e.addComponent(typeof(MotionComponent), m1);
                focusedGem.addComponent(typeof(MotionComponent), m2);
                focusedGem = null;
                engine.ImputIsLocked = true;
            }
        }

        public void onGemSwapped(Entity e){
            if (swappedGem1 == null)
                swappedGem1 = e;
            else{
                previousState = calcGemMask();
                swappedGem2 = e;
                GameFieldPosition pos = swappedGem2.getComponent<GameFieldPosition>();
                GameFieldPosition pos2 = swappedGem1.getComponent<GameFieldPosition>();
                gems[pos.x, pos.y] = swappedGem1;
                gems[pos2.x, pos2.y] = swappedGem2;
                Point buf = new Point(pos.x, pos.y);
                pos.x = pos2.x;
                pos.y = pos2.y;
                pos2.x = buf.X;
                pos2.y = buf.Y;
                destroyGems();
            }

        }
        public void startGame(Entity e = null){
            engine.removeAll();
            creator.createBackground("background", viewport.Width, viewport.Height);
            if (GameConstants.CUSTOM_LEVEL){
                loadLevel(TestField.LineTestField);
            }else{
                Random rnd = new Random();
                for (int i = 0; i < GameConstants.ROWS_COUNT; i++)
                    for (int j = 0; j < GameConstants.COLS_COUNT; j++){
                        int number = rnd.Next(1, Enum.GetValues(typeof(GemComponent.TYPE)).Length);
                        Entity entity = creator.createGem(new Point(GAME_FIELD_POS_X + j * GEM_SIDE, GAME_FIELD_POS_Y + i * GEM_SIDE), GEM_SIDE, GEM_SIDE, (GemComponent.TYPE)number, i, j);
                        entity.getComponent<MouseInteractionComponent>().onCLickListeners += onGemClicked;
                        gems[i, j] = entity;
                    }
            }
            creator.createScore(new Point(viewport.Width / 10, viewport.Height / 4));
            if (!GameConstants.INFINIT_TIME){
                Entity timer = creator.createTimer(new Point(viewport.Width / 10,  viewport.Height / 2));
                timer.getComponent<Timer>().onTimeEnd += onTimeOut;
            }
            previousState = calcGemMask();
            destroyGems();
        }

        public void init(){
            GAME_FIELD_POS_Y = 0;
            GEM_SIDE = viewport.Height / 8;
            GAME_FIELD_POS_X = (viewport.Width - viewport.Height);
            creator.createBackground("background", viewport.Width, viewport.Height);
            showMainMenu();
        }

        public void clear(){
            engine.removeAll();
            swappedGem1 = null;
            swappedGem2 = null;
            fallingStack.Clear();
            effectStack.Clear();
        }
        public void onTimeOut(){
            clear();
            engine.ImputIsLocked = false;
            creator.createBackground("background", viewport.Width, viewport.Height);
            creator.createGameOverTitle(new Point(viewport.Width / 2 , viewport.Height  / 4)
           );
            Entity okButton = creator.createOkButton( new Point(viewport.Width / 2, viewport.Height / 2));
            okButton.getComponent<MouseInteractionComponent>().onCLickListeners += showMainMenu;
        }

        public void showMainMenu(Entity e = null){
            engine.removeAll();
            creator.createBackground("background", viewport.Width, viewport.Height);
            Entity mainMenu = creator.createMainMenu(new Point(viewport.Width / 2, viewport.Height / 2));
            mainMenu.getComponent<MouseInteractionComponent>().onCLickListeners += startGame;

        }
        public void update(GameTime time){

            foreach (var d in engine.getNode(DelayNode.components)){
                ((IDelay)d[typeof(IDelay)]).update(time);
            }

            foreach (var d in engine.getNode(DestroyerNode.components)){
                var pos = (PositionComponent)d[typeof(PositionComponent)];
                Point p = getCoord(pos);
                if (p.X != -1 && p.Y != -1){
                    if (gems[p.X, p.Y] != null){
                        if (gems[p.X, p.Y].haveComponent(typeof(BonusBomb))){
                            activateBomb(gems[p.X, p.Y]);
                        }
                        else if (gems[p.X, p.Y].haveComponent(typeof(BonusLine))){
                            activateLine(gems[p.X, p.Y]);
                        }
                        else if (gems[p.X, p.Y].haveComponent(typeof(Gem))){
                            destroyGem(gems[p.X, p.Y]);
                        }
                        gems[p.X, p.Y] = null;
                    }
                }
            }
        }

        private Point getCoord(PositionComponent pos){
            int i = -1;
            int j = -1;
            var p = new Point(pos.x, pos.y);
            if (new Rectangle(GAME_FIELD_POS_X, GAME_FIELD_POS_Y, GEM_SIDE * GameConstants.COLS_COUNT, GEM_SIDE * GameConstants.ROWS_COUNT).Contains(p)){
                j = (pos.x - GAME_FIELD_POS_X) / GEM_SIDE;
                i = (pos.y - GAME_FIELD_POS_Y) / GEM_SIDE;
            }
            return new Point(i, j);
        }
    }
}
