using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Vesuv.Core;

namespace Vesuv.Editor
{
    /// <summary>
    /// Interaktionslogik für ProjectManagerWindow.xaml
    /// </summary>
    public partial class ProjectManagerWindow : Window
    {
        public ProjectManagerWindow()
        {
            InitializeComponent();

            //var projectInfo = new ProjectFile(new DirectoryInfo("S:\\Work\\Vesuv\\Project 1"));
        }
    }
}
