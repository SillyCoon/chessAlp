using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject
{
    // Описывает фигуру на доске, то есть сама фигура и ее квадрат
    class FigureOnSquare
    {
        public Figure Figure { get; private set; }
        public Square Square { get; private set; }

        public FigureOnSquare(Figure figure, Square square)
        {
            this.Figure = figure;
            this.Square = square;
        }
    }
}
