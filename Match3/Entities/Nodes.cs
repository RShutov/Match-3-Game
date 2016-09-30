using Match3.Animations;
using Match3.Components;
using Match3.views;
using Match3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3.Entities
{
    public static class RenderNode
    {
        public static HashSet<Type> components = new HashSet<Type>() {
            typeof(PositionComponent),
            typeof(View)
        };
    }

    public static class GemNode
    {
        public static HashSet<Type> components = new HashSet<Type>() {
            typeof(Gem),
            typeof(PositionComponent),
            typeof(MouseInteractionComponent),
             typeof(StatesComponent),
        };
    }

    public static class MouseInteractionNode
    {
        public static HashSet<Type> components = new HashSet<Type>() {
            typeof(MouseInteractionComponent),
            typeof(PositionComponent),
        };
    }
    public static class MotionNode
    {
        public static HashSet<Type> components = new HashSet<Type>() {
            typeof(MotionComponent),
            typeof(PositionComponent),
        };
    }
    public static class AnimationNode
    {
        public static HashSet<Type> components = new HashSet<Type>() {
            typeof(IAnimation),
        };
    }
    public static class GameFieldElemNode
    {
        public static HashSet<Type> components = new HashSet<Type>() {
            typeof(GameFieldPosition),
            typeof(GemComponent),
            typeof(Gem),
            typeof(StatesComponent),
        };
    }

    public static class DelayNode
    {
        public static HashSet<Type> components = new HashSet<Type>() {
            typeof(IDelay)
        };
    }

    public static class BonusBombNode
    {
        public static HashSet<Type> components = new HashSet<Type>() {
            typeof(GameFieldPosition),
            typeof(StatesComponent),
            typeof(BonusBomb)
        };
    }

    public static class DestroyerNode
    {
        public static HashSet<Type> components = new HashSet<Type>() {
            typeof(PositionComponent),
            typeof(Destroyer)
        };
    }

    public static class ScoreNode
    {
        public static HashSet<Type> components = new HashSet<Type>() {
            typeof(Score),
        };
    }
}
