using System;

public class Matrix {

    public int rows, columns;
    public double[] matrix;

    public Matrix(int columns, int rows) {
        matrix = new double[rows * columns];
        this.rows = rows;
        this.columns = columns;
    }

    public double get(int column, int row) {
        return matrix[row * columns + column];
    }
    
    public void set(int column, int row, double value) {
        matrix[row * columns + column] = value;
    }

    public void dump() {
        int space = 1;
        foreach (double num in matrix) {
            space = Math.Max(num.ToString("G").Length + 1, space);
        }
        for (int row = 0; row < rows; row++) {
            string line = "";
            for (int column = 0; column < columns; column++) {
               string element = get(column, row).ToString("G");
               while (element.Length < space) {
                    element += " ";
               }
               line += element;
            }
            Console.WriteLine(line);
        }
    }

    public Matrix plus(Matrix other) {
        if (columns != other.columns || rows != other.rows) {
            throw new Exception("Unsupported plus operation: different matrices dimension");
        }

        Matrix result = new Matrix(rows, columns);
        for (int row = 0; row < rows; row++) {
            for (int column = 0; column < columns; column++) {
                result.set(column, row, get(column, row) + other.get(column, row));
            }
        }

        return result;
    }

    public Matrix multiply(Matrix other) {
        if (columns != other.rows) {
            throw new Exception("Unsupported multiply operation: unsupported matrices dimension");
        }

        Matrix result = new Matrix(other.columns, rows);
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < other.columns; col++) {
                double sum = 0;
                for (int k = 0; k < columns; k++) {
                    sum += get(k, row) * other.get(col, k);
                }
                result.set(col, row, sum);
            }
        }

        return result;
    }

    public double determinant() {
        if (rows != columns) {
            throw new Exception("Unsupported operations: determinant requires sequare matrix");
        }
        
        if (rows == 1) {
            return get(0, 0);
        }
        
        if (rows == 2) {
            return get(0, 0) * get(1, 1) - get(1, 0) * get(0, 1);
        }
        
        double det = 0;
        for (int i = 0; i < columns; i++) {
            Matrix subMatrix = createSubMatrix(0, i);
            int sign = (i % 2 == 0) ? 1 : -1;
            det += sign * get(i, 0) * subMatrix.determinant();
        }
        
        return det;
    }
    
    private Matrix createSubMatrix(int excludeRow, int excludeCol) {
        Matrix subMatrix = new Matrix(columns - 1, rows - 1);
        int subRow = 0;
        
        for (int row = 0; row < rows; row++) {
            if (row == excludeRow) continue;
            
            int subCol = 0;
            for (int col = 0; col < columns; col++) {
                if (col == excludeCol) continue;
                
                subMatrix.set(subCol, subRow, get(col, row));
                subCol++;
            }
            subRow++;
        }
        
        return subMatrix;
    }

    public Matrix inverse() {
        if (rows != columns) {
            throw new Exception("Unsupported operations: inverse requires square matrix");
        }
        
        double det = determinant();
        if (det == 0) {
            throw new Exception("Unsupported operations: matrix determinant is zero");
        }
        
        Matrix adjugate = adjugateMatrix();
        Matrix inverse = new Matrix(columns, rows);
        
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < columns; col++) {
                inverse.set(col, row, adjugate.get(col, row) / det);
            }
        }
        
        return inverse;
    }
    
    private Matrix adjugateMatrix() {
        Matrix adjugate = new Matrix(columns, rows);
        
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < columns; col++) {
                Matrix subMatrix = createSubMatrix(row, col);
                int sign = ((row + col) % 2 == 0) ? 1 : -1;
                adjugate.set(row, col, sign * subMatrix.determinant());
            }
        }
        
        return adjugate;
    }
    
    public Matrix transpose() {
        Matrix transposed = new Matrix(rows, columns);
        
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < columns; col++) {
                transposed.set(row, col, get(col, row));
            }
        }
        
        return transposed;
    }


    public static void fillRandom(Matrix matrix, int min, int max) {
        Random rnd = new Random();
        for (int column = 0; column < matrix.columns; column++) {
            for (int row = 0; row < matrix.rows; row++) {
                matrix.set(column, row, rnd.Next(min, max));
            }
        }
    }
    
    public static void fillInput(Matrix matrix) {
        for (int column = 0; column < matrix.columns; column++) {
            for (int row = 0; row < matrix.rows; row++) {
                Console.Write($"Number on column={column}, row={row}: ");
                matrix.set(column, row, Convert.ToDouble(Console.ReadLine()));   
            }
        }
    }

    public static void Main() {
        Matrix first = new Matrix(4, 3);
        Matrix second = new Matrix(3, 4);
        fillRandom(first, 0, 10);
        fillRandom(second, 0, 10);
        
        Matrix matrix = first.multiply(second);

        matrix.dump();
        matrix.transpose().dump();
        matrix.inverse().dump();
        Console.WriteLine(matrix.determinant());
    }
    
}