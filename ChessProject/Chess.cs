using System;

namespace ChessProject
{
    // Класс игры
    public class Chess // Immutable класс, не изменяется во времени
    {

        // Положение фигур по нотации Форсайта — Эдвардса
        public string fen;
        Board Board;
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;
            Board = new Board(fen);
        }

        // Приватный конструктор, создающий игру по доске
        private Chess(Board board)
        {
            this.Board = board;
            this.fen = board.Fen;

        }

        // Создает новое положение доски
        public Chess Move (string move)
        {
            FigureMoving fm = new FigureMoving(move); // Двигаемая фигура
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
