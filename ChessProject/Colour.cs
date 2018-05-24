using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject
{

    // Перечисление цветов
    enum Color
    {
        none,
        black,
        white
    }

    // Добавим к перечислению метод для смены цвета фигуры
    static class ColorMethods
    {
        public static Color FlipColor (this Color color)
        {
            if (color == Color.black)
            {
                return Color.white;
            }

            if(color == Color.white)
            {
                return Color.black;
            }
            
            return Color.none;
        }
    }
}
