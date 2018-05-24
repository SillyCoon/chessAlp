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
    }
}
