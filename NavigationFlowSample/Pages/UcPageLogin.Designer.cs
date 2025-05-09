namespace NavigationFlowSample.Pages
{
    partial class UcPageLogin
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            txtUserId = new TextBox();
            btnCancel = new Button();
            btnNext = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtUserId
            // 
            txtUserId.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtUserId.Location = new Point(242, 186);
            txtUserId.Name = "txtUserId";
            txtUserId.Size = new Size(273, 36);
            txtUserId.TabIndex = 0;
            txtUserId.KeyDown += txtUserId_KeyDown;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnCancel.Location = new Point(0, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 44);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "キャンセル";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNext.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnNext.Location = new Point(641, 491);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(127, 44);
            btnNext.TabIndex = 5;
            btnNext.Text = "次へ";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Font = new Font("メイリオ", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(242, 225);
            label1.Name = "label1";
            label1.Size = new Size(273, 36);
            label1.TabIndex = 6;
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // UcPageLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(btnNext);
            Controls.Add(btnCancel);
            Controls.Add(txtUserId);
            Name = "UcPageLogin";
            Size = new Size(768, 535);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUserId;
        private Button btnCancel;
        private Button btnNext;
        private Label label1;
    }
}
