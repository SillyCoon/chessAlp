using System;
using System.Collections.Generic;
using System.Text;

namespace ChessProject
{
    // Класс шахматной доски 
    class Board
    {
        public string Fen { get; private set; }
        Figure[,] Figures;
        public Color MoveColor { get; private set; } // Цвет ходящего

        public int MoveNumber { get; private set; } // Номер хода, увеличивается после хода черных

        public Board( string fen)
        {
            this.Fen = fen;
            Figures = new Figure[8, 8]; // Матрица фигур на доске
            Init(); // Парсит Fen, инициализирует положение фигур

        }

        void Init()
        {
            //rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
            //0                                           1 2    3 4 5
            // 0 - положение фигур
            // 2 - очередь хода
            // 2 - флаги рокировки
            // 3 - битое поле
            // 4 - кол-во ходов
            // 5 - номер хода

            string[] parts = Fen.Split();
            if (parts.Length != 6)
            {
                return;
            }

            InitFigures(parts[0]);
            MoveColor = parts[1] == "b" ? Color.black : Color.white;
            MoveNumber = int.Parse(parts[5]);
        }

        // Парсим фен фигур на фигуры в матрице
        void InitFigures(string figures)
        {
            for(int j = 8; j >=2; j--)
            {
                figures = figures.Replace(j.ToString(), (j - 1).ToString() + "1");
            }
            figures = figures.Replace("1", ".");
            string[] lines = figures.Split('/');

            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    Figures[x, y] = lines[7-y][x] == '.' ? Figure.none : (Figure)lines[7 - y][x];
                }
            }
        }

        // Получаем фигуру на данной клетке
        public Figure GetFigureAt (Square square)
        {
            if (square.OnBoard())
            {
                return Figures[square.x, square.y];
            }
            else
            {
                return Figure.none;
            }
        }

        public IEnumerable<FigureOnSquare> YieldFigures()
        {
            foreach(Square square in Square.YieldSquares()) // Для каждого квадрата
            {
                if (GetFigureAt(square).GetColor() == MoveColor) // Если данная фигура цвета текущего хода 
                {
                    yield return new FigureOnSquare(GetFigureAt(square), square); // возвращаем ее
                }
            }
        }

        // Устанавливает фигуру, вызывается только из Init во время нового хода
        private void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard())
            {
                Figures[square.x, square.y] = figure;
            }
        }

        // Генерирует фен на основе ситуации на доске
        private void GenerateFen()
        {
            Fen = FenFigures() + " " + (MoveColor == Color.white ? "w" : "b")+ 
                " - - 0 " + MoveNumber.ToString();
        }

        // Генерируем фигуры фена
        private string FenFigures()
        {
            StringBuilder builder = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                // Ставим 1 если в матрице нет фигур
                for (int x = 0; x <= 7; x++)
                {
                    builder.Append(Figures[x, y] == Figure.none ? '1' : (char)Figures[x, y]);
                }
                // Если линия фигур кончилась, то отделяем слешем
                if (y > 0)
                {
                    builder.Append('/');
                }
            }
            string eight = "11111111"; // Менякм кучу единиц на восьмерки (правильно по нотации)
            for (int j = 8; j >= 2; j--)
            {
                builder.Replace(eight.Substring(0, j), j.ToString());
            }
            return builder.ToString();
        }

        // Метод хода, создаем новую доску с измененным положением фигур
        public Board Move (FigureMoving fm)
        {

            Board next = new Board(Fen); // Новая доска с тем же феном 
            next.SetFigureAt(fm.From, Figure.none); // Удаляем фигуру с прошлой клетки
            next.SetFigureAt(fm.To, fm.Promotion == Figure.none ? fm.Figure : fm.Promotion); // Двигаем фигуру (или еще и повышаем)
            if (MoveColor == Color.black) // Увеличиваем номер хода, если ходили черные
            {
                next.MoveNumber++;
            }
            next.MoveColor = MoveColor.FlipColor(); // Меняем игрока
            next.GenerateFen(); // Восстанавливаем фен по положению фигур
            return next;
        }
    }
}
