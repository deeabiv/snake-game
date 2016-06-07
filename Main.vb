Imports System.Drawing.Drawing2D

Public Class main
    

    Private Structure strSnake
        Dim square As Rectangle
        Dim x As Integer
        Dim y As Integer
    End Structure
    Public Enum FoodType As Integer

        Apple = 0
        Banana = 1
        Grape = 2
        Orange = 3
        Papaya = 4
        Pineapple = 5
        Strawberry = 6
        Watermelon = 7
    End Enum

    Private Const INITIAL_LENGHT As Integer = 15  'lungimea initiala a sarpelui
    Private Const COLUMN_MAX As Integer = 65  'latimea spatiului de joc
    Private Const ROW_MAX As Integer = 47     'inaltimea spatiului de joc

    Private matrix(,) As Rectangle       '
    Private isSnake(,) As Boolean   '
    Private snake As Collection
    Private snakeHead As Image = RotateImage(Image.FromFile("head2.png"), 180) ' va memora capul sarpelui
    Private snakeHead1 As Image = Image.FromFile("head2.png")
    Private snakeBody As Image = Image.FromFile("body.png") ' corpul sarpelui
    Private snakeTail As Brush = New SolidBrush(Color.Red) 'coada sarpelui
    'Private bonus As Image = Image.FromFile("star1.ico")
    Private backBrush As Brush = New SolidBrush(Color.FromArgb(1, 1, 1)) ' pentru a acoperi la deplasarea sarpelui ultimul dreptunghi
    Private CurrentDirection As Direction
    Private background As Bitmap
    Private snakePoints As Integer ' contorizam nr fructelor mancate
    Private snakeSpeed As Double
    Private snakeLength As Integer
    Private food As Rectangle
    'Private Rbonus As Rectangle
    Private type As String = Nothing  'FREE sau WALL
    Dim check As String = Nothing ' pentru repetarea jocului
    Dim nr As Integer = 0
    Dim d As Dictionary(Of FoodType, Image)



    Public Sub InitiateFoodImages()

        d = New Dictionary(Of FoodType, Image)(10)

        d.Add(FoodType.Apple, Image.FromFile("Icons/apple.ico"))
        d.Add(FoodType.Banana, Image.FromFile("Icons/banana.ico"))
        d.Add(FoodType.Grape, Image.FromFile("Icons/grape.ico"))
        d.Add(FoodType.Orange, Image.FromFile("Icons/orange.ico"))
        d.Add(FoodType.Papaya, Image.FromFile("Icons/papaya.ico"))
        d.Add(FoodType.Pineapple, Image.FromFile("Icons/pineapple.ico"))
        d.Add(FoodType.Strawberry, Image.FromFile("Icons/strawberry.ico"))
        d.Add(FoodType.Watermelon, Image.FromFile("Icons/watermelon.ico"))

    End Sub
    Public Function GetFoodType() As FoodType

        Return CType(New Random().Next(8), FoodType) 'conversie FoodType
    End Function
    Public Function GetImageForFoodItem(ByVal ft As FoodType) As Image

        If IsNothing(d) Or Not d.ContainsKey(ft) Then 'd=null
            Return Nothing
        End If
        ' returneaza imaginea
        Return d(ft)
    End Function
    Private Enum Direction
        right
        down
        left
        up
    End Enum

    Private Function GetRotatedImage(ByVal pI As Image) As Image 'neutilizata

        If IsNothing(pI) Then
            Return Nothing
        End If
        If CurrentDirection = 1 Then
            Return pI
        ElseIf CurrentDirection = 2 Then
            Return RotateImage(pI, 180)
        ElseIf CurrentDirection = 3 Then
            Return RotateImage(pI, 270)
        ElseIf CurrentDirection = 4 Then
            Return RotateImage(pI, 90)
        End If

        Return Nothing
    End Function

    Public Function RotateImage(ByVal pImage As Image, ByVal pAngle As Integer) As Image

        If IsNothing(pImage) = True Then
            Return Nothing
        End If

        Dim b As Bitmap = New Bitmap(pImage.Width, pImage.Height)
        Dim g As Graphics = Graphics.FromImage(b)

        ' setam punctul de rotatie
        g.TranslateTransform(b.Width / 2, b.Height / 2)
        g.RotateTransform(pAngle)
        g.TranslateTransform(-b.Width / 2, -b.Height / 2)
        g.DrawImage(pImage, New Point(0, 0))


        Return b

    End Function



    Private Sub initialize()

        InitiateFoodImages()

        CurrentDirection = Direction.down   'directia de start a sarpelui in jos

        snakePoints = 0
        If check = "Again" Then
            snakeHead = RotateImage(snakeHead1, 180)
            nr = 0
        End If
        initRectangles()
        initSnake()
        selectRectangles()
        setFood()
        setPoints()
        tmr.Interval = 50
        tmr.Enabled = True


    End Sub

    Private Sub initRectangles()

        Dim i As Integer
        Dim j As Integer

        ReDim matrix(COLUMN_MAX, ROW_MAX)
        ReDim isSnake(COLUMN_MAX, ROW_MAX)
        'stabilim grosimea sarpelui
        For j = 0 To ROW_MAX
            For i = 0 To COLUMN_MAX
                matrix(i, j) = New Rectangle((i * 10) + 1, (j * 10) + 1, 9, 9) ' desenam suprafata de joc cu dreptunghiuri de dimensiunea 

                isSnake(i, j) = False
            Next
        Next

        '  status.Items("tss0").Text = "Screen Size: " & CStr(COLUMN_MAX) & " X " & CStr(ROW_MAX)

    End Sub


    Private Sub initSnake() 'initializarea sarpelui

        Dim x As Integer
        Dim y As Integer
        Dim i As Integer
        Dim bodySnake As strSnake
        snake = New Collection 'aici vom memora sarpele intr/o colectie de structuri

        x = ((COLUMN_MAX) - 10) \ 2 'pozitia sarpelui de start (x,y)
        y = ((ROW_MAX) - 6) \ 2

        For i = 1 To INITIAL_LENGHT
            bodySnake.square = matrix(x, y)
            bodySnake.x = x
            bodySnake.y = y
            snake.Add(bodySnake)
            y -= 1
        Next

        snakeLength = INITIAL_LENGHT
        snakeSpeed = 1000
        status.Items("tssSnakeLength").Text = "Length: " & CStr(snakeLength)
        status.Items("tssSnakeSpeed").Text = "Speed: " & CStr(snakeSpeed) & "%"

    End Sub

    Private Sub selectRectangles()

        Dim g As Graphics = Graphics.FromImage(My.Resources.back)
        Dim i As Integer
        Dim bodySnake As strSnake

        For i = 1 To INITIAL_LENGHT - 1
            bodySnake = snake(i)

            g.DrawImage(snakeBody, bodySnake.square)
            isSnake(bodySnake.x, bodySnake.y) = True
        Next
        bodySnake = snake(INITIAL_LENGHT)
        g.FillRectangle(snakeTail, bodySnake.square)

        isSnake(bodySnake.x, bodySnake.y) = True

        background = New Bitmap(My.Resources.back)

        g.Dispose()
        Refresh()

    End Sub



    Private Sub setFood()

        Randomize()
        Dim x As Integer
        Dim y As Integer
        Dim g As Graphics = Graphics.FromImage(background)

        x = CInt(Rnd() * COLUMN_MAX)
        Do While x > COLUMN_MAX Or isSnake(x, y) = True
            x = CInt(Rnd() * COLUMN_MAX)
        Loop

        y = CInt(Rnd() * ROW_MAX)
        Do While y > ROW_MAX Or y < 2 Or isSnake(x, y) = True
            y = CInt(Rnd() * ROW_MAX)
        Loop

        food = Matrix(x, y)  'pozitia mancarii 
        ' status.Items("tss1").Text = "food Location: ( " & CStr(x) & " , " & CStr(y) & " )"
        g.DrawImage(GetImageForFoodItem(GetFoodType()), food)
        Refresh()
        g.Dispose()

    End Sub
    

    Private Sub setPoints()
        status.Items("tss3").Text = "Total Points: " & CStr(snakePoints)
    End Sub

    Private Sub main_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.Down
                If Not (CurrentDirection = Direction.down Or CurrentDirection = Direction.up) Then
                    CurrentDirection = Direction.down
                    snakeHead = snakeHead1
                    snakeHead = RotateImage(snakeHead, 180)
                End If

            Case Keys.Left
                If Not (CurrentDirection = Direction.left Or CurrentDirection = Direction.right) Then
                    CurrentDirection = Direction.left
                    snakeHead = snakeHead1
                    snakeHead = RotateImage(snakeHead, 270)
                End If

            Case Keys.Right
                If Not (CurrentDirection = Direction.right Or CurrentDirection = Direction.left) Then
                    CurrentDirection = Direction.right
                    snakeHead = snakeHead1
                    snakeHead = RotateImage(snakeHead, 90)
                End If

            Case Keys.Up
                If Not (CurrentDirection = Direction.up Or CurrentDirection = Direction.down) Then
                    CurrentDirection = Direction.up
                    snakeHead = snakeHead1
                End If
        End Select

    End Sub
    Private Sub tmr_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr.Tick

        moveSnake()
    End Sub

    Private Sub moveSnake()

        Dim bodySnake As strSnake
        Dim x As Integer
        Dim y As Integer
        Dim rect As Rectangle = New Rectangle()
        Dim g As Graphics = Graphics.FromImage(background)

        tmr.Enabled = False

        ' stergem ultimul dreptunghi
        bodySnake = snake(snake.Count)
        g.FillRectangle(backBrush, bodySnake.square) ' acoperim ultimul dreptunghi cu culoarea fundalului
        snake.Remove(snake.Count)
        isSnake(bodySnake.x, bodySnake.y) = False

        'plasarea initiala a cozii

        nr += 1
        If nr >= 14 Then
            bodySnake = snake(snake.Count)
            g.FillRectangle(snakeTail, bodySnake.square)
        End If
        ' Obtinem indexul primului drpetunghi

        bodySnake = snake.Item(1)

        g.DrawImage(snakeBody, bodySnake.square)

        x = bodySnake.x
        y = bodySnake.y

        Select Case CurrentDirection
            Case Direction.down
                y = y + 1
                If type = "FREE" Then
                    If y > ROW_MAX Then y = 2
                Else
                    If y > ROW_MAX Then
                        Dim r = MsgBox("Game Over! Start again?", MsgBoxStyle.YesNo)
                        If r = vbYes Then
                            initialize()
                            check = "Again"
                            tmr.Enabled = True
                            Return
                        Else
                            Close()
                        End If
                    End If
                End If
            Case Direction.left
                x = x - 1
                If type = "FREE" Then
                    If x < 0 Then x = COLUMN_MAX
                Else
                    If x < 0 Then
                        Dim r = MsgBox("Game Over! Start again?", MsgBoxStyle.YesNo)
                        If r = vbYes Then
                            check = "Again"
                            initialize()
                            tmr.Enabled = True
                            Return
                        Else
                            Close()
                        End If
                    End If
                End If
            Case Direction.right
                x = x + 1
                If type = "FREE" Then
                    If x > COLUMN_MAX Then x = 0
                Else
                    If x > COLUMN_MAX Then
                        Dim r = MsgBox("Game Over! Start again?", MsgBoxStyle.YesNo)
                        If r = vbYes Then
                            check = "Again"
                            initialize()
                            tmr.Enabled = True
                            Return
                        Else
                            Close()

                        End If
                    End If
                End If
            Case Direction.up
                y = y - 1
                If type = "FREE" Then
                    If y - 2 < 0 Then y = ROW_MAX
                Else
                    If y - 2 < 0 Then
                        Dim r = MsgBox("Game Over! Start again?", MsgBoxStyle.YesNo)
                        If r = vbYes Then
                            check = "Again"
                            initialize()
                            tmr.Enabled = True
                            Return
                        Else
                            Close()

                        End If
                    End If
                End If
        End Select

        If isSnake(x, y) = True Then
            tmr.Enabled = False

            If MessageBox.Show("Game over! Start again?", "Snake", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                check = "Again"
                initialize()
                Exit Sub
            Else
                End
            End If

        End If

        '  status.Items("tss2").Text = "Snake Location: ( " & CStr(x) & " , " & CStr(y) & " )"

        rect = matrix(x, y)

        bodySnake.x = x
        bodySnake.y = y
        bodySnake.square = rect
        isSnake(x, y) = True

        'SNAKE HEAD
        g.DrawImage(snakeHead, bodySnake.square)
        Me.BackgroundImage = background

        ' Adaugam dreptunghiul cu cap la inceputul colectiei
        snake.Add(bodySnake, , 1)


        If Matrix(x, y).Equals(CObj(food)) Then

            snakePoints += 1 'numara fructele mancate
            setPoints()
            

            bodySnake = snake.Item(snake.Count)

            '
            Select Case CurrentDirection
                Case Direction.down

                    bodySnake.y -= 1
                Case (Direction.left)

                    bodySnake.x += 1
                Case Direction.right
                    bodySnake.x -= 1

                Case Direction.up
                    bodySnake.y += 1
            End Select

            bodySnake.square = matrix(bodySnake.x, bodySnake.y)


            'cand mananca

            g.FillRectangle(snakeTail, bodySnake.square)
            Me.BackgroundImage = background
            snake.Add(bodySnake, , , snake.Count)
            snakeLength = snake.Count
            If tmr.Interval - 2 > 0 Then tmr.Interval -= 2 'marim viteza cu 2
            ' If tmr.Interval = 1 Then tmr.Interval = 1

            snakeSpeed = 100 + (((50 - tmr.Interval) / 50) * 100) 'calculam cu cat a crescut viteza
            status.Items("tssSnakeLength").Text = "Length: " & CStr(snakeLength)
            status.Items("tssSnakeSpeed").Text = "Speed: " & CStr(snakeSpeed) & "%"

            'End If

            setFood()

        End If

        
        Refresh()

        tmr.Enabled = True

    End Sub
    

    

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Dim msg As String = MsgBox("Are you sure you want to Exit?", MsgBoxStyle.YesNo, "Exit")
        If msg = vbYes Then
            Close()
        End If


    End Sub

    Private Sub PauseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PauseToolStripMenuItem.Click
        If PauseToolStripMenuItem.Text = "Pause" Then
            PauseToolStripMenuItem.Text = "Resume"
            tmr.Enabled = False
        Else
            PauseToolStripMenuItem.Text = "Pause"
            tmr.Enabled = True
        End If
    End Sub

    Private Sub BlocToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlocToolStripMenuItem.Click
        type = "FREE"
        initialize()
        tmr.Enabled = True
    End Sub

    Private Sub WallColisionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WallColisionToolStripMenuItem.Click
        type = "WALL"
        initialize()
        tmr.Enabled = True
    End Sub

    Private Sub FileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileToolStripMenuItem.Click
        
            tmr.Enabled = False

    End Sub

    Private Sub main_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If Asc(e.KeyChar) = 32 Then
            PauseToolStripMenuItem_Click(sender, e)
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        tmr.Enabled = False
        MsgBox("SNAKE GAME" & vbCrLf & "Created by Andreea Bivol")
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        tmr.Enabled = False
        MsgBox("Use Arrow Keys to move the snake." & vbCrLf & "The goal is to eat as many fruits as posible." & vbCrLf & "Avoid self contact or else you'll die.")
    End Sub
End Class
