using System;

namespace ChessProject
{
    // Класс игры
    public class Chess // Immutable класс, не изменяется во времени
    {

        // Положение фигур по нотации Форсайта — Эдвардса
        public string fen;

        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;
        }

        // Создает новое положение доски
        public Chess Move (string move)
        {
            Chess nextChess = new Chess(fen);
            return nextChess;
        }

        // Возвращает положение фигуры
        public char GetFigureAt (int x, int y)
        {
            return '.';
        }
    }
}
