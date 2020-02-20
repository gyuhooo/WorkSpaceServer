using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.Hive.Plugin;

namespace RCC.WorkSpaceVR.HivePlugin
{
    public class WorkSpaceServer : PluginBase
    {
        public override string Name
        {
            get { return "WorkSpaceServer"; }
        }
            
        public override void OnCreateGame(ICreateGameCallInfo info)
        {
            this.PluginHost.LogInfo(string.Format("OnCreateGame {0} by user {1}", info.Request.GameId, info.UserId));
            info.Continue(); // same as base.OnCreateGame(info);
        }
    }
}
