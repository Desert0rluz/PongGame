namespace PongGame
{
    public partial class Form1 : Form
    {

        private int player1Y = 150;  // Raquete do jogador 1 
        private int player2Y = 150;  // Raquete do jogador 2 

        private int ballX = 400;
        private int ballY = 225;
        private int ballSpeedX = 5;  // Velocidade horizontal da bola
        private int ballSpeedY = 5;  // Velocidade vertical da bola

        private int player1Score = 0;
        private int player2Score = 0;

        // Dimensões das raquetes e da bola
        private int racketWidth = 10;
        private int racketHeight = 60;
        private int ballSize = 15;
        public Form1()
        {
            InitializeComponent();
            gameTimer.Tick += new EventHandler(GameTick);
            gameTimer.Start();
        }

        private void GameTick(object sender, EventArgs e)
        {
            MoveBall();
            CheckCollision();
            Invalidate();
        }
        private void MoveBall()
        {
            ballX += ballSpeedX;
            ballY += ballSpeedY;

            if (ballY <= 0 || ballY + ballSize >= this.ClientSize.Height)
            {
                ballSpeedY = -ballSpeedY; 
            }
        }

        private void CheckCollision()
        {
            if (ballX <= 40 && ballY + ballSize >= player1Y && ballY <= player1Y + racketHeight)
            {
                ballSpeedX = -ballSpeedX; 
            }

            if (ballX + ballSize >= this.ClientSize.Width - 40 && ballY + ballSize >= player2Y && ballY <= player2Y + racketHeight)
            {
                ballSpeedX = -ballSpeedX;
            }

            if (ballX <= 0)
            {
                player2Score++;
                ResetBall();
            }
            else if (ballX + ballSize >= this.ClientSize.Width)
            {
                player1Score++;
                ResetBall();
            }
        }

        private void ResetBall()
        {
            ballX = this.ClientSize.Width / 2;
            ballY = this.ClientSize.Height / 2;

            ballSpeedX = -ballSpeedX;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.FillRectangle(Brushes.Blue, 30, player1Y, racketWidth, racketHeight);
            g.FillRectangle(Brushes.Red, this.ClientSize.Width - 40, player2Y, racketWidth, racketHeight);


            g.FillEllipse(Brushes.Black, ballX, ballY, ballSize, ballSize);


            using (Font scoreFont = new Font("Arial", 12))
            {
                g.DrawString($"Jogador 1: {player1Score}", scoreFont, Brushes.Blue, 50, 20);
                g.DrawString($"Jogador 2: {player2Score}", scoreFont, Brushes.Red, this.ClientSize.Width - 150, 20);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.W:
                    if (player1Y > 0) player1Y -= 10;
                    break;
                case Keys.S:
                    if (player1Y + racketHeight < this.ClientSize.Height) player1Y += 10;
                    break;
                case Keys.Up:
                    if (player2Y > 0) player2Y -= 10;
                    break;
                case Keys.Down:
                    if (player2Y + racketHeight < this.ClientSize.Height) player2Y += 10;
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
