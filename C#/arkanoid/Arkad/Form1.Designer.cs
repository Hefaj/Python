namespace Arkad
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.ScoreValue = new System.Windows.Forms.Label();
            this.Over = new System.Windows.Forms.PictureBox();
            this.Over_Score = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Over)).BeginInit();
            this.SuspendLayout();
            // 
            // Timer
            // 
            this.Timer.Interval = 10;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Canvas.Location = new System.Drawing.Point(13, 12);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(856, 436);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            // 
            // ScoreValue
            // 
            this.ScoreValue.AutoSize = true;
            this.ScoreValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ScoreValue.Location = new System.Drawing.Point(419, 9);
            this.ScoreValue.Name = "ScoreValue";
            this.ScoreValue.Size = new System.Drawing.Size(86, 25);
            this.ScoreValue.TabIndex = 1;
            this.ScoreValue.Text = "SCORE";
            // 
            // Over
            // 
            this.Over.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Over.Location = new System.Drawing.Point(182, 90);
            this.Over.Name = "Over";
            this.Over.Size = new System.Drawing.Size(558, 276);
            this.Over.TabIndex = 2;
            this.Over.TabStop = false;
            this.Over.Visible = false;
            // 
            // Over_Score
            // 
            this.Over_Score.AutoSize = true;
            this.Over_Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Over_Score.Location = new System.Drawing.Point(208, 128);
            this.Over_Score.Name = "Over_Score";
            this.Over_Score.Size = new System.Drawing.Size(132, 25);
            this.Over_Score.TabIndex = 3;
            this.Over_Score.Text = "Your Score: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(817, 422);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "P - Start\r\nR - Reset";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 460);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Over_Score);
            this.Controls.Add(this.Over);
            this.Controls.Add(this.ScoreValue);
            this.Controls.Add(this.Canvas);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Over)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.Label ScoreValue;
        private System.Windows.Forms.PictureBox Over;
        private System.Windows.Forms.Label Over_Score;
        private System.Windows.Forms.Label label1;
    }
}

