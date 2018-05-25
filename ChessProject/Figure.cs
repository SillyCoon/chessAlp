using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject
{

    // Перечисление фигур
    enum Figure
    {
        none,

        whiteKing = 'K',
        whiteQueen = 'Q',
        whiteRook = 'R',
        whiteBishop = 'B',
        whiteKnight = 'N',
        whitePawn = 'P',

        blackKing = 'k',
        blackQueen = 'q',
        blackRook = 'r',
        blackBishop = 'b',
        blackKnight = 'n',
        blackPawn = 'p'

    }


    // Класс расширений для фигуры
    static class FigureMethods
    {
        // Возвращает цвет фигуры
        public static Color GetColor (this Figure figure)
        {
            if (figure == Figure.none)
            {
                return Color.none;
            }
            else if (figure == Figure.whiteBishop || figure == Figure.whiteKing
                || figure == Figure.whiteQueen|| figure == Figure.whiteKnight
                || figure == Figure.whitePawn || figure == Figure.whiteRook)
            {
                return Color.white;
            }
            else
            {
                return Color.black;
            }
        }
    }
}
