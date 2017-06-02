namespace AdminGUI
{
    partial class FormModUsers
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonDeleteUser = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerNewDateLeft = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerNewDateJoined = new System.Windows.Forms.DateTimePicker();
            this.checkBoxNewZitNogOpSchool = new System.Windows.Forms.CheckBox();
            this.textBoxNewNfcid = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonSaveNew = new System.Windows.Forms.Button();
            this.textBoxNewAchternaam = new System.Windows.Forms.TextBox();
            this.textBoxNewvoornaam = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerUpdateDateLeft = new System.Windows.Forms.DateTimePicker();
            this.checkBoxUpdateZitNogOpSchool = new System.Windows.Forms.CheckBox();
            this.connectRederToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectReaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateTimePickerUpdateDateJoined = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.textBoxUpdateID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxUpdateNFCID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonUopdate = new System.Windows.Forms.Button();
            this.textBoxUpdateAchternaam = new System.Windows.Forms.TextBox();
            this.textBoxUpdateVoornaam = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(927, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 140;
            this.label6.Text = "Search ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(826, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 139;
            // 
            // buttonDeleteUser
            // 
            this.buttonDeleteUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteUser.Location = new System.Drawing.Point(930, 306);
            this.buttonDeleteUser.Name = "buttonDeleteUser";
            this.buttonDeleteUser.Size = new System.Drawing.Size(123, 33);
            this.buttonDeleteUser.TabIndex = 138;
            this.buttonDeleteUser.Text = "Delete User";
            this.buttonDeleteUser.UseVisualStyleBackColor = true;
            this.buttonDeleteUser.Click += new System.EventHandler(this.buttonDeleteUser_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(945, 566);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 137;
            this.label3.Text = "DateLeft";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(945, 531);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 20);
            this.label4.TabIndex = 136;
            this.label4.Text = "DateJoined";
            // 
            // dateTimePickerNewDateLeft
            // 
            this.dateTimePickerNewDateLeft.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerNewDateLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerNewDateLeft.Location = new System.Drawing.Point(738, 559);
            this.dateTimePickerNewDateLeft.MaxDate = new System.DateTime(4200, 4, 20, 0, 0, 0, 0);
            this.dateTimePickerNewDateLeft.MinDate = new System.DateTime(2000, 2, 19, 0, 0, 0, 0);
            this.dateTimePickerNewDateLeft.Name = "dateTimePickerNewDateLeft";
            this.dateTimePickerNewDateLeft.Size = new System.Drawing.Size(201, 26);
            this.dateTimePickerNewDateLeft.TabIndex = 135;
            // 
            // dateTimePickerNewDateJoined
            // 
            this.dateTimePickerNewDateJoined.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerNewDateJoined.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerNewDateJoined.Location = new System.Drawing.Point(738, 524);
            this.dateTimePickerNewDateJoined.MaxDate = new System.DateTime(4200, 4, 20, 0, 0, 0, 0);
            this.dateTimePickerNewDateJoined.MinDate = new System.DateTime(2000, 2, 19, 0, 0, 0, 0);
            this.dateTimePickerNewDateJoined.Name = "dateTimePickerNewDateJoined";
            this.dateTimePickerNewDateJoined.Size = new System.Drawing.Size(201, 26);
            this.dateTimePickerNewDateJoined.TabIndex = 134;
            // 
            // checkBoxNewZitNogOpSchool
            // 
            this.checkBoxNewZitNogOpSchool.AutoSize = true;
            this.checkBoxNewZitNogOpSchool.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNewZitNogOpSchool.Location = new System.Drawing.Point(738, 494);
            this.checkBoxNewZitNogOpSchool.Name = "checkBoxNewZitNogOpSchool";
            this.checkBoxNewZitNogOpSchool.Size = new System.Drawing.Size(145, 24);
            this.checkBoxNewZitNogOpSchool.TabIndex = 133;
            this.checkBoxNewZitNogOpSchool.Text = "ZitNogOpSchool";
            this.checkBoxNewZitNogOpSchool.UseVisualStyleBackColor = true;
            this.checkBoxNewZitNogOpSchool.CheckedChanged += new System.EventHandler(this.checkBoxNewZitNogOpSchool_CheckedChanged);
            // 
            // textBoxNewNfcid
            // 
            this.textBoxNewNfcid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNewNfcid.Location = new System.Drawing.Point(738, 461);
            this.textBoxNewNfcid.Name = "textBoxNewNfcid";
            this.textBoxNewNfcid.Size = new System.Drawing.Size(124, 26);
            this.textBoxNewNfcid.TabIndex = 132;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(868, 461);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 20);
            this.label10.TabIndex = 131;
            this.label10.Text = "NFCID";
            // 
            // buttonSaveNew
            // 
            this.buttonSaveNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveNew.Location = new System.Drawing.Point(738, 604);
            this.buttonSaveNew.Name = "buttonSaveNew";
            this.buttonSaveNew.Size = new System.Drawing.Size(87, 33);
            this.buttonSaveNew.TabIndex = 130;
            this.buttonSaveNew.Text = "Add New";
            this.buttonSaveNew.UseVisualStyleBackColor = true;
            this.buttonSaveNew.Click += new System.EventHandler(this.buttonSaveNew_Click);
            // 
            // textBoxNewAchternaam
            // 
            this.textBoxNewAchternaam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNewAchternaam.Location = new System.Drawing.Point(738, 429);
            this.textBoxNewAchternaam.Name = "textBoxNewAchternaam";
            this.textBoxNewAchternaam.Size = new System.Drawing.Size(124, 26);
            this.textBoxNewAchternaam.TabIndex = 129;
            // 
            // textBoxNewvoornaam
            // 
            this.textBoxNewvoornaam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNewvoornaam.Location = new System.Drawing.Point(738, 397);
            this.textBoxNewvoornaam.Name = "textBoxNewvoornaam";
            this.textBoxNewvoornaam.Size = new System.Drawing.Size(124, 26);
            this.textBoxNewvoornaam.TabIndex = 128;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(868, 397);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 20);
            this.label11.TabIndex = 127;
            this.label11.Text = "Voornaam";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(868, 429);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 20);
            this.label12.TabIndex = 126;
            this.label12.Text = "Achternaam";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(945, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 125;
            this.label2.Text = "DateLeft";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(945, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 124;
            this.label1.Text = "DateJoined";
            // 
            // dateTimePickerUpdateDateLeft
            // 
            this.dateTimePickerUpdateDateLeft.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerUpdateDateLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerUpdateDateLeft.Location = new System.Drawing.Point(738, 261);
            this.dateTimePickerUpdateDateLeft.MaxDate = new System.DateTime(4200, 4, 20, 0, 0, 0, 0);
            this.dateTimePickerUpdateDateLeft.MinDate = new System.DateTime(2000, 2, 19, 0, 0, 0, 0);
            this.dateTimePickerUpdateDateLeft.Name = "dateTimePickerUpdateDateLeft";
            this.dateTimePickerUpdateDateLeft.Size = new System.Drawing.Size(201, 26);
            this.dateTimePickerUpdateDateLeft.TabIndex = 123;
            // 
            // checkBoxUpdateZitNogOpSchool
            // 
            this.checkBoxUpdateZitNogOpSchool.AutoSize = true;
            this.checkBoxUpdateZitNogOpSchool.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxUpdateZitNogOpSchool.Location = new System.Drawing.Point(738, 196);
            this.checkBoxUpdateZitNogOpSchool.Name = "checkBoxUpdateZitNogOpSchool";
            this.checkBoxUpdateZitNogOpSchool.Size = new System.Drawing.Size(145, 24);
            this.checkBoxUpdateZitNogOpSchool.TabIndex = 121;
            this.checkBoxUpdateZitNogOpSchool.Text = "ZitNogOpSchool";
            this.checkBoxUpdateZitNogOpSchool.UseVisualStyleBackColor = true;
            this.checkBoxUpdateZitNogOpSchool.CheckedChanged += new System.EventHandler(this.checkBoxUpdateZitNogOpSchool_CheckedChanged);
            // 
            // connectRederToolStripMenuItem
            // 
            this.connectRederToolStripMenuItem.Name = "connectRederToolStripMenuItem";
            this.connectRederToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.connectRederToolStripMenuItem.Text = "Connect NFC Reader";
            this.connectRederToolStripMenuItem.Click += new System.EventHandler(this.connectRederToolStripMenuItem_Click);
            // 
            // connectReaderToolStripMenuItem
            // 
            this.connectReaderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectRederToolStripMenuItem});
            this.connectReaderToolStripMenuItem.Name = "connectReaderToolStripMenuItem";
            this.connectReaderToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.connectReaderToolStripMenuItem.Text = "Device";
            // 
            // dateTimePickerUpdateDateJoined
            // 
            this.dateTimePickerUpdateDateJoined.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerUpdateDateJoined.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerUpdateDateJoined.Location = new System.Drawing.Point(738, 226);
            this.dateTimePickerUpdateDateJoined.MaxDate = new System.DateTime(4200, 4, 20, 0, 0, 0, 0);
            this.dateTimePickerUpdateDateJoined.MinDate = new System.DateTime(2000, 2, 19, 0, 0, 0, 0);
            this.dateTimePickerUpdateDateJoined.Name = "dateTimePickerUpdateDateJoined";
            this.dateTimePickerUpdateDateJoined.Size = new System.Drawing.Size(201, 26);
            this.dateTimePickerUpdateDateJoined.TabIndex = 122;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.Size = new System.Drawing.Size(665, 696);
            this.dataGridView1.TabIndex = 109;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefresh.Location = new System.Drawing.Point(671, 27);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(69, 27);
            this.buttonRefresh.TabIndex = 119;
            this.buttonRefresh.Text = "Reload";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // textBoxUpdateID
            // 
            this.textBoxUpdateID.Enabled = false;
            this.textBoxUpdateID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUpdateID.Location = new System.Drawing.Point(738, 67);
            this.textBoxUpdateID.Name = "textBoxUpdateID";
            this.textBoxUpdateID.Size = new System.Drawing.Size(124, 26);
            this.textBoxUpdateID.TabIndex = 118;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(868, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 20);
            this.label9.TabIndex = 117;
            this.label9.Text = "ID";
            // 
            // textBoxUpdateNFCID
            // 
            this.textBoxUpdateNFCID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUpdateNFCID.Location = new System.Drawing.Point(738, 163);
            this.textBoxUpdateNFCID.Name = "textBoxUpdateNFCID";
            this.textBoxUpdateNFCID.Size = new System.Drawing.Size(124, 26);
            this.textBoxUpdateNFCID.TabIndex = 116;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(868, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.TabIndex = 115;
            this.label5.Text = "NFCID";
            // 
            // buttonUopdate
            // 
            this.buttonUopdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUopdate.Location = new System.Drawing.Point(738, 306);
            this.buttonUopdate.Name = "buttonUopdate";
            this.buttonUopdate.Size = new System.Drawing.Size(87, 33);
            this.buttonUopdate.TabIndex = 114;
            this.buttonUopdate.Text = "Update";
            this.buttonUopdate.UseVisualStyleBackColor = true;
            this.buttonUopdate.Click += new System.EventHandler(this.buttonUopdate_Click);
            // 
            // textBoxUpdateAchternaam
            // 
            this.textBoxUpdateAchternaam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUpdateAchternaam.Location = new System.Drawing.Point(738, 131);
            this.textBoxUpdateAchternaam.Name = "textBoxUpdateAchternaam";
            this.textBoxUpdateAchternaam.Size = new System.Drawing.Size(124, 26);
            this.textBoxUpdateAchternaam.TabIndex = 113;
            // 
            // textBoxUpdateVoornaam
            // 
            this.textBoxUpdateVoornaam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUpdateVoornaam.Location = new System.Drawing.Point(738, 99);
            this.textBoxUpdateVoornaam.Name = "textBoxUpdateVoornaam";
            this.textBoxUpdateVoornaam.Size = new System.Drawing.Size(124, 26);
            this.textBoxUpdateVoornaam.TabIndex = 112;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(868, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 20);
            this.label7.TabIndex = 111;
            this.label7.Text = "Voornaam";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(868, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 20);
            this.label8.TabIndex = 110;
            this.label8.Text = "Achternaam";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectReaderToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1138, 24);
            this.menuStrip1.TabIndex = 120;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FormModUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 720);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonDeleteUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePickerNewDateLeft);
            this.Controls.Add(this.dateTimePickerNewDateJoined);
            this.Controls.Add(this.checkBoxNewZitNogOpSchool);
            this.Controls.Add(this.textBoxNewNfcid);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.buttonSaveNew);
            this.Controls.Add(this.textBoxNewAchternaam);
            this.Controls.Add(this.textBoxNewvoornaam);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerUpdateDateLeft);
            this.Controls.Add(this.checkBoxUpdateZitNogOpSchool);
            this.Controls.Add(this.dateTimePickerUpdateDateJoined);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.textBoxUpdateID);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxUpdateNFCID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonUopdate);
            this.Controls.Add(this.textBoxUpdateAchternaam);
            this.Controls.Add(this.textBoxUpdateVoornaam);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FormModUsers";
            this.Text = "FormModUsers";
            this.Load += new System.EventHandler(this.FormModUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonDeleteUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerNewDateLeft;
        private System.Windows.Forms.DateTimePicker dateTimePickerNewDateJoined;
        private System.Windows.Forms.CheckBox checkBoxNewZitNogOpSchool;
        private System.Windows.Forms.TextBox textBoxNewNfcid;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonSaveNew;
        private System.Windows.Forms.TextBox textBoxNewAchternaam;
        private System.Windows.Forms.TextBox textBoxNewvoornaam;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerUpdateDateLeft;
        private System.Windows.Forms.CheckBox checkBoxUpdateZitNogOpSchool;
        private System.Windows.Forms.ToolStripMenuItem connectRederToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectReaderToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dateTimePickerUpdateDateJoined;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.TextBox textBoxUpdateID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxUpdateNFCID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonUopdate;
        private System.Windows.Forms.TextBox textBoxUpdateAchternaam;
        private System.Windows.Forms.TextBox textBoxUpdateVoornaam;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}