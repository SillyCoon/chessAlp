using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject
{
    // Шахматная клетка
    struct Square
    {
        public int x { get; private set; }
        public int y { get; private set; }

        // Несуществующая клетка
        public static Square none = new Square(-1, -1);

        // Стандартный конструктор клетки, пихнем для Unity
        public Square(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Для создания клетки с помощью символьного обозначения
        public Square(string e2)
        {
            if(e2.Length == 2 &&
                e2[0] >= 'a' &&
                e2[0] <= 'h' &&
                e2[1] >= '1' && 
                e2[1] <= '8')
            {
                x = e2[0] - 'a';
                y = e2[1] - '1';
            }
            else
            {
                this = none;
            }
        }

        // Находится ли клетка на доске?
        public bool OnBoard()
        {
            return x >= 0 && x < 8 && y >= 0 && y < 8;
        }
    }
}
