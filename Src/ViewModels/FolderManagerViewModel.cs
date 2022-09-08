using Microsoft.WindowsAPICodePack.Dialogs;
using Stylet;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using FolderManager.Src.Models;
using FolderManager.Src.Utilities;
using System.Linq;
using System.Diagnostics;

namespace FolderManager.Src.ViewModels
{
    public class FolderManagerViewModel : Screen
    {
        public ObservableCollection<FolderInfo> FolderInfos { get; set; }

        private RWController rw = new("folderInfo.txt");

        public FolderManagerViewModel()
        {
            FolderInfos = new();
            InitialFolderInfos();
        }

        public void OpenFolderPicker(string isMultiselect)
        {
            CommonOpenFileDialog dialog = new();
            dialog.Title = "Folder Picker";
            dialog.Multiselect = isMultiselect == "0" ? false : true;
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (dialog.FileNames != null)
                {
                    foreach (string fileName in dialog.FileNames)
                    {
                        AddNewFolderInfo(fileName);
                    }

                    SaveFolderInfo();
                }
            }
        }

        public void SaveFolderInfo()
        {
            if (FolderInfos == null || FolderInfos.Count == 0)
            {
                return;
            }

            string folderInfoString = $"{FolderInfos[0].Name},{FolderInfos[0].Path}";
            rw.WriteData(folderInfoString);

            for (int i = 1; i < FolderInfos.Count; i++)
            {
                folderInfoString = $"{FolderInfos[i].Name},{FolderInfos[i].Path}";
                rw.AppendData(folderInfoString);
            }
        }

        private void InitialFolderInfos()
        {
            var folderInfoList = rw.ReadData();

            foreach (var folderInfoString in folderInfoList)
            {
                var arrFolderInfoString = folderInfoString.Trim().Split(',');

                if (arrFolderInfoString.Length == 2)
                {
                    if (string.IsNullOrEmpty(arrFolderInfoString[0]) ||
                        string.IsNullOrEmpty(arrFolderInfoString[1]))
                    {
                        FolderInfo folderInfo = new();
                        folderInfo.IsSpliter = true;
                        FolderInfos.Add(folderInfo);
                    }
                    else
                    {
                        AddNewFolderInfo(arrFolderInfoString[0], arrFolderInfoString[1]);
                    }
                }
            }
        }

        public void AddNewFolderInfo(string name, string path)
        {
            FolderInfo folderInfo = new();
            folderInfo.Name = name;
            folderInfo.Path = path;
            folderInfo.IsEdit = false;

            FolderInfos.Add(folderInfo);
        }

        public void AddNewFolderInfo(string path)
        {
            string name = path.Split('\\').Last();
            if (name.Contains('.'))
            {
                name = name.Substring(0, name.IndexOf('.'));
            }

            FolderInfo folderInfo = new();
            folderInfo.Name = name;
            folderInfo.Path = path;
            folderInfo.IsEdit = false;

            FolderInfos.Add(folderInfo);
        }

        public void OpenFolder(FolderInfo folderInfo)
        {
            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = folderInfo.Path,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch
            {

            }
        }

        public void EditFolderInfo(FolderInfo folderInfo)
        {
            if (folderInfo != null)
            {
                folderInfo.IsEdit = !folderInfo.IsEdit;
            }
            else
            {
                SaveFolderInfo();
            }

            foreach (var item in FolderInfos)
            {
                if (item != folderInfo)
                {
                    item.IsEdit = false;
                }
            }
        }

        public void DeleteFolderInfo(FolderInfo folderInfo)
        {
            if (folderInfo == null)
            {
                return;
            }

            FolderInfos.Remove(folderInfo);
            SaveFolderInfo();
        }
    }
}
