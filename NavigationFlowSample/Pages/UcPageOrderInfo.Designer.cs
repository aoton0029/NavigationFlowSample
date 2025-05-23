﻿namespace NavigationFlowSample.Pages
{
    partial class UcPageOrderInfo
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
            btnCancel = new Button();
            btnBack = new Button();
            btnNext = new Button();
            lblProductName = new Label();
            txtOrderSheetNo = new TextBox();
            label2 = new Label();
            SuspendLayout();
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
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBack.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnBack.Location = new Point(0, 471);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(127, 44);
            btnBack.TabIndex = 4;
            btnBack.Text = "戻る";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNext.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnNext.Location = new Point(672, 471);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(127, 44);
            btnNext.TabIndex = 5;
            btnNext.Text = "次へ";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // lblProductName
            // 
            lblProductName.BorderStyle = BorderStyle.FixedSingle;
            lblProductName.Font = new Font("メイリオ", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lblProductName.Location = new Point(267, 201);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(273, 36);
            lblProductName.TabIndex = 8;
            lblProductName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtOrderSheetNo
            // 
            txtOrderSheetNo.Font = new Font("メイリオ", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            txtOrderSheetNo.Location = new Point(267, 162);
            txtOrderSheetNo.Name = "txtOrderSheetNo";
            txtOrderSheetNo.Size = new Size(273, 36);
            txtOrderSheetNo.TabIndex = 7;
            txtOrderSheetNo.KeyDown += txtOrderSheetNo_KeyDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(158, 16);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 9;
            label2.Text = "受注情報";
            // 
            // UcPageOrderInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(lblProductName);
            Controls.Add(txtOrderSheetNo);
            Controls.Add(btnNext);
            Controls.Add(btnBack);
            Controls.Add(btnCancel);
            Name = "UcPageOrderInfo";
            Size = new Size(799, 515);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnBack;
        private Button btnNext;
        private Label lblProductName;
        private TextBox txtOrderSheetNo;
        private Label label2;
    }
}
