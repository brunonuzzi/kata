using System;
using System.Collections.Generic;
using System.Linq;

namespace Kata.Snail
{
    public static class TestSnail
    {
        public static void Test()
        {
            //Kata Url
            //https://www.codewars.com/kata/521c2db8ddc89b9b7a0000c1

            int[][] array =
            {
                new []{1, 2, 3},
                new []{4, 5, 6},
                new []{7, 8, 9}
            };
            //int[][] array = new int[][] { };
            OrderMatrix matrix = new OrderMatrix(array);
            var result = matrix.ScanArray();

            Console.WriteLine("End");
        }
    }
}

public class OrderMatrix
{
    private List<Axes> visitedDirections = new List<Axes>();
    int[][] input;
    int[] result;
    Direction runningDirection;
    Axes runningAxes = new Axes();
    int resultArrayIndex = 0;

    public OrderMatrix(int[][] input)
    {
        this.input = input;
        result = new int[calculateInputSize()];
        runningDirection = Direction.LeftRight;
    }

    private int calculateInputSize()
    {
        int size = 0;
        foreach (var item in input)
        {
            size += item.Length;
        }
        return size;
    }

    public int[] ScanArray()
    {
        if (result.Length == 0) return new int[] { };


        var movement = calculateMovement(runningDirection); //First movement
        do
        {

            result[resultArrayIndex] = input[runningAxes.row][runningAxes.column];
            visitedDirections.Add(new Axes { row = runningAxes.row, column = runningAxes.column });

            Console.WriteLine("Found number {0} at Row:{1} Column:{2}", result[resultArrayIndex], runningAxes.row, runningAxes.column);

            if (!availableDiretion(runningAxes.row + movement.axes.row, runningAxes.column + movement.axes.column)) //Change Direction
            {
                movement = calculateMovement(movement.nextDirection);
                runningDirection = movement.nextDirection;
            }

            resultArrayIndex++;
            runningAxes.row += movement.axes.row;
            runningAxes.column += movement.axes.column;

        } while (visitedDirections.Count < result.Length);

        return result;
    }
    private NextMovement calculateMovement(Direction? runningDirection)
    {
        NextMovement nextMovement = new NextMovement();
        runningDirection = runningDirection ?? Direction.LeftRight;

        switch (runningDirection)
        {
            case Direction.LeftRight:
                {
                    nextMovement.nextDirection = Direction.TopDown;
                    nextMovement.axes.row = 0;
                    nextMovement.axes.column = 1;
                    break;
                }
            case Direction.TopDown:
                {
                    nextMovement.nextDirection = Direction.RightLeft;
                    nextMovement.axes.row = 1;
                    nextMovement.axes.column = 0;
                    break;
                }
            case Direction.RightLeft:
                {
                    nextMovement.nextDirection = Direction.DownTop;
                    nextMovement.axes.row = 0;
                    nextMovement.axes.column = -1;
                    break;
                }
            case Direction.DownTop:
                {
                    nextMovement.nextDirection = Direction.LeftRight;
                    nextMovement.axes.row = -1;
                    nextMovement.axes.column = 0;
                    break;
                }
        }
        return nextMovement;
    }
    private bool availableDiretion(int row, int column)
    {
        try
        {
            int i = input[row][column];
            return !visitedDirection(row, column);
        }
        catch (System.IndexOutOfRangeException)
        {
            return false;
        }
    }
    private bool visitedDirection(int row, int column)
    {
        return visitedDirections.Any(x => x.row == row && x.column == column);
    }

}

public class NextMovement
{
    public Direction nextDirection;
    public Axes axes;

    public NextMovement()
    {
        this.nextDirection = new Direction();
        this.axes = new Axes();
    }
}

public class Axes
{
    public int column { get; set; } = 0;
    public int row { get; set; } = 0;
}

public enum Direction
{
    LeftRight = 0,
    TopDown = 1,
    RightLeft = 2,
    DownTop = 3
}
