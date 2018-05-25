using System;
using System.Collections.Generic;

namespace ChessProject
{
    // Класс игры
    public class Chess // Immutable класс, не изменяется во времени
    {

        // Положение фигур по нотации Форсайта — Эдвардса
        public string fen;

        Board Board;

        Moves Moves;

        List<FigureMoving> AllMoves; // Все возможные ходы на доске

        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;           
            Board = new Board(fen);   // Создаем доску на основе фена
            Moves = new Moves(Board); // Инициализируем возможные ходы для существующей доски
        }

        // Приватный конструктор, создающий игру по доске
        private Chess(Board board)
        {
            this.Board = board;
            this.fen = board.Fen;
            Moves = new Moves(Board);
        }

        // Создает новое положение доски (после хода)
        public Chess Move (string move)
        {
            FigureMoving fm = new FigureMoving(move); // Двигаемая фигура
            if (!Moves.CanMove(fm)) // Проверяем, можем ли мы двигать эту фигуру
            {
                return this; // Если нет, оставляем ситуацию на доске той же
            }
            Board nextBoard = Board.Move(fm); // Двигаем фигуру по доске
            Chess nextChess = new Chess(nextBoard); // Новый экземпляр игры
            return nextChess;
        }

        // Возвращает положение фигуры
        public char GetFigureAt (int x, int y)  
        {
            Figure figure = Board.GetFigureAt(new Square(x, y)); // получает фигуру с клетки
            return figure == Figure.none ? '.' : (char)figure; // Возвращает точку, если фигуры не стоит, иначе фигуру в виде символа

        }

        // Получает все возможные на данный момент ходы текущего цвета
        private void FindAllMoves()
        {
            AllMoves = new List<FigureMoving>(); // инициализируем ходы
            foreach (FigureOnSquare fs in Board.YieldFigures()) // Для каждой фигуры того цвета на доске
            {
                foreach (Square to in Square.YieldSquares()) // Для каждого квадрата на доске
                {
                    FigureMoving fm = new FigureMoving(fs, to); // Создаем двигаемую фигуру на основе всех доступных фигур и квадратов
                    if (Moves.CanMove(fm)) // Если можем ее двигать
                    {
                        AllMoves.Add(fm); // Добавляем в список
                    }
                }
            }
        }
        
        // Возвращает все возможные ходы
        public List<string> GetAllMoves()
        {
            FindAllMoves();
            List<string> list = new List<string>();
            foreach(FigureMoving fm in AllMoves)
            {
                list.Add(fm.ToString());
            }
            return list;
        }
    }
}
