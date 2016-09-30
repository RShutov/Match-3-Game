using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.Entities
{
    static class GameConstants
    {
        public const int GEM_FALLING_SPEED = 5;
        public const int ROWS_COUNT = 8;
        public const int COLS_COUNT = 8;
        public const int SCORES_STEP = 10;
        public const int DESTROYER_SPEED = 7;
        public const bool INFINIT_TIME = false;
        public const double BOMB_DELAY = 2.5;
        public static bool CUSTOM_LEVEL = false;
        public static int POSITION_DELTA = 5;
        public static int GEM_FADEOUT_DURATION = 1000;
        public static int GAMETIME_DURATION = 60;
    }
}
