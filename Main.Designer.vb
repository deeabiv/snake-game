<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.tmr = New System.Windows.Forms.Timer(Me.components)
        Me.status = New System.Windows.Forms.StatusStrip()
        Me.tss0 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tss1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tss2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssSnakeLength = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssSnakeSpeed = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tss3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewGameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BlocToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WallColisionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PauseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.status.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tmr
        '
        '
        'status
        '
        Me.status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tss0, Me.tss1, Me.tss2, Me.tssSnakeLength, Me.tssSnakeSpeed, Me.tss3})
        Me.status.Location = New System.Drawing.Point(0, 488)
        Me.status.Name = "status"
        Me.status.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.status.Size = New System.Drawing.Size(661, 22)
        Me.status.TabIndex = 0
        Me.status.Text = "StatusStrip1"
        '
        'tss0
        '
        Me.tss0.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.tss0.Name = "tss0"
        Me.tss0.Size = New System.Drawing.Size(66, 17)
        Me.tss0.Text = "Screen Size:"
        Me.tss0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tss0.Visible = False
        '
        'tss1
        '
        Me.tss1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.tss1.Name = "tss1"
        Me.tss1.Size = New System.Drawing.Size(83, 17)
        Me.tss1.Text = "Token Location:"
        Me.tss1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tss1.Visible = False
        '
        'tss2
        '
        Me.tss2.BorderStyle = System.Windows.Forms.Border3DStyle.Raised
        Me.tss2.Name = "tss2"
        Me.tss2.Size = New System.Drawing.Size(83, 17)
        Me.tss2.Text = "Snake Location:"
        Me.tss2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tss2.Visible = False
        '
        'tssSnakeLength
        '
        Me.tssSnakeLength.BorderStyle = System.Windows.Forms.Border3DStyle.Raised
        Me.tssSnakeLength.Name = "tssSnakeLength"
        Me.tssSnakeLength.Size = New System.Drawing.Size(76, 17)
        Me.tssSnakeLength.Text = "Snake Length:"
        Me.tssSnakeLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tssSnakeSpeed
        '
        Me.tssSnakeSpeed.BorderStyle = System.Windows.Forms.Border3DStyle.Raised
        Me.tssSnakeSpeed.Name = "tssSnakeSpeed"
        Me.tssSnakeSpeed.Size = New System.Drawing.Size(73, 17)
        Me.tssSnakeSpeed.Text = "Snake Speed:"
        Me.tssSnakeSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tss3
        '
        Me.tss3.BorderStyle = System.Windows.Forms.Border3DStyle.Raised
        Me.tss3.Name = "tss3"
        Me.tss3.Size = New System.Drawing.Size(497, 17)
        Me.tss3.Spring = True
        Me.tss3.Text = "Total Points:"
        Me.tss3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.White
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.AboutToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip1.Size = New System.Drawing.Size(661, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewGameToolStripMenuItem, Me.ExitToolStripMenuItem, Me.PauseToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewGameToolStripMenuItem
        '
        Me.NewGameToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BlocToolStripMenuItem, Me.WallColisionToolStripMenuItem})
        Me.NewGameToolStripMenuItem.Name = "NewGameToolStripMenuItem"
        Me.NewGameToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.NewGameToolStripMenuItem.Text = "New Game"
        '
        'BlocToolStripMenuItem
        '
        Me.BlocToolStripMenuItem.Name = "BlocToolStripMenuItem"
        Me.BlocToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.BlocToolStripMenuItem.Text = "Free"
        '
        'WallColisionToolStripMenuItem
        '
        Me.WallColisionToolStripMenuItem.Name = "WallColisionToolStripMenuItem"
        Me.WallColisionToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.WallColisionToolStripMenuItem.Text = "Wall Colision"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'PauseToolStripMenuItem
        '
        Me.PauseToolStripMenuItem.Name = "PauseToolStripMenuItem"
        Me.PauseToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.PauseToolStripMenuItem.Text = "Pause"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.snake.My.Resources.Resources.rsz_start1
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(661, 510)
        Me.Controls.Add(Me.status)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(669, 537)
        Me.MinimumSize = New System.Drawing.Size(669, 537)
        Me.Name = "main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Snake"
        Me.status.ResumeLayout(False)
        Me.status.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tmr As System.Windows.Forms.Timer
    Friend WithEvents status As System.Windows.Forms.StatusStrip
    Friend WithEvents tss1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tss0 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tss2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tss3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssSnakeLength As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssSnakeSpeed As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewGameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PauseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BlocToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WallColisionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
