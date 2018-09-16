/*
  Title Display plugin for KeePass for Pleasant Password Server.
  Copyright (C) 2018 Tomas Kouba

  This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeePass.Plugins;

namespace PpsTitleDisplay
{
    public class PpsTitleDisplayExt : Plugin
    {

        private const string PLEASANT_PASSWORD_SERVER = "Pleasant Password Server";
        private const string KEEPASS = "KeePass";

        private IPluginHost m_host = null;
        public override bool Initialize(IPluginHost host)
        {
            Debug.Assert(host != null);
            if (host == null)
                return false;

            m_host = host;

            m_host.MainWindow.TextChanged += new EventHandler(UpdateTitle);
            m_host.MainWindow.UIStateUpdated += new EventHandler(UpdateTitle);
            m_host.MainWindow.MainNotifyIcon.MouseMove += new MouseEventHandler(UpdateTitle);

            UpdateTitle();
            CreateOverlay();

            return true;
        }

        public override void Terminate()
        {
            // Important! Remove event handlers!
            m_host.MainWindow.TextChanged -= UpdateTitle;
            m_host.MainWindow.UIStateUpdated -= UpdateTitle;
            m_host.MainWindow.MainNotifyIcon.MouseMove -= UpdateTitle;
        }

        private void UpdateTitle(object sender, EventArgs e)
        {
            UpdateTitle();
            CreateOverlay();
        }

        private void UpdateTitle()
        {
            bool isOpened = m_host.Database.IsOpen;
            bool isLocked = m_host.MainWindow.IsFileLocked(m_host.MainWindow.DocumentManager.ActiveDocument);

            m_host.MainWindow.MainNotifyIcon.Icon = isLocked ?
                Properties.Resources.bird_lock : 
                Properties.Resources.bird;
            m_host.MainWindow.MainNotifyIcon.Text = String.Format(
                isLocked ? "{0} [locked]{1}{2}" : "{0}{1}{2}",
                PLEASANT_PASSWORD_SERVER, Environment.NewLine, m_host.Database.IOConnectionInfo.UserName);
            
            m_host.MainWindow.Icon = Properties.Resources.bird;
            m_host.MainWindow.Text = m_host.MainWindow.Text.Replace(KEEPASS, PLEASANT_PASSWORD_SERVER);
        }

        void CreateOverlay()
        {
            Taskbar.SetOverlayIcon(m_host.MainWindow.Handle, Properties.Resources.bird.Handle, PLEASANT_PASSWORD_SERVER);
            Taskbar.SetValue(m_host.MainWindow.Handle, 100, 100);
            Taskbar.SetState(m_host.MainWindow.Handle, Taskbar.TaskbarStates.Error);
        }
    }
}
