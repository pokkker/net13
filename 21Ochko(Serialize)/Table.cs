using System;
using System.Collections;

namespace _21Ochko_Serialize_
{
    [Serializable]
    class Table
    {
        private ArrayList _deck = new ArrayList(37);
        private ArrayList _dealer = new ArrayList();
        private ArrayList _player = new ArrayList();
        public ArrayList Deck { get { return _deck; } set { _deck = value; } }
        public ArrayList Dealer { get { return _dealer; } set { _dealer = value; } }
        public ArrayList Player { get { return _player; } set { _player = value; } }
        //Crtor with inicialization our deck
        public Table()
        {
            for (var i = 1; i < 7; i++)
                for (var j = 0; j < 4; j++)
                    _deck.Add(i + 5);
            for (var i = 25; i < 28; i++)
                for (var j = 0; j < 4; j++)
                    _deck.Add(i - 23);
        }
    }
}
