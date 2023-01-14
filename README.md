# UriSudokuSolver
This is a generic Sudoku solver project written by Uri Sorek.
The main goal of the poject is to solve NXN sudoku boards (from 1X1 - 25X25) fast.

## Sudoku Rules
Sudoku game is played on a grid of N x N spaces.

Within the rows and columns are N “squares” (made up of Sqrt(N) x Sqrt(N) spaces).

Each row, column and square (N spaces each) needs to be filled out with the numbers 1-N, without repeating any numbers within the row, column or square.

In this sudoku solver project the empty spaces are represented by 0 and the max N value that a board can get is N=25 and it has to be a square number.

## About the project
This project is a genric board game solver C# projet. Right now the project supports only solving the game of sudoku with Console UI And Console/File IO, but because of the projects generic structure (OOP programing, multiple interfaces and SOLID principles), the avarege developer can easly add his own game board solver to the project.

### Sudoku Solving methods
In this project I chose to used bitwise Bactracking with some human tactics for as my board solving method.

Backtracking using bitwise optimization and human solving tactics algorithm:

1. Try to solve the board using human solving tactics until there is nothing to solve using human tactics.

2. Find the empty cell with the least possible values

3. Try to solve the board using backtracking (place a value in the cell and try to solve the board and call the solving function recursively --> from stage 1.).

4. If the board is solved return true.

5. If the board is not solved, remove the changed values from the relevant cells and try to solve the board with the next possible value.

6. If there is no possible value for the cell, return false.

#### Bitwise optimization
In the Sudoku Solver class there are 4 int array saved as properties:
- array of valid values for all rows
- array of valid values for all columns
- array of valid values for all boxs
- array of bit masks for each possible value that can be on the board.

The first three contain one integer number for each row/column/box that save the possible values in that row/column/box as binary number (0 if is valid value 1 if not). 

For example if the binary number (integer) representing a row is 011100111 it means the row missing the values 4,5,9 (index of the one's in the binary number.

The last array contains the bit masks of each value that the board can get. The solver use the bit masks to reduce/add specific value for a row/column/box possible values using bit operators.
For example: 000010000 is bit mask for 5 value. 

If we want to reduce from row with possible values of 1111011110 the value 5 we will do OR bit operation between he possible values and 5's bit mask and we will get 111111110.

If we want to add to row with possible values of 1111111110 the value 5 we will do AND NOT bit operations between the row possible values and 5's bit mask and we will get 111101110.


Using bitwise reduces the runtime of the backtracking. Bit operations is the fastests calculation for a cumputer. It also make the IsSafe() function, that checks if a value can be placed in a certian cell, O(1) with one calculation instead of O(n) (goes over the row, column and box of a cell and check if a value already exists).


#### Human tactics optimization
The project using two simple human solving algorithems:
 - naked singles
 - hidden singles
 
Naked singles - if a cell has only one possible value place the value in the cell.
Hidden singles - if all other cells in the row/column/box don't have a value that a cell can get, place the value in the cell.

The human tactics function runs on those two algorithm and fill the missing values in the board, until there are no more hidden or naked singles values to fill.

Human tactics optimization reduce the avarege runtime of solving (depending on the given board), and solve some hard boards that a regular backtracking algorithm doesn't solve.

Can make the easy sudoku boards slower in a few miliseconds.

#### Other optimizations
In this project I added a few optimizations:
 - Reducing memory usage using stack that saves the board changes
 - Saving "peers" for each cell and when changing a value go over the changed values "peers" only
 - Find the empty cell with the least posible values
 
Reducing memory usage using stack that saves the board changes - instead of copy the matrix and the arrays every time, the changes in the baords are saved in a stack, and every time the algorithm returns false (no solution), the algorithm pop the changed values indexes from the stack and place empty values back on the board. by that we save a lot of memory. 

Saving "peers" for each cell and when changing a value go over the changed values "peers" only -  every time a value is changed, enqueue it to a "peersQueue" and then go over all the peers of that cell. 
By saving peers (the cells that are effected if this cell would change) of each cell, we reduce runtime from O(n^2) (go over all the matrix every time) to O(n).

Find the empty cell with the least posible values - go over the matrix and find the empty cell with the least possible values.
By doing that we reducing the runtime of solving most od the sudoku boards.


## UI Options

When you run the project, this console window will appear:
![image](https://user-images.githubusercontent.com/58790516/212472305-f230f6d8-fb1c-47f4-9853-177630116c85.png)

As you can see there are 4 valid options in the menu:
- 's' - to solve a sudoku board
- 'r' - to display game rules
- 'm' - to show the menu again
- 'e' (or ctrl + c) - to exit the program
If the user doesn't select a valid input the program will ask him again for valid input.

### Solving options
After choosing 's' Solving option this message will appear: 
"Please enter the type of input you want to use --> c for console, f for file and e to exit:"

If the user choose Console this window will appear (it has all the instractions and example): 
![image](https://user-images.githubusercontent.com/58790516/212474224-b5a57632-3f8e-4408-a188-575d4d9d9be3.png)
The board has to be a board string like the example, otherwise the porgram will show error message in red color.

If the user choose File he'll be asked to give a valid path to a valid <b>txt file</b> with a valid string board.
The file path has to be a valid path to an existing txt file containing a valid board string, otherwise the porgram will show error message in red color.
The solution string board will be written to a new txt file with the txt file name + _Solved!:
![image](https://user-images.githubusercontent.com/58790516/212474866-88f97079-9c3e-4567-be4c-1d39350c7d9f.png)


In both cases, file or console, the solution board will be printed (the numbers will be green if solved, red if not -> has no solution) with the solution board string.
![image](https://user-images.githubusercontent.com/58790516/212474360-db4944d7-ff73-4809-a826-40193d8d7cf5.png)


## Build the project

Here are the steps for runing the project:
 - Clone the project to your local machine from this link: https://github.com/urisorek34/Uri_Sudoku_Solver 
 - Open visual studio version 2019-2022
 - Press File menu and choose "Open Project/Solution" (Ctrl + Shift + O)
 - Choose the project you cloned
 - Choose to use .NET version 6.0 (IMPORTENT!)
 - Run the project and have fun :))


## Tests
The project has 35 tests that contain Validation testing and sudoku board solving testing seperated by board difficulty.

To run the tests press right click on one of the public classes in the project and press Run Test (or Ctrl+R+T).
![image](https://user-images.githubusercontent.com/58790516/212475319-4b36e43c-7d67-485a-af92-68732ea3eeb6.png)
![image](https://user-images.githubusercontent.com/58790516/212475347-ed0305fa-aa05-42eb-8aab-efe3585698c5.png)



