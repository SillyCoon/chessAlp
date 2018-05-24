using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject
{
    // Описывает ходы
    class Moves
    {
        FigureMoving FigureMoving;
        Board Board;

        public Moves(Board board)
        {
            this.Board = board;
        }

        // Проверяет, можно ли сделать такой ход
        public bool CanMove(FigureMoving fm)
        {
            this.FigureMoving = fm;

            return CanMoveFrom() && CanMoveTo() && CanFigureMove();
        }

        // Может ли фигура сходить с той клетки
        private bool CanMoveFrom()
        {
            return FigureMoving.From.OnBoard() && 
                FigureMoving.Figure.GetColor() == Board.MoveColor(); // Совпадают ли цвета и существует ли эта клетка
        }
    }
}
