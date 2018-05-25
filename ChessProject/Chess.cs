using System;

namespace ChessProject
{
    // Класс игры
    public class Chess // Immutable класс, не изменяется во времени
    {

        // Положение фигур по нотации Форсайта — Эдвардса
        public string fen;

        Board Board;

        Moves Moves;

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
            if (!Moves.CanMove(fm))
            {
                return this;
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
    }
}
