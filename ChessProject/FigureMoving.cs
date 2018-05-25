using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject
{
    // Перемещение фигуры
    class FigureMoving
    {
        public Figure Figure { get; private set; }
        public Square From { get; private set; }
        public Square To { get; private set; }
        public Figure Promotion { get; private set; }

        // Принимает фигуру на доске, куда ей двигаться и получает ли промоушн
        public FigureMoving(FigureOnSquare fs, Square to, Figure promotion = Figure.none)
        {
            this.Figure = fs.Figure;
            this.From = fs.Square;
            this.To = to;
            this.Promotion = promotion;
        }

        // Ход в текстовом варианте
        public FigureMoving(string move)
        {
            this.Figure = (Figure)move[0];
            this.From = new Square(move.Substring(1, 2));
            this.To = new Square(move.Substring(3, 2));
            this.Promotion = (move.Length == 6) ? (Figure)move[5] : Figure.none;
        }


        // Разница между исходным и конечным положением фигуры
        public int DeltaX {
            get
            {
                return To.x - From.x;
            }
        }

        public int DeltaY
        {
            get
            {
                return To.y - From.y;
            }
        }

        // Абсолютные показатели разницы
        public int AbsDeltaX
        {
            get
            {
                return Math.Abs(DeltaX);
            }
        }

        public int AbsDeltaY
        {
            get
            {
                return Math.Abs(DeltaY);
            }
        }

        // Узнаем направление движения фигуры
        public int SignX
        {
            get
            {
                return Math.Sign(DeltaX);
            }
        }

        public int SignY
        {
            get
            {
                return Math.Sign(DeltaY);
            }
        }


        // Переопределяем ToString
        override public string ToString()
        {
            string text = (char)Figure + From.Name + To.Name;
            if (Promotion != Figure.none)
            {
                text += (char)Promotion;
            }
            return text;
        }

    }
}
