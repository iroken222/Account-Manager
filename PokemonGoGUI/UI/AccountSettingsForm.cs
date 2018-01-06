﻿using PokemonGoGUI.Enums;
using PokemonGoGUI.Extensions;
using PokemonGoGUI.GoManager;
using PokemonGoGUI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PokemonGoGUI.UI
{
    public partial class AccountSettingsForm : Form
    {
        private Manager _manager;

        public bool AutoUpdate { get; set; }

        public AccountSettingsForm(Manager manager)
        {
            InitializeComponent();

            _manager = manager;

            #region Catching

            olvColumnCatchId.AspectGetter = delegate(object x)
            {
                var setting = (CatchSetting)x;

                return (int)setting.Id;
            };

            #endregion

            #region Evolving

            olvColumnEvolveId.AspectGetter = delegate(object x)
            {
                var setting = (CatchSetting)x;

                return (int)setting.Id;
            };

            #endregion

            #region Transfer

            olvColumnTransferId.AspectGetter = delegate(object x)
            {
                var setting = (CatchSetting)x;

                return (int)setting.Id;
            };

            #endregion

        }

        private void AccountSettingsForm_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            foreach (AccountState state in Enum.GetValues(typeof(AccountState)))
            {
                if(state == AccountState.Good)
                {
                    continue;
                }

                comboBoxMinAccountState.Items.Add(state);
            }

            UpdateDetails(_manager.UserSettings);

            UpdateListViews();

            comboBoxLocationPresets.DataSource = _manager.FarmLocations;
            comboBoxLocationPresets.DisplayMember = "Name";
        }

        private void UpdateListViews()
        {
            fastObjectListViewRecycling.SetObjects(_manager.UserSettings.ItemSettings);
            fastObjectListViewCatch.SetObjects(_manager.UserSettings.PokemonSettings);
            fastObjectListViewEvolve.SetObjects(_manager.UserSettings.PokemonSettings);
            fastObjectListViewTransfer.SetObjects(_manager.UserSettings.PokemonSettings);
        }

        private void UpdateDetails(Settings settings)
        {
            textBoxPtcPassword.Text = settings.Password;
            textBoxPtcUsername.Text = settings.Username;
            textBoxLat.Text = settings.Location.Latitude.ToString();
            textBoxLong.Text = settings.Location.Longitude.ToString();
            textBoxName.Text = settings.AccountName;
            textBoxMaxTravelDistance.Text = settings.MaxTravelDistance.ToString();
            textBoxWalkSpeed.Text = settings.WalkingSpeed.ToString();
            textBoxPokemonBeforeEvolve.Text = settings.MinPokemonBeforeEvolve.ToString();
            textBoxMaxLevel.Text = settings.MaxLevel.ToString();
            textBoxProxy.Text = settings.Proxy.ToString();
            checkBoxMimicWalking.Checked = settings.MimicWalking;
            checkBoxShufflePokestops.Checked = settings.ShufflePokestops;
            checkBoxEncounterWhileWalking.Checked = settings.EncounterWhileWalking;
            checkBoxRecycle.Checked = settings.RecycleItems;
            checkBoxEvolve.Checked = settings.EvolvePokemon;
            checkBoxTransfers.Checked = settings.TransferPokemon;
            checkBoxTransferSlashPokemons.Checked = settings.TransferSlashPokemons;
            checkBoxUseLuckyEgg.Checked = settings.UseLuckyEgg;
            checkBoxIncubateEggs.Checked = settings.IncubateEggs;
            checkBoxOnlyUnlimitedIncubator.Checked = settings.OnlyUnlimitedIncubator;
            checkBoxCatchPokemon.Checked = settings.CatchPokemon;
            numericUpDownRunForHours.Value = new Decimal(settings.RunForHours);
            numericUpDownMaxLogs.Value = settings.MaxLogs;
            numericUpDownMaxFailBeforeReset.Value = settings.MaxFailBeforeReset;
            checkBoxStopOnIPBan.Checked = settings.StopOnIPBan;
            checkBoxAutoRotateProxies.Checked = settings.AutoRotateProxies;
            checkBoxRemoveOnStop.Checked = settings.AutoRemoveOnStop;
            checkBoxClaimLevelUp.Checked = settings.ClaimLevelUpRewards;
            numericUpDownSearchFortBelow.Value = new Decimal(settings.SearchFortBelowPercent);
            numericUpDownForceEvolveAbove.Value = new Decimal(settings.ForceEvolveAbovePercent);
            checkBoxStopOnAPIUpdate.Checked = settings.StopOnAPIUpdate;
            checkBoxSpinGyms.Checked = settings.SpinGyms;

            //Humanization
            checkBoxHumanizeThrows.Checked = settings.EnableHumanization;
            numericUpDownInsideReticuleChance.Value = settings.InsideReticuleChance;

            numericUpDownGeneralDelay.Value = settings.GeneralDelay;
            numericUpDownGeneralDelayRandom.Value = settings.GeneralDelayRandom;

            numericUpDownLocationUpdateDelay.Value = settings.DelayBetweenLocationUpdates;
            numericUpDownLocationUpdateRandom.Value = settings.LocationupdateDelayRandom;

            numericUpDownPlayerActionDelay.Value = settings.DelayBetweenPlayerActions;
            numericUpDownPlayerActionRandomiz.Value = settings.PlayerActionDelayRandom;

            numericUpDownWalkingOffset.Value = new Decimal(settings.WalkingSpeedOffset);
            //End humanization

            //Device settings
            textBoxDeviceId.Text = settings.DeviceInfo.DeviceId;
            textBoxDeviceModel.Text = settings.DeviceInfo.DeviceModel;
            textBoxDeviceBrand.Text = settings.DeviceInfo.DeviceBrand;
            textBoxDeviceModelBoot.Text = settings.DeviceInfo.DeviceModelBoot;
            textBoxFirmwareBrand.Text = settings.DeviceInfo.FirmwareBrand;
            textBoxFirmwareType.Text = settings.DeviceInfo.FirmwareType;
            textBoxHardwareManufacturer.Text = settings.DeviceInfo.HardwareManufacturer;
            textBoxHardwareModel.Text = settings.DeviceInfo.HardwareModel;
            //End device settings

            //Api config
            cbHashHost.Text = settings.HashHost.ToString();
            cbHashEndpoint.Text = settings.HashEndpoint;
            tbAuthHashKey.Text = settings.AuthAPIKey;
            cbUseOnlyThisHashKey.Checked = settings.UseOnlyOneKey;
            
            checkBoxUseBerries.Checked = settings.UseBerries;
            checkBoxGetARBonus.Checked = settings.GetArBonus;
            checkBoxCompleteTutorial.Checked = settings.CompleteTutorial;
            checkBoxTransferAtOnce.Checked = settings.TransferAtOnce;
            numericUpDownProximity.Value = settings.ARBonusProximity;
            numericUpDownAwareness.Value = settings.ARBonusAwareness;

            cbUseOnlyThisHashKey.Checked = _manager.UserSettings.UseOnlyOneKey;
            tbAuthHashKey.Text = _manager.UserSettings.AuthAPIKey;
            cbAutoUpdate.Checked = AutoUpdate;
            numericUpDownDisableCatchDelay.Value = new Decimal(_manager.UserSettings.DisableCatchDelay);
            
            checkBoxShowDebugLogs.Checked = settings.ShowDebugLogs;

            //Location time zones
            var zones = new TimeZoneIds().GetTimeZoneIds();
            foreach (var tz in zones)
            {
                cbTimeZones.Items.Add(tz.Key);
            }

            cbTimeZones.Text = _manager.UserSettings.PlayerLocale.Timezone;

            for (int i = 0; i < comboBoxMinAccountState.Items.Count; i++)
            {
                if((AccountState)comboBoxMinAccountState.Items[i] == settings.StopAtMinAccountState)
                {
                    comboBoxMinAccountState.SelectedIndex = i;
                    break;
                }
            }
        }


        private void CheckBoxMimicWalking_CheckedChanged(object sender, EventArgs e)
        {
            textBoxWalkSpeed.Enabled = false; 
            checkBoxEncounterWhileWalking.Enabled = false;

            if(checkBoxMimicWalking.Checked)
            {
                textBoxWalkSpeed.Enabled = true;
                checkBoxEncounterWhileWalking.Enabled = true;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (SaveSettings())
            {
                MessageBox.Show("Settings saved.\nSome settings won't take effect until the account stops running.", "Info");
            }
        }

        private bool SaveSettings()
        {
            Settings userSettings = _manager.UserSettings;

            ProxyEx proxyEx = null;

            int maxLevel;
            if (!Int32.TryParse(textBoxMaxLevel.Text, out maxLevel) || maxLevel < 0)
            {
                MessageBox.Show("Invalid Max level", "Warning");
                return false;
            }

            int minPokemonBeforeEvolve;
            if (!Int32.TryParse(textBoxPokemonBeforeEvolve.Text, out minPokemonBeforeEvolve) || minPokemonBeforeEvolve < 0)
            {
                MessageBox.Show("Invalid pokemon before evolve", "Warning");
                return false;
            }
            int walkingSpeed;
            if (!Int32.TryParse(textBoxWalkSpeed.Text, out  walkingSpeed) || walkingSpeed <= 0)
            {
                MessageBox.Show("Invalid walking speed", "Warning");
                return false;
            }
            int maxTravelDistance;
            if (!Int32.TryParse(textBoxMaxTravelDistance.Text, out maxTravelDistance) || maxTravelDistance <= 0)
            {
                MessageBox.Show("Invalid max travel distance", "Warning");
                return false;
            }
            double defaultLat;
            if (!Double.TryParse(textBoxLat.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out defaultLat))
            {
                MessageBox.Show("Invalid latitude", "Warning");
                return false;
            }
            double defaultLong;
            if (!Double.TryParse(textBoxLong.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out defaultLong))
            {
                MessageBox.Show("Invalid longitude", "Warning");
                return false;
            }

            if (!String.IsNullOrEmpty(textBoxProxy.Text) && !ProxyEx.TryParse(textBoxProxy.Text, out proxyEx))
            {
                MessageBox.Show("Invalid proxy format", "Warning");
                return false;
            }

            if (comboBoxMinAccountState.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid min account state", "Warning");
                return false;
            }

            if (walkingSpeed < (double)numericUpDownWalkingOffset.Value)
            {
                MessageBox.Show("Walking offset must be more than walking speed", "Warning");
                return false;
            }

            if (String.IsNullOrEmpty(textBoxPtcUsername.Text))
            {
                MessageBox.Show("Invalid Username", "Warning");
                return false;
            }
            if (String.IsNullOrEmpty(textBoxPtcPassword.Text))
            {
                MessageBox.Show("Invalid Password", "Warning");
                return false;
            }

            userSettings.AuthType = textBoxPtcUsername.Text.Contains("@") ?  AuthType.Google : AuthType.Ptc;

            userSettings.MimicWalking = checkBoxMimicWalking.Checked;
            userSettings.ShufflePokestops = checkBoxShufflePokestops.Checked;
            
            userSettings.Username = textBoxPtcUsername.Text.Trim();
            userSettings.Password = textBoxPtcPassword.Text.Trim();
            userSettings.Location.Latitude = defaultLat;
            userSettings.Location.Longitude = defaultLong;
            userSettings.WalkingSpeed = walkingSpeed;
            userSettings.MaxTravelDistance = maxTravelDistance;
            userSettings.EncounterWhileWalking = checkBoxEncounterWhileWalking.Checked;
            userSettings.AccountName = textBoxName.Text;
            userSettings.TransferPokemon = checkBoxTransfers.Checked;
            userSettings.TransferSlashPokemons = checkBoxTransferSlashPokemons.Checked;
            userSettings.EvolvePokemon = checkBoxEvolve.Checked;
            userSettings.RecycleItems = checkBoxRecycle.Checked;
            userSettings.MinPokemonBeforeEvolve = minPokemonBeforeEvolve;
            userSettings.UseLuckyEgg = checkBoxUseLuckyEgg.Checked;
            userSettings.IncubateEggs = checkBoxIncubateEggs.Checked;
            userSettings.OnlyUnlimitedIncubator = checkBoxOnlyUnlimitedIncubator.Checked;
            userSettings.MaxLevel = maxLevel;
            userSettings.CatchPokemon = checkBoxCatchPokemon.Checked;
            userSettings.StopAtMinAccountState = (AccountState)comboBoxMinAccountState.SelectedItem;
            userSettings.SearchFortBelowPercent = (double)numericUpDownSearchFortBelow.Value;
            userSettings.ForceEvolveAbovePercent = (double) numericUpDownForceEvolveAbove.Value;
            userSettings.ClaimLevelUpRewards = checkBoxClaimLevelUp.Checked;
            userSettings.StopOnAPIUpdate = checkBoxStopOnAPIUpdate.Checked;
            userSettings.SpinGyms = checkBoxSpinGyms.Checked;
            AutoUpdate = cbAutoUpdate.Checked;
            userSettings.UseBerries = checkBoxUseBerries.Checked;
            userSettings.DisableCatchDelay = (int)numericUpDownDisableCatchDelay.Value;

            userSettings.RunForHours = (double)numericUpDownRunForHours.Value;
            userSettings.MaxLogs = (int)numericUpDownMaxLogs.Value;
            userSettings.StopOnIPBan = checkBoxStopOnIPBan.Checked;
            userSettings.MaxFailBeforeReset = (int)numericUpDownMaxFailBeforeReset.Value;
            userSettings.AutoRotateProxies = checkBoxAutoRotateProxies.Checked;
            userSettings.AutoRemoveOnStop = checkBoxRemoveOnStop.Checked;

            //Humanization
            userSettings.EnableHumanization = checkBoxHumanizeThrows.Checked;
            userSettings.InsideReticuleChance = (int)numericUpDownInsideReticuleChance.Value;

            userSettings.GeneralDelay = (int)numericUpDownGeneralDelay.Value;
            userSettings.GeneralDelayRandom = (int)numericUpDownGeneralDelayRandom.Value;

            userSettings.DelayBetweenLocationUpdates = (int)numericUpDownLocationUpdateDelay.Value;
            userSettings.LocationupdateDelayRandom = (int)numericUpDownLocationUpdateRandom.Value;

            userSettings.DelayBetweenPlayerActions = (int)numericUpDownPlayerActionDelay.Value;
            userSettings.PlayerActionDelayRandom = (int)numericUpDownPlayerActionRandomiz.Value;

            userSettings.WalkingSpeedOffset = (double)numericUpDownWalkingOffset.Value;
            //End humanization

            //Device settings
            userSettings.DeviceInfo.DeviceId = textBoxDeviceId.Text;
            userSettings.DeviceInfo.DeviceModel = textBoxDeviceModel.Text;
            userSettings.DeviceInfo.DeviceBrand = textBoxDeviceBrand.Text;
            userSettings.DeviceInfo.DeviceModelBoot = textBoxDeviceModelBoot.Text;
            userSettings.DeviceInfo.HardwareManufacturer = textBoxHardwareManufacturer.Text;
            userSettings.DeviceInfo.HardwareModel = textBoxHardwareModel.Text;
            userSettings.DeviceInfo.FirmwareBrand = textBoxFirmwareBrand.Text;
            userSettings.DeviceInfo.FirmwareType = textBoxFirmwareType.Text;
            //End device settings

            //Api config
            userSettings.HashHost = new Uri(cbHashHost.Text);
            userSettings.HashEndpoint = cbHashEndpoint.Text;
            userSettings.AuthAPIKey = tbAuthHashKey.Text;
            userSettings.UseOnlyOneKey = cbUseOnlyThisHashKey.Checked;
            //End api config

            //Location time zones
            var x = new TimeZoneIds().GetTimeZoneIds();
            userSettings.PlayerLocale.Timezone = cbTimeZones.Text;
            userSettings.PlayerLocale.Country = x[cbTimeZones.Text].Item1;
            userSettings.PlayerLocale.Language = x[cbTimeZones.Text].Item2;
            userSettings.PlayerLocale.POSIX = x[cbTimeZones.Text].Item3;
            //End location time zones
            
            userSettings.GetArBonus = checkBoxGetARBonus.Checked;
            userSettings.CompleteTutorial = checkBoxCompleteTutorial.Checked;
            userSettings.TransferAtOnce = checkBoxTransferAtOnce.Checked;

            if (proxyEx != null)
            {
                userSettings.ProxyIP = proxyEx.Address;
                userSettings.ProxyPort = proxyEx.Port;
                userSettings.ProxyUsername = proxyEx.Username;
                userSettings.ProxyPassword = proxyEx.Password;
            }
            else
            {
                userSettings.ProxyUsername = null;
                userSettings.ProxyPassword = null;
                userSettings.ProxyIP = null;
                userSettings.ProxyPort = 0;
            }
            
            userSettings.ARBonusProximity = numericUpDownProximity.Value;
            userSettings.ARBonusAwareness = numericUpDownAwareness.Value;
            
            // 
            userSettings.ShowDebugLogs = checkBoxShowDebugLogs.Checked;

            return true;
        }

        private void ButtonDone_Click(object sender, EventArgs e)
        {
            if (SaveSettings())
            {
                DialogResult = DialogResult.OK;
            }
        }

        #region Recycling

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int totalObjects = fastObjectListViewRecycling.SelectedObjects.Count;

            if(totalObjects == 0)
            {
                return;
            }

            var iiSettings = fastObjectListViewRecycling.SelectedObjects[0] as InventoryItemSetting;

            if(iiSettings == null)
            {
                return;
            }

            string num = Prompt.ShowDialog("Max Inventory Amount", "Edit Amount", iiSettings.MaxInventory.ToString());

            if(String.IsNullOrEmpty(num))
            {
                return;
            }
            int maxInventory;
            if (!Int32.TryParse(num, out maxInventory))
            {
                return;
            }

            foreach (InventoryItemSetting item in fastObjectListViewRecycling.SelectedObjects)
            {
                item.MaxInventory = maxInventory;
            }

            fastObjectListViewRecycling.SetObjects(_manager.UserSettings.ItemSettings);
        }

        #endregion

        #region CatchPokemon

        private void TrueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tSMI = sender as ToolStripMenuItem;
 
            if(tSMI == null)
            {
                return;
            }

            var checkType = (CheckType)Int32.Parse(tSMI.Tag.ToString());

            foreach(CatchSetting cSetting in fastObjectListViewCatch.SelectedObjects)
            {
                if(checkType == CheckType.Toggle)
                {
                    cSetting.Catch = !cSetting.Catch;
                }
                else if (checkType == CheckType.True)
                {
                    cSetting.Catch = true;
                }
                else
                {
                    cSetting.Catch = false;
                }
            }

            fastObjectListViewCatch.RefreshSelectedObjects();
        }

        private void RestoreDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Reset defaults?", "Confirmation", MessageBoxButtons.YesNoCancel);

            if(result != DialogResult.Yes)
            {
                return;
            }

            _manager.RestoreCatchDefaults();

            fastObjectListViewCatch.SetObjects(_manager.UserSettings.PokemonSettings);
        }

        #endregion

        #region Evolving

        private void RestoreDefaultsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Reset defaults?", "Confirmation", MessageBoxButtons.YesNoCancel);

            if (result != DialogResult.Yes)
            {
                return;
            }

            _manager.RestoreEvolveDefaults();

            fastObjectListViewEvolve.SetObjects(_manager.UserSettings.PokemonSettings);
        }

        private void EditCPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = fastObjectListViewEvolve.SelectedObjects.Count;

            if(count == 0)
            {
                return;
            }

            int defaultCP = ((CatchSetting)fastObjectListViewEvolve.SelectedObjects[0]).MinEvolveCP;

            string cp = Prompt.ShowDialog("Enter minimum CP:", "Edit CP", defaultCP.ToString());

            if(String.IsNullOrEmpty(cp))
            {
                return;
            }
            int changeCp;
            if (!Int32.TryParse(cp, out changeCp) || changeCp < 0)
            {
                MessageBox.Show("Invalid amount", "Warning");

                return;
            }

            foreach (CatchSetting setting in fastObjectListViewEvolve.SelectedObjects)
            {
                setting.MinEvolveCP = changeCp;
            }

            fastObjectListViewEvolve.SetObjects(_manager.UserSettings.PokemonSettings);
        }

        private void TrueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var tSMI = sender as ToolStripMenuItem;

            if (tSMI == null)
            {
                return;
            }

            var checkType = (CheckType)Int32.Parse(tSMI.Tag.ToString());

            foreach (CatchSetting eSetting in fastObjectListViewEvolve.SelectedObjects)
            {
                if (checkType == CheckType.Toggle)
                {
                    eSetting.Evolve = !eSetting.Evolve;
                }
                else if (checkType == CheckType.True)
                {
                    eSetting.Evolve = true;
                }
                else
                {
                    eSetting.Evolve = false;
                }
            }

            fastObjectListViewEvolve.SetObjects(_manager.UserSettings.PokemonSettings);
        }

        #endregion

        #region Transfer

        private void EditTransferSettings(List<CatchSetting> settings)
        {
            if(settings.Count == 0)
            {
                return;
            }

            var transferSettingForm = new TransferSettingsForm(settings);
            transferSettingForm.ShowDialog();

            fastObjectListViewTransfer.RefreshObjects(settings);
        }

        private void EditToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditTransferSettings(fastObjectListViewTransfer.SelectedObjects.Cast<CatchSetting>().ToList());
        }

        private void RestoreDefaultsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Reset defaults?", "Confirmation", MessageBoxButtons.YesNoCancel);

            if (result != DialogResult.Yes)
            {
                return;
            }

            _manager.RestoreTransferDefaults();

            fastObjectListViewTransfer.SetObjects(_manager.UserSettings.PokemonSettings);
        }

        #endregion

        private void ComboBoxLocationPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fLocation = comboBoxLocationPresets.SelectedItem as FarmLocation;
            if (fLocation!=null)
            {
                if (fLocation.Name == "Current")
                {
                    textBoxLat.Text = _manager.UserSettings.Location.Latitude.ToString();
                    textBoxLong.Text = _manager.UserSettings.Location.Longitude.ToString();
                }
                else
                {
                    textBoxLat.Text = fLocation.Latitude.ToString();
                    textBoxLong.Text = fLocation.Longitude.ToString();
                }
            }
        }

        private async void ButtonExportConfig_Click(object sender, EventArgs e)
        {
            if(!SaveSettings())
            {
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Json Files (*.json)|*.json|All Files (*.*)|*.*";

                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                MethodResult result = await _manager.ExportConfig(sfd.FileName);

                if (result.Success)
                {
                    MessageBox.Show("Config exported", "Info");
                }
            }
        }

        private async void ButtonImportConfig_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Open config file";
                ofd.Filter = "Json Files (*.json)|*.json|All Files (*.*)|*.*";

                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                MethodResult result = await _manager.ImportConfigFromFile(ofd.FileName);

                if(!result.Success)
                {
                    return;
                }

                UpdateDetails(_manager.UserSettings);
                UpdateListViews();
            }
        }

        private void ButtonDeviceRandom_Click(object sender, EventArgs e)
        {
            _manager.RandomDeviceId();

            textBoxDeviceId.Text = _manager.UserSettings.DeviceInfo.DeviceId;

            //UpdateDetails(_manager.UserSettings);
        }

        private void ButtonResetDefaults_Click(object sender, EventArgs e)
        {
            _manager.RandomDevice();

            UpdateDetails(_manager.UserSettings);
        }

        private void CheckBoxAutoRotateProxies_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxRemoveOnStop.Enabled = checkBoxAutoRotateProxies.Checked;
        }

        private void AccountSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        void setUsePinapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fastObjectListViewCatch.SelectedObjects== null)
                return;

            foreach(CatchSetting cSetting in fastObjectListViewCatch.SelectedObjects)
                cSetting.UsePinap = !cSetting.UsePinap;

            fastObjectListViewCatch.RefreshSelectedObjects();
        }

    }
}
