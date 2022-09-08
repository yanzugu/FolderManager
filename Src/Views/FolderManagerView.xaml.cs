using FolderManager.Src.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FolderManager.Src.Views
{
    public partial class FolderManagerView : Window
    {
        private FolderManagerViewModel vm;

        public FolderManagerView()
        {
            InitializeComponent();
            Loaded += FolderManagerView_Loaded;
        }

        private void FolderManagerView_Loaded(object sender, RoutedEventArgs e)
        {
            vm = DataContext as FolderManagerViewModel;
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            UIElement thumb = e.Source as UIElement;

            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
        }

        private void Border_PreviewDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (vm != null)
            {
                foreach (string fileName in files)
                {
                    vm.AddNewFolderInfo(fileName);
                }

                vm.SaveFolderInfo();
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btn_Minimized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
