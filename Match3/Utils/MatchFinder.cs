using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Match3
{
    class Finder
    {
        public enum ComboType { LINE, BOMB, SIMPLE };
    }
    class MatchFinder<T>: Finder
    {
        HashSet<Point> used;
        private T[,] graph;
        private T[,] prev;
        private List<Tuple<Point, ComboType, T>> components;
        private int rows;
        private int cols;
        private int mult;
        private T nullObj;
        Dictionary<Point, Tuple<HashSet<Point>, HashSet<Point>>> friends;
        public MatchFinder(T[,] graph, T[,] prev, int rows, int cols, int mult, T nullObj)
        {
            used = new HashSet<Point>();
            this.rows = rows;
            this.cols = cols;
            this.graph = graph;
            this.mult = mult;
            this.nullObj = nullObj;
            this.prev = prev;
            friends = new Dictionary<Point, Tuple<HashSet<Point>, HashSet<Point>>>();
            components = new List<Tuple<Point, ComboType, T>>();
        }

        public List<Tuple<Point, ComboType>> find()
        {
           
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    if (!graph[i,j].Equals(nullObj))
                    {
                        horizontal(i, j);
                        vertical(i, j);
                    }    
                }
            
            return uniteFriends();
        }

        public Tuple<Point, ComboType> checkType(Point p, ref bool lineUsed, ref bool bombUsed)
        {
            HashSet<Point> friendLst = new HashSet<Point>();
            
            int mod = 0;
            if(friends[p].Item1.Count != 0 && friends[p].Item2.Count != 0)
            {
                if(!used.Contains(p))
                    return new Tuple<Point, ComboType>(p, ComboType.BOMB);
            }
            else{
                if (friends[p].Item1.Count == 4 && friends[p].Item2.Count == 0)
                {
                    friendLst = friends[p].Item1;
                    mod = 4;
                }
                else if (friends[p].Item2.Count == 4 && friends[p].Item1.Count == 0)
                {
                    friendLst = friends[p].Item2;
                    mod = 4;
                }
                if (friends[p].Item1.Count >= 5 && friends[p].Item2.Count == 0)
                {
                    friendLst = friends[p].Item1;
                    mod = 5;
                }
                else if (friends[p].Item2.Count >= 5 && friends[p].Item1.Count == 0)
                {
                    friendLst = friends[p].Item2;
                    mod = 5;
                }

                if (!prev[p.X, p.Y].Equals(graph[p.X, p.Y]))
                    if (mod == 4 && !lineUsed)
                    {
                        lineUsed = true;
                        return new Tuple<Point, ComboType>(p, ComboType.LINE);
                    }
                    else if (mod == 5 && !bombUsed)
                    {
                        bombUsed = true;
                        return new Tuple<Point, ComboType>(p, ComboType.BOMB);
                    }
                return new Tuple<Point, ComboType>(p, ComboType.SIMPLE);
            }
            return null;  
        }

 
        public List<Tuple<Point, ComboType>> scanFriends(int i, int j)
        {
            List<Tuple<Point, ComboType>> lst = new List<Tuple<Point, ComboType>>();
            Point curr = new Point(i, j);
            if (!used.Contains(curr) && friends.ContainsKey(curr))
            {
                    bool lineBonusUsed = false;
                    bool bombUsed = false;
                    foreach (Point p in friends[curr].Item1.Concat(friends[curr].Item2))
                    {
                    Tuple<Point, ComboType> t = checkType(p, ref lineBonusUsed, ref bombUsed);
                    if(t!= null)
                    {
                        lst.Add(t);
                        used.Add(p);
                    }           
                }
            }
            return lst;
        }

        public List<Tuple<Point, ComboType>> uniteFriends()
        {

            List<Tuple<Point, ComboType>> l = new List<Tuple<Point, ComboType>>();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    l = l.Concat(scanFriends(i, j)).ToList();
                }
            return l;
        }

        public void updateFriendList(Point p, HashSet<Point> fl, string direction)
        {
            foreach (Point friend in fl)
            {
                if (!friends.ContainsKey(p))
                    friends[p] = new Tuple<HashSet<Point>, HashSet<Point>>(new HashSet<Point>(), new HashSet<Point>());
                if (!friends.ContainsKey(friend))
                    friends[friend] = new Tuple<HashSet<Point>, HashSet<Point>>(new HashSet<Point>(), new HashSet<Point>());
                if (direction == "vertical")
                {
                    friends[p].Item2.Add(friend);
                    friends[friend].Item2.Add(p);
                }else if(direction == "horizontal")
                {
                    friends[p].Item1.Add(friend);
                    friends[friend].Item1.Add(p);
                }
            }
        }


    public void horizontal(int i, int j)
        {
            HashSet<Point> friendLst = new HashSet<Point>();
            friendLst.Add(new Point(i, j));
            int x = i + 1;
            int y = j;
            T t = graph[i, j];
            int sum = 1;
            while(x < rows)
            {
                if(graph[x,y].Equals(t))
                {
                    sum++;
                    friendLst.Add(new Point(x, y));
                    x++;
                }
                else { break; }
            }
            x = i -1;
            y = j;
            while (x >= 0)
            {
                if (graph[x, y].Equals(t))
                {
                    sum++;
                    friendLst.Add(new Point(x, y));
                    x--;
                }
                else { break; }
            }
            if (sum >= 3)
            {
                foreach(Point elem1 in friendLst)
                {
                    updateFriendList(elem1, friendLst, "horizontal");
                }
            }

        }

        public void vertical(int i, int j)
        {
            HashSet<Point> friendLst = new HashSet<Point>();
            friendLst.Add(new Point(i, j));
            int x = i;
            int y = j + 1;
            T t = graph[i, j];
            int sum = 1;
            while (y < cols)
            {
                if (graph[x, y].Equals(t))
                {
                    sum++;
                    friendLst.Add(new Point(x, y));
                    y++;
                }
                else { break; }
            }
            x = i;
            y = j - 1;
            while (y >= 0)
            {
                if (graph[x, y].Equals(t))
                {
                    sum++;
                    friendLst.Add(new Point(x, y));
                    y--;
                }
                else { break; }
            }
            if (sum >= 3)
            {
                foreach (Point elem1 in friendLst)
                {
                    updateFriendList(elem1, friendLst, "vertical");
                }
            }

        }
    }
}
