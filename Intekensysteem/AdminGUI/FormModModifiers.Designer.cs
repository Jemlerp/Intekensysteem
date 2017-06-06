namespace AdminGUI
{
    partial class FormModModifiers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.checkBoxZo = new System.Windows.Forms.CheckBox();
            this.checkBoxZa = new System.Windows.Forms.CheckBox();
            this.checkBoxWo = new System.Windows.Forms.CheckBox();
            this.buttonSaveNew = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxOpmerking = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxVr = new System.Windows.Forms.CheckBox();
            this.checkBoxDi = new System.Windows.Forms.CheckBox();
            this.checkBoxDo = new System.Windows.Forms.CheckBox();
            this.checkBoxMo = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerDatumTotEnMet = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerVanafEnMet = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerHoursToAdd = new System.Windows.Forms.DateTimePicker();
            this.checkBoxIsExcurtie = new System.Windows.Forms.CheckBox();
            this.checkBoxIsFlexiebelverlof = new System.Windows.Forms.CheckBox();
            this.checkBoxIsStudiVerlof = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewModEntrys = new System.Windows.Forms.DataGridView();
            this.dataGridViewSelecetedUsers = new System.Windows.Forms.DataGridView();
            this.checkBoxShowExUsers = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModEntrys)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelecetedUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxZo
            // 
            this.checkBoxZo.AutoSize = true;
            this.checkBoxZo.Location = new System.Drawing.Point(724, 463);
            this.checkBoxZo.Name = "checkBoxZo";
            this.checkBoxZo.Size = new System.Drawing.Size(39, 17);
            this.checkBoxZo.TabIndex = 139;
            this.checkBoxZo.Text = "Zo";
            this.checkBoxZo.UseVisualStyleBackColor = true;
            // 
            // checkBoxZa
            // 
            this.checkBoxZa.AutoSize = true;
            this.checkBoxZa.Location = new System.Drawing.Point(724, 440);
            this.checkBoxZa.Name = "checkBoxZa";
            this.checkBoxZa.Size = new System.Drawing.Size(39, 17);
            this.checkBoxZa.TabIndex = 138;
            this.checkBoxZa.Text = "Za";
            this.checkBoxZa.UseVisualStyleBackColor = true;
            // 
            // checkBoxWo
            // 
            this.checkBoxWo.AutoSize = true;
            this.checkBoxWo.Location = new System.Drawing.Point(724, 371);
            this.checkBoxWo.Name = "checkBoxWo";
            this.checkBoxWo.Size = new System.Drawing.Size(43, 17);
            this.checkBoxWo.TabIndex = 137;
            this.checkBoxWo.Text = "Wo";
            this.checkBoxWo.UseVisualStyleBackColor = true;
            // 
            // buttonSaveNew
            // 
            this.buttonSaveNew.Location = new System.Drawing.Point(488, 619);
            this.buttonSaveNew.Name = "buttonSaveNew";
            this.buttonSaveNew.Size = new System.Drawing.Size(134, 52);
            this.buttonSaveNew.TabIndex = 136;
            this.buttonSaveNew.Text = "SAVE NEW";
            this.buttonSaveNew.UseVisualStyleBackColor = true;
            this.buttonSaveNew.Click += new System.EventHandler(this.buttonSaveNew_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Enabled = false;
            this.buttonUpdate.Location = new System.Drawing.Point(676, 619);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(127, 52);
            this.buttonUpdate.TabIndex = 135;
            this.buttonUpdate.Text = "UPDATE ENTRY";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(468, 9);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(107, 33);
            this.buttonDelete.TabIndex = 134;
            this.buttonDelete.Text = "DELETE ENTRY";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.AllowUserToAddRows = false;
            this.dataGridViewUsers.AllowUserToDeleteRows = false;
            this.dataGridViewUsers.AllowUserToResizeColumns = false;
            this.dataGridViewUsers.AllowUserToResizeRows = false;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridViewUsers.Location = new System.Drawing.Point(830, 0);
            this.dataGridViewUsers.MultiSelect = false;
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.ReadOnly = true;
            this.dataGridViewUsers.RowHeadersVisible = false;
            this.dataGridViewUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUsers.ShowCellErrors = false;
            this.dataGridViewUsers.ShowCellToolTips = false;
            this.dataGridViewUsers.Size = new System.Drawing.Size(391, 782);
            this.dataGridViewUsers.TabIndex = 110;
            this.dataGridViewUsers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUsers_CellDoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(500, 506);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 132;
            this.label7.Text = "Opmerking";
            // 
            // textBoxOpmerking
            // 
            this.textBoxOpmerking.Location = new System.Drawing.Point(501, 522);
            this.textBoxOpmerking.Multiline = true;
            this.textBoxOpmerking.Name = "textBoxOpmerking";
            this.textBoxOpmerking.Size = new System.Drawing.Size(176, 74);
            this.textBoxOpmerking.TabIndex = 131;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(721, 309);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 130;
            this.label6.Text = "Effective On ";
            // 
            // checkBoxVr
            // 
            this.checkBoxVr.AutoSize = true;
            this.checkBoxVr.Location = new System.Drawing.Point(724, 417);
            this.checkBoxVr.Name = "checkBoxVr";
            this.checkBoxVr.Size = new System.Drawing.Size(36, 17);
            this.checkBoxVr.TabIndex = 129;
            this.checkBoxVr.Text = "Vr";
            this.checkBoxVr.UseVisualStyleBackColor = true;
            // 
            // checkBoxDi
            // 
            this.checkBoxDi.AutoSize = true;
            this.checkBoxDi.Location = new System.Drawing.Point(724, 348);
            this.checkBoxDi.Name = "checkBoxDi";
            this.checkBoxDi.Size = new System.Drawing.Size(36, 17);
            this.checkBoxDi.TabIndex = 128;
            this.checkBoxDi.Text = "Di";
            this.checkBoxDi.UseVisualStyleBackColor = true;
            // 
            // checkBoxDo
            // 
            this.checkBoxDo.AutoSize = true;
            this.checkBoxDo.Location = new System.Drawing.Point(724, 394);
            this.checkBoxDo.Name = "checkBoxDo";
            this.checkBoxDo.Size = new System.Drawing.Size(40, 17);
            this.checkBoxDo.TabIndex = 127;
            this.checkBoxDo.Text = "Do";
            this.checkBoxDo.UseVisualStyleBackColor = true;
            // 
            // checkBoxMo
            // 
            this.checkBoxMo.AutoSize = true;
            this.checkBoxMo.Location = new System.Drawing.Point(724, 325);
            this.checkBoxMo.Name = "checkBoxMo";
            this.checkBoxMo.Size = new System.Drawing.Size(41, 17);
            this.checkBoxMo.TabIndex = 126;
            this.checkBoxMo.Text = "Mo";
            this.checkBoxMo.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(499, 368);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 125;
            this.label5.Text = "Datum Tot en met";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(499, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 124;
            this.label4.Text = "Datum Van - en met";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(502, 440);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 123;
            this.label3.Text = "Hours To Add";
            // 
            // dateTimePickerDatumTotEnMet
            // 
            this.dateTimePickerDatumTotEnMet.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerDatumTotEnMet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerDatumTotEnMet.Location = new System.Drawing.Point(498, 384);
            this.dateTimePickerDatumTotEnMet.MaxDate = new System.DateTime(4200, 4, 20, 0, 0, 0, 0);
            this.dateTimePickerDatumTotEnMet.MinDate = new System.DateTime(2000, 2, 19, 0, 0, 0, 0);
            this.dateTimePickerDatumTotEnMet.Name = "dateTimePickerDatumTotEnMet";
            this.dateTimePickerDatumTotEnMet.Size = new System.Drawing.Size(201, 29);
            this.dateTimePickerDatumTotEnMet.TabIndex = 122;
            // 
            // dateTimePickerVanafEnMet
            // 
            this.dateTimePickerVanafEnMet.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerVanafEnMet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerVanafEnMet.Location = new System.Drawing.Point(498, 325);
            this.dateTimePickerVanafEnMet.MaxDate = new System.DateTime(4200, 4, 20, 0, 0, 0, 0);
            this.dateTimePickerVanafEnMet.MinDate = new System.DateTime(2000, 2, 19, 0, 0, 0, 0);
            this.dateTimePickerVanafEnMet.Name = "dateTimePickerVanafEnMet";
            this.dateTimePickerVanafEnMet.Size = new System.Drawing.Size(201, 29);
            this.dateTimePickerVanafEnMet.TabIndex = 121;
            // 
            // dateTimePickerHoursToAdd
            // 
            this.dateTimePickerHoursToAdd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerHoursToAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerHoursToAdd.Location = new System.Drawing.Point(498, 457);
            this.dateTimePickerHoursToAdd.MaxDate = new System.DateTime(4200, 4, 20, 0, 0, 0, 0);
            this.dateTimePickerHoursToAdd.MinDate = new System.DateTime(2000, 2, 19, 0, 0, 0, 0);
            this.dateTimePickerHoursToAdd.Name = "dateTimePickerHoursToAdd";
            this.dateTimePickerHoursToAdd.Size = new System.Drawing.Size(201, 29);
            this.dateTimePickerHoursToAdd.TabIndex = 120;
            // 
            // checkBoxIsExcurtie
            // 
            this.checkBoxIsExcurtie.AutoSize = true;
            this.checkBoxIsExcurtie.Location = new System.Drawing.Point(727, 560);
            this.checkBoxIsExcurtie.Name = "checkBoxIsExcurtie";
            this.checkBoxIsExcurtie.Size = new System.Drawing.Size(68, 17);
            this.checkBoxIsExcurtie.TabIndex = 119;
            this.checkBoxIsExcurtie.Text = "IsExursie";
            this.checkBoxIsExcurtie.UseVisualStyleBackColor = true;
            this.checkBoxIsExcurtie.CheckedChanged += new System.EventHandler(this.checkcahnged);
            // 
            // checkBoxIsFlexiebelverlof
            // 
            this.checkBoxIsFlexiebelverlof.AutoSize = true;
            this.checkBoxIsFlexiebelverlof.Location = new System.Drawing.Point(727, 583);
            this.checkBoxIsFlexiebelverlof.Name = "checkBoxIsFlexiebelverlof";
            this.checkBoxIsFlexiebelverlof.Size = new System.Drawing.Size(102, 17);
            this.checkBoxIsFlexiebelverlof.TabIndex = 118;
            this.checkBoxIsFlexiebelverlof.Text = "IsFlexiebelVerlof";
            this.checkBoxIsFlexiebelverlof.UseVisualStyleBackColor = true;
            this.checkBoxIsFlexiebelverlof.CheckedChanged += new System.EventHandler(this.checkcahnged);
            // 
            // checkBoxIsStudiVerlof
            // 
            this.checkBoxIsStudiVerlof.AutoSize = true;
            this.checkBoxIsStudiVerlof.Location = new System.Drawing.Point(727, 537);
            this.checkBoxIsStudiVerlof.Name = "checkBoxIsStudiVerlof";
            this.checkBoxIsStudiVerlof.Size = new System.Drawing.Size(90, 17);
            this.checkBoxIsStudiVerlof.TabIndex = 117;
            this.checkBoxIsStudiVerlof.Text = "IsStudieverlof";
            this.checkBoxIsStudiVerlof.UseVisualStyleBackColor = true;
            this.checkBoxIsStudiVerlof.CheckedChanged += new System.EventHandler(this.checkcahnged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(700, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 116;
            this.label2.Text = "Search";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(703, 22);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(100, 20);
            this.textBoxSearch.TabIndex = 115;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(498, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 114;
            this.label1.Text = "Selected users";
            // 
            // dataGridViewModEntrys
            // 
            this.dataGridViewModEntrys.AllowUserToAddRows = false;
            this.dataGridViewModEntrys.AllowUserToDeleteRows = false;
            this.dataGridViewModEntrys.AllowUserToResizeColumns = false;
            this.dataGridViewModEntrys.AllowUserToResizeRows = false;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewModEntrys.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridViewModEntrys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewModEntrys.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridViewModEntrys.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewModEntrys.MultiSelect = false;
            this.dataGridViewModEntrys.Name = "dataGridViewModEntrys";
            this.dataGridViewModEntrys.ReadOnly = true;
            this.dataGridViewModEntrys.RowHeadersVisible = false;
            this.dataGridViewModEntrys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewModEntrys.ShowCellErrors = false;
            this.dataGridViewModEntrys.ShowCellToolTips = false;
            this.dataGridViewModEntrys.Size = new System.Drawing.Size(462, 782);
            this.dataGridViewModEntrys.TabIndex = 113;
            this.dataGridViewModEntrys.SelectionChanged += new System.EventHandler(this.dataGridViewModEntrys_SelectionChanged);
            // 
            // dataGridViewSelecetedUsers
            // 
            this.dataGridViewSelecetedUsers.AllowUserToAddRows = false;
            this.dataGridViewSelecetedUsers.AllowUserToDeleteRows = false;
            this.dataGridViewSelecetedUsers.AllowUserToResizeColumns = false;
            this.dataGridViewSelecetedUsers.AllowUserToResizeRows = false;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSelecetedUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridViewSelecetedUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelecetedUsers.Location = new System.Drawing.Point(498, 80);
            this.dataGridViewSelecetedUsers.MultiSelect = false;
            this.dataGridViewSelecetedUsers.Name = "dataGridViewSelecetedUsers";
            this.dataGridViewSelecetedUsers.ReadOnly = true;
            this.dataGridViewSelecetedUsers.RowHeadersVisible = false;
            this.dataGridViewSelecetedUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSelecetedUsers.ShowCellErrors = false;
            this.dataGridViewSelecetedUsers.ShowCellToolTips = false;
            this.dataGridViewSelecetedUsers.Size = new System.Drawing.Size(281, 220);
            this.dataGridViewSelecetedUsers.TabIndex = 112;
            this.dataGridViewSelecetedUsers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSelecetedUsers_CellDoubleClick);
            // 
            // checkBoxShowExUsers
            // 
            this.checkBoxShowExUsers.AutoSize = true;
            this.checkBoxShowExUsers.Location = new System.Drawing.Point(703, 48);
            this.checkBoxShowExUsers.Name = "checkBoxShowExUsers";
            this.checkBoxShowExUsers.Size = new System.Drawing.Size(107, 17);
            this.checkBoxShowExUsers.TabIndex = 111;
            this.checkBoxShowExUsers.Text = "ShowExStudents";
            this.checkBoxShowExUsers.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(716, 521);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 140;
            this.label9.Text = "<Volle School Dag>";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(581, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 141;
            this.button1.Text = "Reload";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormModModifiers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 782);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.checkBoxZo);
            this.Controls.Add(this.checkBoxZa);
            this.Controls.Add(this.checkBoxWo);
            this.Controls.Add(this.buttonSaveNew);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.dataGridViewUsers);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxOpmerking);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBoxVr);
            this.Controls.Add(this.checkBoxDi);
            this.Controls.Add(this.checkBoxDo);
            this.Controls.Add(this.checkBoxMo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePickerDatumTotEnMet);
            this.Controls.Add(this.dateTimePickerVanafEnMet);
            this.Controls.Add(this.dateTimePickerHoursToAdd);
            this.Controls.Add(this.checkBoxIsExcurtie);
            this.Controls.Add(this.checkBoxIsFlexiebelverlof);
            this.Controls.Add(this.checkBoxIsStudiVerlof);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewModEntrys);
            this.Controls.Add(this.dataGridViewSelecetedUsers);
            this.Controls.Add(this.checkBoxShowExUsers);
            this.Name = "FormModModifiers";
            this.Text = "FormModModifiers";
            this.Load += new System.EventHandler(this.FormModModifiers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModEntrys)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelecetedUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxZo;
        private System.Windows.Forms.CheckBox checkBoxZa;
        private System.Windows.Forms.CheckBox checkBoxWo;
        private System.Windows.Forms.Button buttonSaveNew;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxOpmerking;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxVr;
        private System.Windows.Forms.CheckBox checkBoxDi;
        private System.Windows.Forms.CheckBox checkBoxDo;
        private System.Windows.Forms.CheckBox checkBoxMo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerDatumTotEnMet;
        private System.Windows.Forms.DateTimePicker dateTimePickerVanafEnMet;
        private System.Windows.Forms.DateTimePicker dateTimePickerHoursToAdd;
        private System.Windows.Forms.CheckBox checkBoxIsExcurtie;
        private System.Windows.Forms.CheckBox checkBoxIsFlexiebelverlof;
        private System.Windows.Forms.CheckBox checkBoxIsStudiVerlof;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewModEntrys;
        private System.Windows.Forms.DataGridView dataGridViewSelecetedUsers;
        private System.Windows.Forms.CheckBox checkBoxShowExUsers;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
    }
}