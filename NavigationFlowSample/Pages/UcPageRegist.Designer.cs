namespace NavigationFlowSample.Pages
{
    partial class UcPageRegist
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
            btnRegist = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // btnRegist
            // 
            btnRegist.Anchor = AnchorStyles.Bottom;
            btnRegist.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnRegist.Location = new Point(217, 490);
            btnRegist.Name = "btnRegist";
            btnRegist.Size = new Size(417, 64);
            btnRegist.TabIndex = 5;
            btnRegist.Text = "登録";
            btnRegist.UseVisualStyleBackColor = true;
            btnRegist.Click += btnComplete_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnCancel.Location = new Point(0, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 44);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "キャンセル";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // UcPageRegist
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnCancel);
            Controls.Add(btnRegist);
            Name = "UcPageRegist";
            Size = new Size(850, 569);
            ResumeLayout(false);
        }

        #endregion

        private Button btnRegist;
        private Button btnCancel;
    }
}
