namespace CellularAutomata
{
    class CellularAutomata
    {
        public CellularAutomata(int size)
        {
            Board = new int[size, size];
            SupportBoard = new int[size, size];
            BoardFlag = true;
            BoardSize = size;
        }

        public bool BoardFlag { get; private set; }
        public int[,] SupportBoard { get; set; }
        public int[,] Board { get; set; }
        public int BoardSize { get; }

        public int GetBoard(int x, int y)
        {
            if (BoardFlag)
            {
                return Board[x, y];
            }
            return SupportBoard[x, y];
        }

        public void ChangeType(int x, int y)
        {
            if (BoardFlag)
            {
                if (Board[x, y] == 0)
                {
                    Board[x, y] = 1;
                }
                else
                {
                    Board[x, y] = 0;
                }
            }
            else
            {
                if (SupportBoard[x, y] == 0)
                {
                    SupportBoard[x, y] = 1;
                }
                else
                {
                    SupportBoard[x, y] = 0;
                }
            }
        }

        public void GenerateNextBoard()
        {
            for(int i = 0; i < BoardSize; i++)
            {
                for(int j = 0; j <BoardSize; j++)
                {
                    if (BoardFlag)
                    {
                        int counter = CountNeighbour(Board, i, j);
                        if(Board[i,j] == 1 && counter != 3 && counter != 2)
                        {
                            SupportBoard[i, j] = 0;
                        } else if (Board[i,j] == 0 && counter == 3)
                        {
                            SupportBoard[i, j] = 1;
                        }
                        else
                        {
                            SupportBoard[i, j] = Board[i, j];
                        }
                    }
                    else
                    {
                        int counter = CountNeighbour(SupportBoard, i, j);
                        if (SupportBoard[i, j] == 1 && counter != 3 && counter != 2)
                        {
                            Board[i, j] = 0;
                        }
                        else if (SupportBoard[i, j] == 0 && counter == 3)
                        {
                            Board[i, j] = 1;
                        }
                        else
                        {
                            Board[i, j] = SupportBoard[i, j];
                        }
                    }
                }
            }
            BoardFlag = !BoardFlag;
        }

        private int CountNeighbour(int[,] board, int posX, int posY)
        {
            if (posX < BoardSize-1 && posX > 0 && posY < BoardSize-1 && posY > 0) 
            {
                return board[posX - 1, posY - 1] + board[posX - 1, posY] +
                    board[posX - 1, posY + 1] + board[posX, posY - 1] + board[posX, posY + 1] +
                    board[posX + 1, posY - 1] + board[posX + 1, posY] + board[posX + 1, posY + 1];
            } else if (posX == 0 && posY == 0) 
            {
                return board[posX, posY + 1] + board[posX + 1, posY] +
                    board[posX + 1, posY + 1] + board[posX, BoardSize - 1] + board[posX + 1, BoardSize - 1] +
                    board[BoardSize - 1, BoardSize - 1] + board[BoardSize - 1, posY] + board[BoardSize - 1, posY + 1];
            } else if (posX == 0 && posY == (BoardSize -1)) 
            {
                return board[posX, posY - 1] + board[posX + 1, posY - 1] +
                    board[posX + 1, posY] + board[BoardSize - 1, posY - 1] + board[BoardSize - 1, posY] +
                    board[BoardSize - 1, 0] + board[posX, 0] + board[posX + 1, 0];
            } else if (posX == (BoardSize - 1) && posY == 0)
            {
                return board[posX - 1, posY] + board[posX - 1, posY + 1] +
                    board[posX, posY + 1] + board[posX, BoardSize - 1] + board[posX - 1, BoardSize - 1] +
                    board[0, BoardSize - 1] + board[0, posY] + board[0, posY + 1];
            }
            else if (posX == (BoardSize - 1) && posY == (BoardSize - 1))
            {
                return board[posX, posY - 1] + board[posX - 1, posY - 1] +
                    board[posX - 1, posY] + board[posX - 1, 0] + board[posX, 0] +
                    board[0, 0] + board[0, posY] + board[0, posY - 1];
            }
            else if (posY == 0)
            {
                return board[posX - 1, posY] + board[posX - 1, posY + 1] +
                board[posX, posY + 1] + board[posX + 1, posY] + board[posX + 1, posY + 1]
                + board[posX - 1, BoardSize - 1] + board[posX, BoardSize - 1] + board[posX + 1, BoardSize - 1];
            }
            else if (posY == (BoardSize - 1))
            {
                return board[posX - 1, posY] + board[posX - 1, posY - 1] +
                board[posX, posY - 1] + board[posX + 1, posY] + board[posX + 1, posY - 1]
                + board[posX - 1, 0] + board[posX, 0] + board[posX + 1, 0];
            }
            else if (posX == 0)
            {
                return board[posX, posY - 1] + board[posX, posY + 1] +
                board[posX + 1, posY - 1] + board[posX + 1, posY] + board[posX + 1, posY + 1]
                + board[BoardSize - 1, posY - 1] + board[BoardSize - 1, posY] + board[BoardSize - 1, posY + 1];
            }
            else
            {       // (posX == (boardSize-1))
                return board[posX, posY - 1] + board[posX, posY + 1] +
                board[posX - 1, posY - 1] + board[posX - 1, posY] + board[posX - 1, posY + 1]
                + board[0, posY - 1] + board[0, posY] + board[0, posY + 1];
            }
        }
    }
}
