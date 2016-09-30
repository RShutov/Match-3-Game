using Match3.Animations;
using Match3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Match3.Components
{
    class StatesComponent
    {
        EntityStateManager stateManager;

        public StatesComponent(EntityStateManager stateManager){
            this.stateManager = stateManager;
        }

        public EntityState getState(){
            return stateManager.getCurrState();
        }
        public void changeState(string stateName){
            stateManager.changeState(stateName);
        }
    }
    class Gem { }

    class BonusBomb { }

    class Destroyer {
        public enum Direction { TOP, BOTTOM, LEFT, RIGHT }
        public Direction direction;
        public Destroyer(Direction direction){
            this.direction = direction;
        }
    }
    class BonusLine {
        public Orientation orientation;
        public enum Orientation { VERTICAL, HORIZONTAL };
        public BonusLine(Orientation orientation){
            this.orientation = orientation;
        }
    }
    class Score{
        private int _score;
        public int score{
            get{
                return _score;
            }
            set{
                _score = value;
                onChange();
            }
        }
        public Score(){
            _score = 0;
        }
        public delegate  void OnScoreChanged(string text);
        public event OnScoreChanged onScoreChanged;
        private void onChange(){
            onScoreChanged(_score.ToString());
        }
    }

    class Timer : IDelay{
        public delegate void OnUpdate(string text);
        public event OnUpdate onUpdate;
        private int _time;
        private TimeSpan startTime;

        public delegate void OnTimeEnd();
        public event OnTimeEnd onTimeEnd;

        private readonly int TIME_LIMIT;

        public int time{
            get{
                return _time;
            }
 
        }
        public Timer(int timeLimit){
            TIME_LIMIT = timeLimit;
        }


        private void onChange(){
            onUpdate(time.ToString());
        }

        public void update(GameTime gameTime){
            if (startTime == TimeSpan.Zero)
                startTime = gameTime.TotalGameTime;
            if ((gameTime.TotalGameTime - startTime).TotalSeconds >= TIME_LIMIT)
                end();
            _time = TIME_LIMIT - (int)(gameTime.TotalGameTime - startTime).TotalSeconds;
            onChange();
        }

        public void end(){
            onTimeEnd();
        }
    }

}
