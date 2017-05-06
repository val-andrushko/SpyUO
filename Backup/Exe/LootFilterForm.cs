using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Windows.Forms;

using SpyUO.Packets;

namespace SpyUO
{
	public class LootFilterForm : System.Windows.Forms.Form
	{
		#region Windows Form Designer generated code
		private CheckedListBox ItemFilter;
		private Button ButtonOk;
		private Button ButtonCancel;
		private Button ButtonLoad;
		private Button ButtonSave;
		private SaveFileDialog FilterSave;
		private OpenFileDialog FilterLoad;
		private CheckBox ToggleArmor;
		private CheckBox ToggleAos;
		private CheckBox ToggleWeapon;
		private CheckedListBox MobileFilter;
		private Label LabelItemFilter;
		private Label LabelMobileFilter;
		private CheckBox ToggleAllMobiles;
		private TextBox SavePath;
		private Button ButtonSelectFile;
		private CheckBox ToggleWearable;
		private CheckBox ToggleOther;
		private SaveFileDialog LootSave;
		private TextBox PropertyFilter;
		private RadioButton RadioInclude;
		private RadioButton RadioExclude;

		private System.ComponentModel.Container components = null;

		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				if ( components != null )
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
			this.ItemFilter = new System.Windows.Forms.CheckedListBox();
			this.ButtonOk = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.ButtonLoad = new System.Windows.Forms.Button();
			this.ButtonSave = new System.Windows.Forms.Button();
			this.FilterSave = new System.Windows.Forms.SaveFileDialog();
			this.FilterLoad = new System.Windows.Forms.OpenFileDialog();
			this.ToggleArmor = new System.Windows.Forms.CheckBox();
			this.ToggleAos = new System.Windows.Forms.CheckBox();
			this.ToggleWeapon = new System.Windows.Forms.CheckBox();
			this.MobileFilter = new System.Windows.Forms.CheckedListBox();
			this.LabelItemFilter = new System.Windows.Forms.Label();
			this.LabelMobileFilter = new System.Windows.Forms.Label();
			this.ToggleAllMobiles = new System.Windows.Forms.CheckBox();
			this.SavePath = new System.Windows.Forms.TextBox();
			this.ButtonSelectFile = new System.Windows.Forms.Button();
			this.ToggleWearable = new System.Windows.Forms.CheckBox();
			this.ToggleOther = new System.Windows.Forms.CheckBox();
			this.LootSave = new System.Windows.Forms.SaveFileDialog();
			this.PropertyFilter = new System.Windows.Forms.TextBox();
			this.RadioInclude = new System.Windows.Forms.RadioButton();
			this.RadioExclude = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// ItemFilter
			// 
			this.ItemFilter.Location = new System.Drawing.Point( 12, 71 );
			this.ItemFilter.Name = "ItemFilter";
			this.ItemFilter.Size = new System.Drawing.Size( 176, 259 );
			this.ItemFilter.TabIndex = 0;
			this.ItemFilter.SelectedIndexChanged += new System.EventHandler( this.ItemFilter_SelectedIndexChanged );
			this.ItemFilter.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler( this.FilterList_ItemCheck );
			// 
			// ButtonOk
			// 
			this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ButtonOk.Location = new System.Drawing.Point( 295, 403 );
			this.ButtonOk.Name = "ButtonOk";
			this.ButtonOk.Size = new System.Drawing.Size( 75, 23 );
			this.ButtonOk.TabIndex = 1;
			this.ButtonOk.Text = "Ok";
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point( 214, 402 );
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size( 75, 23 );
			this.ButtonCancel.TabIndex = 2;
			this.ButtonCancel.Text = "Cancel";
			// 
			// ButtonLoad
			// 
			this.ButtonLoad.Location = new System.Drawing.Point( 113, 6 );
			this.ButtonLoad.Name = "ButtonLoad";
			this.ButtonLoad.Size = new System.Drawing.Size( 75, 23 );
			this.ButtonLoad.TabIndex = 7;
			this.ButtonLoad.Text = "Load";
			this.ButtonLoad.Click += new System.EventHandler( this.ButtonLoad_Click );
			// 
			// ButtonSave
			// 
			this.ButtonSave.Location = new System.Drawing.Point( 194, 6 );
			this.ButtonSave.Name = "ButtonSave";
			this.ButtonSave.Size = new System.Drawing.Size( 75, 23 );
			this.ButtonSave.TabIndex = 8;
			this.ButtonSave.Text = "Save";
			this.ButtonSave.Click += new System.EventHandler( this.ButtonSave_Click );
			// 
			// FilterSave
			// 
			this.FilterSave.FileName = "Filter.loot";
			this.FilterSave.Filter = "Filter files (*.loot)|*.loot";
			// 
			// FilterLoad
			// 
			this.FilterLoad.FileName = "Filter.loot";
			this.FilterLoad.Filter = "Filter files (*.loot)|*.loot";
			// 
			// ToggleArmor
			// 
			this.ToggleArmor.AutoSize = true;
			this.ToggleArmor.Checked = true;
			this.ToggleArmor.CheckState = System.Windows.Forms.CheckState.Indeterminate;
			this.ToggleArmor.Location = new System.Drawing.Point( 12, 41 );
			this.ToggleArmor.Name = "ToggleArmor";
			this.ToggleArmor.Size = new System.Drawing.Size( 53, 17 );
			this.ToggleArmor.TabIndex = 9;
			this.ToggleArmor.Text = "Armor";
			this.ToggleArmor.UseVisualStyleBackColor = true;
			this.ToggleArmor.CheckStateChanged += new System.EventHandler( this.ToggleArmor_CheckStateChanged );
			// 
			// ToggleAos
			// 
			this.ToggleAos.AutoSize = true;
			this.ToggleAos.Checked = true;
			this.ToggleAos.CheckState = System.Windows.Forms.CheckState.Indeterminate;
			this.ToggleAos.Location = new System.Drawing.Point( 71, 41 );
			this.ToggleAos.Name = "ToggleAos";
			this.ToggleAos.Size = new System.Drawing.Size( 44, 17 );
			this.ToggleAos.TabIndex = 10;
			this.ToggleAos.Text = "Aos";
			this.ToggleAos.UseVisualStyleBackColor = true;
			this.ToggleAos.CheckStateChanged += new System.EventHandler( this.ToggleAos_CheckStateChanged );
			// 
			// ToggleWeapon
			// 
			this.ToggleWeapon.AutoSize = true;
			this.ToggleWeapon.Checked = true;
			this.ToggleWeapon.CheckState = System.Windows.Forms.CheckState.Indeterminate;
			this.ToggleWeapon.Location = new System.Drawing.Point( 121, 41 );
			this.ToggleWeapon.Name = "ToggleWeapon";
			this.ToggleWeapon.Size = new System.Drawing.Size( 67, 17 );
			this.ToggleWeapon.TabIndex = 11;
			this.ToggleWeapon.Text = "Weapon";
			this.ToggleWeapon.UseVisualStyleBackColor = true;
			this.ToggleWeapon.CheckStateChanged += new System.EventHandler( this.ToggleWeapon_CheckStateChanged );
			// 
			// MobileFilter
			// 
			this.MobileFilter.Location = new System.Drawing.Point( 194, 71 );
			this.MobileFilter.Name = "MobileFilter";
			this.MobileFilter.Size = new System.Drawing.Size( 176, 259 );
			this.MobileFilter.TabIndex = 0;
			this.MobileFilter.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler( this.MobileFilter_ItemCheck );
			// 
			// LabelItemFilter
			// 
			this.LabelItemFilter.AutoSize = true;
			this.LabelItemFilter.Font = new System.Drawing.Font( "Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte) ( 238 ) ) );
			this.LabelItemFilter.Location = new System.Drawing.Point( 25, 9 );
			this.LabelItemFilter.Name = "LabelItemFilter";
			this.LabelItemFilter.Size = new System.Drawing.Size( 76, 16 );
			this.LabelItemFilter.TabIndex = 13;
			this.LabelItemFilter.Text = "Item Filter";
			// 
			// LabelMobileFilter
			// 
			this.LabelMobileFilter.AutoSize = true;
			this.LabelMobileFilter.Font = new System.Drawing.Font( "Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte) ( 238 ) ) );
			this.LabelMobileFilter.Location = new System.Drawing.Point( 276, 9 );
			this.LabelMobileFilter.Name = "LabelMobileFilter";
			this.LabelMobileFilter.Size = new System.Drawing.Size( 85, 16 );
			this.LabelMobileFilter.TabIndex = 14;
			this.LabelMobileFilter.Text = "Mobs Filter";
			// 
			// ToggleAllMobiles
			// 
			this.ToggleAllMobiles.AutoSize = true;
			this.ToggleAllMobiles.Checked = true;
			this.ToggleAllMobiles.CheckState = System.Windows.Forms.CheckState.Indeterminate;
			this.ToggleAllMobiles.Location = new System.Drawing.Point( 242, 41 );
			this.ToggleAllMobiles.Name = "ToggleAllMobiles";
			this.ToggleAllMobiles.Size = new System.Drawing.Size( 76, 17 );
			this.ToggleAllMobiles.TabIndex = 15;
			this.ToggleAllMobiles.Text = "All Mobiles";
			this.ToggleAllMobiles.UseVisualStyleBackColor = true;
			this.ToggleAllMobiles.CheckedChanged += new System.EventHandler( this.ToggleAllMobiles_CheckedChanged );
			// 
			// SavePath
			// 
			this.SavePath.Location = new System.Drawing.Point( 12, 376 );
			this.SavePath.Name = "SavePath";
			this.SavePath.Size = new System.Drawing.Size( 329, 20 );
			this.SavePath.TabIndex = 16;
			// 
			// ButtonSelectFile
			// 
			this.ButtonSelectFile.Location = new System.Drawing.Point( 347, 374 );
			this.ButtonSelectFile.Name = "ButtonSelectFile";
			this.ButtonSelectFile.Size = new System.Drawing.Size( 23, 23 );
			this.ButtonSelectFile.TabIndex = 17;
			this.ButtonSelectFile.Text = "...";
			this.ButtonSelectFile.UseVisualStyleBackColor = true;
			this.ButtonSelectFile.Click += new System.EventHandler( this.ButtonSelectFile_Click );
			// 
			// ToggleWearable
			// 
			this.ToggleWearable.AutoSize = true;
			this.ToggleWearable.Checked = true;
			this.ToggleWearable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ToggleWearable.Location = new System.Drawing.Point( 12, 407 );
			this.ToggleWearable.Name = "ToggleWearable";
			this.ToggleWearable.Size = new System.Drawing.Size( 72, 17 );
			this.ToggleWearable.TabIndex = 19;
			this.ToggleWearable.Text = "Wearable";
			this.ToggleWearable.UseVisualStyleBackColor = true;
			this.ToggleWearable.CheckStateChanged += new System.EventHandler( this.ToggleWearable_CheckedStateChanged );
			// 
			// ToggleOther
			// 
			this.ToggleOther.AutoSize = true;
			this.ToggleOther.Checked = true;
			this.ToggleOther.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ToggleOther.Location = new System.Drawing.Point( 90, 407 );
			this.ToggleOther.Name = "ToggleOther";
			this.ToggleOther.Size = new System.Drawing.Size( 52, 17 );
			this.ToggleOther.TabIndex = 20;
			this.ToggleOther.Text = "Other";
			this.ToggleOther.UseVisualStyleBackColor = true;
			this.ToggleOther.CheckStateChanged += new System.EventHandler( this.ToggleOther_CheckedStateChanged );
			// 
			// LootSave
			// 
			this.LootSave.FileName = "Loot.txt";
			this.LootSave.Filter = "txt files (*.txt)|*.txt";
			// 
			// PropertyFilter
			// 
			this.PropertyFilter.Enabled = false;
			this.PropertyFilter.Location = new System.Drawing.Point( 113, 343 );
			this.PropertyFilter.Name = "PropertyFilter";
			this.PropertyFilter.Size = new System.Drawing.Size( 257, 20 );
			this.PropertyFilter.TabIndex = 22;
			this.PropertyFilter.Leave += new System.EventHandler( this.PropertyFilter_Leave );
			// 
			// RadioInclude
			// 
			this.RadioInclude.AutoSize = true;
			this.RadioInclude.Checked = true;
			this.RadioInclude.Enabled = false;
			this.RadioInclude.Location = new System.Drawing.Point( 12, 336 );
			this.RadioInclude.Margin = new System.Windows.Forms.Padding( 3, 3, 3, 0 );
			this.RadioInclude.Name = "RadioInclude";
			this.RadioInclude.Size = new System.Drawing.Size( 78, 17 );
			this.RadioInclude.TabIndex = 24;
			this.RadioInclude.TabStop = true;
			this.RadioInclude.Text = "Must have:";
			this.RadioInclude.UseVisualStyleBackColor = true;
			// 
			// RadioExclude
			// 
			this.RadioExclude.AutoSize = true;
			this.RadioExclude.Enabled = false;
			this.RadioExclude.Location = new System.Drawing.Point( 12, 353 );
			this.RadioExclude.Margin = new System.Windows.Forms.Padding( 3, 0, 3, 3 );
			this.RadioExclude.Name = "RadioExclude";
			this.RadioExclude.Size = new System.Drawing.Size( 96, 17 );
			this.RadioExclude.TabIndex = 25;
			this.RadioExclude.Text = "Must not have:";
			this.RadioExclude.UseVisualStyleBackColor = true;
			this.RadioExclude.CheckedChanged += new System.EventHandler( this.RadioExclude_CheckedChanged );
			// 
			// LootFilterForm
			// 
			this.AcceptButton = this.ButtonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size( 382, 438 );
			this.Controls.Add( this.RadioExclude );
			this.Controls.Add( this.RadioInclude );
			this.Controls.Add( this.PropertyFilter );
			this.Controls.Add( this.ToggleOther );
			this.Controls.Add( this.ToggleWearable );
			this.Controls.Add( this.ButtonSelectFile );
			this.Controls.Add( this.SavePath );
			this.Controls.Add( this.ToggleAllMobiles );
			this.Controls.Add( this.LabelMobileFilter );
			this.Controls.Add( this.LabelItemFilter );
			this.Controls.Add( this.MobileFilter );
			this.Controls.Add( this.ToggleWeapon );
			this.Controls.Add( this.ToggleAos );
			this.Controls.Add( this.ToggleArmor );
			this.Controls.Add( this.ButtonSave );
			this.Controls.Add( this.ButtonLoad );
			this.Controls.Add( this.ButtonCancel );
			this.Controls.Add( this.ButtonOk );
			this.Controls.Add( this.ItemFilter );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LootFilterForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Loot Filter";
			this.Shown += new System.EventHandler( this.LootFilterForm_Shown );
			this.ResumeLayout( false );
			this.PerformLayout();

		}
		#endregion

		#region Properties
		private LootFilter m_Filter;
		private bool m_Locked;

		public string FileName
		{
			get { return SavePath.Text; }
		}
		#endregion

		#region Constructors
		public LootFilterForm( LootFilter filter )
		{
			m_Filter = filter;

			InitializeComponent();

			string[] names = Enum.GetNames( typeof( ItemProperty ) );
			ItemFilter.Items.AddRange( names );
			ItemFilter.Items.AddRange( Item.DefaultNames );
		}
		#endregion

		#region Private Members
		private void InvalidateItems()
		{
			m_Locked = true;

			for ( int i = 0; i < ItemFilter.Items.Count; i++ )
				ItemFilter.SetItemChecked( i, m_Filter.Filters( i ) );

			m_Locked = false;
		}

		private void InvalidateMobiles()
		{
			m_Locked = true;

			for ( int i = 0; i < MobileFilter.Items.Count; i++ )
				MobileFilter.SetItemChecked( i, m_Filter.Filters( MobileFilter.Items[ i ].ToString() ) );

			m_Locked = false;
		}

		#region Events
		private void ButtonLoad_Click( object sender, EventArgs e )
		{
			if ( FilterLoad.ShowDialog() == DialogResult.OK )
			{
				m_Filter.Load( FilterLoad.FileName );

				InvalidateItems();
			}
		}

		private void ButtonSave_Click( object sender, EventArgs e )
		{
			if ( FilterSave.ShowDialog() == DialogResult.OK )
			{
				m_Filter.Save( FilterLoad.FileName );
			}
		}

		private void FilterList_ItemCheck( object sender, ItemCheckEventArgs e )
		{
			if ( !m_Locked )
				m_Filter.Toggle( e.Index );
		}

		private void MobileFilter_ItemCheck( object sender, ItemCheckEventArgs e )
		{
			if ( !m_Locked )
				m_Filter.Toggle( MobileFilter.Items[ e.Index ].ToString() );
		}

		private void ToggleAllMobiles_CheckedChanged( object sender, EventArgs e )
		{
			m_Filter.ToggleMobiles( ToggleAllMobiles.Checked );

			InvalidateMobiles();
		}

		private void ToggleArmor_CheckStateChanged( object sender, EventArgs e )
		{
			m_Filter.ToggleArmor( ToggleArmor.Checked );

			InvalidateItems();
		}

		private void ToggleAos_CheckStateChanged( object sender, EventArgs e )
		{
			m_Filter.ToggleAos( ToggleAos.Checked );

			InvalidateItems();
		}

		private void ToggleWeapon_CheckStateChanged( object sender, EventArgs e )
		{
			m_Filter.ToggleWeapon( ToggleWeapon.Checked );

			InvalidateItems();
		}

		private void ToggleWearable_CheckedStateChanged( object sender, EventArgs e )
		{
			m_Filter.Wearable = ToggleWearable.Checked;

		}

		private void ToggleOther_CheckedStateChanged( object sender, EventArgs e )
		{
			m_Filter.Other = ToggleOther.Checked;
		}

		private void ButtonSelectFile_Click( object sender, EventArgs e )
		{
			if ( !String.IsNullOrEmpty( SavePath.Text ) )
				LootSave.FileName = SavePath.Text;

			if ( LootSave.ShowDialog() == DialogResult.OK )
				SavePath.Text = LootSave.FileName;
		}

		private void LootFilterForm_Shown( object sender, EventArgs e )
		{
			MobileFilter.Items.Clear();

			foreach ( string s in m_Filter.MobileFilter.Keys )
				MobileFilter.Items.Add( s );

			InvalidateItems();
			InvalidateMobiles();
		}

		private void RadioExclude_CheckedChanged( object sender, EventArgs e )
		{
			int i = ItemFilter.SelectedIndex;

			if ( i >= 0 )
			{
				PropertyFilter filter = m_Filter.PropertyFilter[ i ];

				filter.Exclude = RadioExclude.Checked;
			}
		}

		private void ItemFilter_SelectedIndexChanged( object sender, EventArgs e )
		{
			int i = ItemFilter.SelectedIndex;

			if ( i >= 0 )
			{
				PropertyFilter filter = m_Filter.PropertyFilter[ i ];
				StringBuilder builder = new StringBuilder();

				foreach ( object o in filter.Values )
				{
					builder.Append( o.ToString() );
					builder.Append( '|' );
				}

				RadioExclude.Enabled = true;
				RadioInclude.Enabled = true;
				PropertyFilter.Enabled = true;
				PropertyFilter.Text = builder.ToString();
			}
			else
			{
				RadioExclude.Enabled = false;
				RadioInclude.Enabled = false;
				PropertyFilter.Enabled = false;
			}
		}

		private void PropertyFilter_Leave( object sender, EventArgs e )
		{
			int i = ItemFilter.SelectedIndex;

			if ( i >= 0 )
			{
				PropertyFilter filter = m_Filter.PropertyFilter[ i ];
				string[] split = PropertyFilter.Text.Split( new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries );
				int iValue;
				double dValue;

				filter.Values.Clear();

				foreach ( string s in split )
				{
					if ( s.StartsWith( "0x" ) && Int32.TryParse( s.Substring( 2, s.Length - 2 ), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out iValue ) )
						filter.Values.Add( iValue );
					else if ( Int32.TryParse( s, out iValue ) )
						filter.Values.Add( iValue );
					else if ( Double.TryParse( s, out dValue ) )
						filter.Values.Add( dValue );
					else
						filter.Values.Add( s );
				}
			}
		}
		#endregion

		#endregion
	}
}