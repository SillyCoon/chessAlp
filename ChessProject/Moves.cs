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
                FigureMoving.Figure.GetColor() == Board.MoveColor; // Совпадают ли цвета и существует ли эта клетка
        }

        // Может ли фигура перейти на выбранную клетку
        private bool CanMoveTo()
        {
            return FigureMoving.To.OnBoard() && FigureMoving.From != FigureMoving.To &&
                Board.GetFigureAt(FigureMoving.To).GetColor() != Board.MoveColor; // Клетка должна существовать и на ней не должно быть фигуры
                                                                                  // того же цвета
        }

        //  Может ли фигура действительно двигаться (правила для каждой фигуры)
        private bool CanFigureMove()
        {
            switch (FigureMoving.Figure)
            {
                case Figure.none:
                    return false;
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanKingMove();
                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return CanStraightMove();
                case Figure.whiteRook:
                case Figure.blackRook:
                    return CanRookMove();
                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return CanBishopMove();
                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return CanKnightMove();
                case Figure.whitePawn:
                case Figure.blackPawn:
                    return CanPawnMove();
                default: return false;
            } 
        }

        private bool CanKingMove()
        {
            if(FigureMoving.AbsDeltaX <= 1 && FigureMoving.AbsDeltaY <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanKnightMove()
        {
            if (FigureMoving.AbsDeltaX == 1 && FigureMoving.AbsDeltaY == 2) return true;
            if (FigureMoving.AbsDeltaY == 1 && FigureMoving.AbsDeltaX == 2) return true;
            return false;
        }

        private bool CanRookMove()
        {
            return (FigureMoving.SignX == 0 || FigureMoving.SignY == 0) && CanStraightMove();
        }

        // Может ли фигура идти прямо по диагонали
        private bool CanStraightMove()
        {
            Square at = FigureMoving.From;
            do
            {
                at = new Square(at.x + FigureMoving.SignX, at.y + FigureMoving.SignY); // Двигается на клетку в сторону (по диагонали тоже)
                if (at == FigureMoving.To) // Если пришли на нужную, то тру
                {
                    return true;
                }
            } while (at.OnBoard() && Board.GetFigureAt(at) == Figure.none); // Двигаемся пока клетка на доске и впереди нет фигур
            return false;
        }

        private bool CanBishopMove()
        {
            return (FigureMoving.SignX != 0 || FigureMoving.SignY != 0) && CanStraightMove();
        }

        private bool CanQueenMove()
        {
            return false;
        }

        private bool CanPawnMove()
        {
            if (FigureMoving.From.y < 1 || FigureMoving.From.y > 6) // Не может быть на 1 и последней горизонтали
            {
                return false;
            }
            int stepY = FigureMoving.Figure.GetColor() == Color.white ? 1 : -1; // Направление белой и черной пешки
            return CanPawnGo(stepY) ||
                CanPawnJump(stepY) ||
                CanPawnEat(stepY);

        }

        // Может ли пешка срубить фигуру
        private bool CanPawnEat(int stepY)
        {
            if (Board.GetFigureAt(FigureMoving.To) != Figure.none) // На клетке есть фигура
            {
                if (FigureMoving.AbsDeltaX == 1) // Смещается на 1 влево или вправо
                {
                    if (FigureMoving.DeltaY == stepY) // Смещение равно смещению
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        // Может ли пешка сходить на 2 клетки вперед
        private bool CanPawnJump(int stepY)
        {
            if (Board.GetFigureAt(FigureMoving.To) == Figure.none)
            {
                if (FigureMoving.DeltaX == 0)
                {
                    if(FigureMoving.DeltaY == 2 * stepY) // Если смещение равно степ*2
                    {
                        if (FigureMoving.From.y == 1 || FigureMoving.From.y == 6) // Ходит со 2 линии
                        {
                            if(Board.GetFigureAt(new Square(FigureMoving.From.x, FigureMoving.From.y + stepY)) == Figure.none) // Не перепрыгивает фигуру
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        // Может ли пешка сходить вперед
        private bool CanPawnGo(int stepY)
        {
            if (Board.GetFigureAt(FigureMoving.To) == Figure.none) // Если поле пустое
            {
                if (FigureMoving.DeltaX == 0) // Если идет по вертикали
                {
                    if(FigureMoving.DeltaY == stepY) // Если пешка идет прямо на 1 шаг
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
