﻿using LiveSplit.Model;
using POE2.AutoSplitter.Component.Settings;
using POE2.AutoSplitter.Component.Timer;
using POE2.AutoSplitter.Component.UI;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace POE2.AutoSplitter.Component
{
    partial class SettingsControl : UserControl
    {
        private ComponentSettings settings;

        private LiveSplitState state;

        public SettingsControl(ComponentSettings settings, LiveSplitState state)
        {
            this.settings = settings;
            this.state = state;
            InitializeComponent();
            XmlRefresh();
        }

        public void XmlRefresh()
        {
            checkAutoSplit.Checked = settings.AutoSplitEnabled;
            checkLoadRemoval.Checked = settings.LoadRemovalEnabled;
            textLogLocation.Text = settings.LogLocation;
            radioZones.Checked = settings.CriteriaToSplit == ComponentSettings.SplitCriteria.Zones;
            radioLevels.Checked = settings.CriteriaToSplit == ComponentSettings.SplitCriteria.Levels;
            radioLab.Checked = settings.CriteriaToSplit == ComponentSettings.SplitCriteria.Labyrinth;
            checkIcons.Checked = settings.GenerateWithIcons;
            radioAllLab.Checked = settings.LabSplitType == ComponentSettings.LabSplitMode.AllZones;
            radioAspirant.Checked = settings.LabSplitType == ComponentSettings.LabSplitMode.Trials;

            updateSplitCriteriaSpecificArea();
        }

        private void updateSplitCriteriaSpecificArea()
        {
            groupBoxLab.Visible = settings.CriteriaToSplit == ComponentSettings.SplitCriteria.Labyrinth;

            panelSplitList.Visible = settings.CriteriaToSplit == ComponentSettings.SplitCriteria.Zones || 
                settings.CriteriaToSplit == ComponentSettings.SplitCriteria.Levels;
            checkIcons.Visible = settings.CriteriaToSplit == ComponentSettings.SplitCriteria.Zones;

            checkedSplitList.Items.Clear();
            if (settings.CriteriaToSplit == ComponentSettings.SplitCriteria.Zones)
            {
                foreach (Zone zone in Zone.ZONES)
                {
                    checkedSplitList.Items.Add(zone, settings.SplitZones.Contains(zone));
                }
                for (int i = 70; i <= 100; i++)
                {
                    checkedSplitList.Items.Add(new LevelLabel(i), settings.SplitZoneLevels.Contains(i));
                }
            }
            else if (settings.CriteriaToSplit == ComponentSettings.SplitCriteria.Levels)
            {
                for (int i = 2; i <= 100; i++)
                {
                    checkedSplitList.Items.Add(new LevelLabel(i), settings.SplitLevels.Contains(i));
                }
            }
        }

        private void HandleAutoSplitChanged(object sender, EventArgs e)
        {
            settings.AutoSplitEnabled = checkAutoSplit.Checked;
        }

        private void handleLoadRemovalChanged(object sender, EventArgs e)
        {
            settings.LoadRemovalEnabled = checkLoadRemoval.Checked;
        }

        private void handleLogLocationChanged(object sender, EventArgs e)
        {
            settings.LogLocation = textLogLocation.Text;
        }

        private void HandleTestClick(object sender, EventArgs e)
        {
            try
            {
                using (FileStream fs = new FileStream(settings.LogLocation, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                { }
                MessageBox.Show("No problems found.", "Log File Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Log File Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleBrowseClick(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textLogLocation.Text = openFileDialog.FileName;
            }
        }

        private void LinkLoadSetupClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/brandondong/POE-LiveSplit-Component/blob/master/README.md");
        }

        private void HandleItemChecked(object sender, ItemCheckEventArgs e)
        {
            object selectedItem = checkedSplitList.Items[e.Index];
            if (settings.CriteriaToSplit == ComponentSettings.SplitCriteria.Zones)
            {
                if (selectedItem is IZone)
                {
                    IZone zone = (IZone)selectedItem;
                    if (e.NewValue == CheckState.Checked)
                    {
                        settings.SplitZones.Add(zone);
                    }
                    else
                    {
                        settings.SplitZones.Remove(zone);
                    }
                }
                else
                {
                    LevelLabel level = (LevelLabel)selectedItem;
                    if (e.NewValue == CheckState.Checked)
                    {
                        settings.SplitZoneLevels.Add(level.Level);
                    }
                    else
                    {
                        settings.SplitZoneLevels.Remove(level.Level);
                    }
                }
            }
            else if (settings.CriteriaToSplit == ComponentSettings.SplitCriteria.Levels)
            {
                LevelLabel level = (LevelLabel)selectedItem;
                if (e.NewValue == CheckState.Checked)
                {
                    settings.SplitLevels.Add(level.Level);
                }
                else
                {
                    settings.SplitLevels.Remove(level.Level);
                }
            }
        }

        private void HandleSelectAll(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedSplitList.Items.Count; i++)
            {
                checkedSplitList.SetItemChecked(i, checkSelectAll.Checked);
            }
        }

        private void HandleGenerateSplits(object sender, EventArgs e)
        {
            if (state.CurrentPhase != TimerPhase.NotRunning)
            {
                MessageBox.Show("Splits cannot be changed while the timer is running or has not been reset.",
                    "Generate Splits", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Your current split segments will be overwritten.\nAre you sure you want to proceed?",
                "Confirm Generate Splits", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }
            state.Run.Clear();
            for (int i = 0; i < checkedSplitList.Items.Count; i++)
            {
                if (checkedSplitList.GetItemChecked(i))
                {
                    object selectedItem = checkedSplitList.Items[i];
                    if (selectedItem is IZone)
                    {
                        IZone zone = (IZone)selectedItem;
                        Image icon = null;
                        state.Run.AddSegment(zone.SplitName(), default(Time), default(Time), icon);
                    }
                    else
                    {
                        state.Run.AddSegment(selectedItem.ToString());
                    }
                }
            }
            if (state.Run.Count == 0)
            {
                state.Run.AddSegment("");
            }
            state.Run.HasChanged = true;
            state.Form.Invalidate();
            MessageBox.Show("Splits generated successfully.\n\n" + 
                "The splits can be edited in the Splits Editor after saving and reopening.\n" + 
                "If the split names do not match the order of your zone progression, they can be reordered using that editor.",
                "Generate Splits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HandleSplitCriteriaChanged(object sender, EventArgs e)
        {
            if (radioZones.Checked)
            {
                settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Zones;
            }
            else if (radioLevels.Checked)
            {
                settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Levels;
            }
            else if (radioLab.Checked)
            {
                settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Labyrinth;
            }
            updateSplitCriteriaSpecificArea();
        }

        private void HandleLabTypeChanged(object sender, EventArgs e)
        {
            if (radioAllLab.Checked)
            {
                settings.LabSplitType = ComponentSettings.LabSplitMode.AllZones;
            }
            else if (radioAspirant.Checked)
            {
                settings.LabSplitType = ComponentSettings.LabSplitMode.Trials;
            }
        }

        private void HandleIconsChecked(object sender, EventArgs e)
        {
            settings.GenerateWithIcons = checkIcons.Checked;
        }
    }
}
