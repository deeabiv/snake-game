Imports System.Drawing.Drawing2D

Public Class main
    

    Private Structure strSnake
        Dim square As Rectangle
        Dim x As Integer
        Dim y As Integer
    End Structure
    Private Enum Direction
        right
        down
        left
        up
    End Enum
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
    Private snakeHead As Image = RotateImage(Image.FromFile("head2.png"), 90) ' va memora capul sarpelui
    Private snakeHead1 As Image = Image.FromFile("head2.png")
    Private snakeBody1 As Image = Image.FromFile("body2.png")
    Private snakeBody As Image = RotateImage(Image.FromFile("body2.png"), 90) ' corpul sarpelui
    Private snakeTail As Image = RotateImage(Image.FromFile("tail.png"), 180)
    Private snakeTail1 As Image = Image.FromFile("tail.png")
    '    Private snakeTail As Brush = New SolidBrush(Color.DarkRed) 'coada sarpelui
    'Private bonus As Image = Image.FromFile("star1.ico")
    Private backBrush As Brush = New SolidBrush(Color.FromArgb(225, 255, 196)) ' pentru a acoperi la deplasarea sarpelui ultimul dreptunghi
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
    Dim p As Integer = 1
    Dim q As Integer = 1
    Dim r As Integer = 1
    Dim s As Integer = 1
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

        If IsNothing(d) Or Not d.ContainsKey(ft) Then
            Return Nothing
        End If
        ' returneaza imaginea
        Return d(ft)
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

        CurrentDirection = Direction.right   'directia de start a sarpelui in jos

        snakePoints = 0
        If check = "Again" Then
            snakeHead = RotateImage(snakeHead1, 90)
            snakeTail = RotateImage(snakeTail1, 90)
            snakeBody = RotateImage(snakeBody1, 90)
            nr = 0
        End If
        initRectangles()
        initSnake()
        selectRectangles()
        setFood()
        setPoints()
        tmr.Interval = 60
        tmr.Enabled = True


    End Sub

    Private Sub initRectangles()

        Dim i As Integer
        Dim j As Integer

        ReDim matrix(COLUMN_MAX, ROW_MAX)
        ReDim isSnake(COLUMN_MAX, ROW_MAX)
        For j = 0 To ROW_MAX
            For i = 0 To COLUMN_MAX
                matrix(i, j) = New Rectangle((i * 19) + 1, (j * 19) + 1, 20, 20) ' desenam suprafata de joc cu dreptunghiuri de dimensiunea 

                isSnake(i, j) = False
            Next
        Next



    End Sub


    Private Sub initSnake() 'initializarea sarpelui

        Dim x As Integer
        Dim y As Integer
        Dim i As Integer
        Dim bodySnake As strSnake
        snake = New Collection 'aici vom memora sarpele intr/o colectie de structuri

        x = 0
        y = 14
        For i = 1 To INITIAL_LENGHT
            bodySnake.square = matrix(x, y)
            bodySnake.x = x
            bodySnake.y = y
            snake.Add(bodySnake)
            y -= 1
        Next

        snakeLength = INITIAL_LENGHT
        snakeSpeed = 1000

    End Sub

    Private Sub selectRectangles()

        Dim g As Graphics = Graphics.FromImage(My.Resources.back4)
        Dim i As Integer
        Dim bodySnake As strSnake

        For i = 1 To INITIAL_LENGHT - 1
            bodySnake = snake(i)

            g.DrawImage(snakeBody, bodySnake.square)
            isSnake(bodySnake.x, bodySnake.y) = True
        Next
        bodySnake = snake(INITIAL_LENGHT)
        g.DrawImage(snakeTail, bodySnake.square)

        isSnake(bodySnake.x, bodySnake.y) = True

        background = New Bitmap(My.Resources.back4)

        g.Dispose()
        Refresh()

    End Sub



    Private Sub setFood()

        Randomize()
        Dim x As Integer
        Dim y As Integer
        Dim g As Graphics = Graphics.FromImage(background)

        x = CInt(Rnd() * 33) 'COLUMN_MAX
        Do While x > 33 Or isSnake(x, y) = True
            x = CInt(Rnd() * 33)
        Loop

        y = CInt(Rnd() * 24) 'ROW_MAX
        Do While y > 24 Or y < 2 Or isSnake(x, y) = True
            y = CInt(Rnd() * 24)
        Loop

        food = Matrix(x, y)  'pozitia mancarii 
        g.DrawImage(GetImageForFoodItem(GetFoodType()), food)
        Refresh()
        g.Dispose()

    End Sub
    

    Private Sub setPoints()
        status.Items(0).Text = "Score: " & CStr(snakePoints)
    End Sub

    Private Sub main_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        p = 1
        q = 1
        r = 1
        s = 1
        Select Case e.KeyCode
            Case Keys.Down
                If Not (CurrentDirection = Direction.down Or CurrentDirection = Direction.up) Then
                    CurrentDirection = Direction.down
                    snakeHead = snakeHead1
                    snakeHead = RotateImage(snakeHead, 180)
                    snakeBody = RotateImage(snakeBody1, 0)
                End If

            Case Keys.Left
                If Not (CurrentDirection = Direction.left Or CurrentDirection = Direction.right) Then
                    CurrentDirection = Direction.left
                    snakeHead = snakeHead1
                    snakeHead = RotateImage(snakeHead, 270)
                    snakeBody = RotateImage(snakeBody1, 90)
                End If

            Case Keys.Right
                If Not (CurrentDirection = Direction.right Or CurrentDirection = Direction.left) Then
                    CurrentDirection = Direction.right
                    snakeHead = snakeHead1
                    snakeHead = RotateImage(snakeHead, 90)
                    snakeBody = RotateImage(snakeBody1, 90)
                End If

            Case Keys.Up
                If Not (CurrentDirection = Direction.up Or CurrentDirection = Direction.down) Then
                    CurrentDirection = Direction.up
                    snakeHead = snakeHead1
                    snakeBody = RotateImage(snakeBody1, 0)
                End If
        End Select

    End Sub
    Private Sub tmr_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr.Tick

        moveSnake()
    End Sub

    Private Sub moveSnake()

        Dim bodySnake As strSnake
        Dim bodyS As strSnake
        Dim x As Integer
        Dim y As Integer
        Dim rect As Rectangle = New Rectangle()
        Dim g As Graphics = Graphics.FromImage(background)

        tmr.Enabled = False

        ' stergem ultimul dreptunghi
        bodySnake = snake(snake.Count)
        g.FillRectangle(backBrush, bodySnake.square)
        snake.Remove(snake.Count)
        isSnake(bodySnake.x, bodySnake.y) = False

        'plasarea initiala a cozii

        bodyS = snake.Item(snake.Count - 1)
        nr += 1
        If nr >= 14 Then
            bodySnake = snake(snake.Count)
            If bodyS.x < bodySnake.x And p = 1 Then
                If bodyS.x = 0 And bodySnake.x = 33 Then
                    snakeTail = RotateImage(snakeTail, 0)
                Else
                    snakeTail = RotateImage(snakeTail1, 270)
                    p = 0
                End If

            ElseIf bodyS.x > bodySnake.x And q = 1 Then
                If bodyS.x = 33 And bodySnake.x = 0 Then
                    snakeTail = RotateImage(snakeTail, 0)
                Else
                    snakeTail = RotateImage(snakeTail1, 90)
                    q = 0
                End If
            ElseIf bodyS.y > bodySnake.y And r = 1 Then
                If (bodyS.y = 24 And bodySnake.y = 1) Then
                    snakeTail = RotateImage(snakeTail, 0)
                Else
                    snakeTail = RotateImage(snakeTail1, 180)
                    r = 0
                End If
            ElseIf bodyS.y < bodySnake.y And s = 1 Then
                If (bodyS.y = 1 And bodySnake.y = 24) Then
                    snakeTail = RotateImage(snakeTail, 0)
                Else
                    snakeTail = RotateImage(snakeTail1, 0)
                    s = 0
                End If
            End If
            g.DrawImage(snakeTail, bodySnake.square)
        End If
        bodySnake = snake.Item(1)

        g.DrawImage(snakeBody, bodySnake.square)

        x = bodySnake.x
        y = bodySnake.y

        Select Case CurrentDirection
            Case Direction.down
                y = y + 1
                If type = "FREE" Then
                    If y > 24 Then
                        y = 1 'ROW_MAX
                    End If
                Else
                    If y > 24 Then
                        Dim r = MsgBox("             Game Over!" & vbCrLf & vbCrLf & " ~  ~  ~  Total Points: " & CStr(snakePoints) & "  ~ ~ ~" & vbCrLf & vbCrLf & "Start again?", MsgBoxStyle.YesNo)
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
                    If x < 0 Then
                        x = 33 'COLUMN_MAX
                    End If
                Else
                    If x < 0 Then
                        Dim r = MsgBox("             Game Over!" & vbCrLf & vbCrLf & " ~  ~  ~  Total Points: " & CStr(snakePoints) & "  ~ ~ ~" & vbCrLf & vbCrLf & "Start again?", MsgBoxStyle.YesNo)
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
                    If x > 33 Then
                        x = 0 'COLUMN_MAX
                    End If
                Else
                    If x > 33 Then
                        Dim r = MsgBox("             Game Over!" & vbCrLf & vbCrLf & " ~  ~  ~  Total Points: " & CStr(snakePoints) & "  ~ ~ ~" & vbCrLf & vbCrLf & "Start again?", MsgBoxStyle.YesNo)
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
                    If y - 1 < 0 Then
                        y = 24
                    End If
                Else
                    If y - 1 < 0 Then
                        Dim r = MsgBox("             Game Over!" & vbCrLf & vbCrLf & " ~  ~  ~  Total Points: " & CStr(snakePoints) & "  ~ ~ ~" & vbCrLf & vbCrLf & "Start again?", MsgBoxStyle.YesNo)
                        If r = vbYes Then
                            check = "Again"
                            initialize()
                            tmr.Enabled = True
                            Exit Sub

                        Else
                            Close()

                        End If
                    End If
                End If
        End Select

        If isSnake(x, y) = True Then
            tmr.Enabled = False

            If MessageBox.Show("             Game Over!" & vbCrLf & vbCrLf & " ~  ~  ~  Total Points: " & CStr(snakePoints) & "  ~ ~ ~" & vbCrLf & vbCrLf & "Start again?", "Snake", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                check = "Again"
                initialize()
                Exit Sub
            Else
                End
            End If

        End If


        rect = matrix(x, y)

        bodySnake.x = x
        bodySnake.y = y
        bodySnake.square = rect
        isSnake(x, y) = True

        'capul sarpelui
        g.DrawImage(snakeHead, bodySnake.square)
        Me.BackgroundImage = background

        ' Adaugam dreptunghiul cu cap la inceputul colectiei
        snake.Add(bodySnake, , 1)


        If matrix(x, y).Equals(CObj(food)) Then     ' cand pozitiile capului si ale hranei coincid am mancat un fruct

            My.Computer.Audio.Play(My.Resources.Eat, AudioPlayMode.Background)
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
            ' g.DrawImage(snakeTail, bodySnake.square)
            'g.FillRectangle(snakeTail, bodySnake.square)
            Me.BackgroundImage = background
            snake.Add(bodySnake, , , snake.Count)
            snakeLength = snake.Count
            If tmr.Interval - 2 > 0 Then tmr.Interval -= 1 'marim viteza 
            snakeSpeed = 100 + (((50 - tmr.Interval) / 50) * 100) 'calculam cu cat a crescut viteza
           


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
