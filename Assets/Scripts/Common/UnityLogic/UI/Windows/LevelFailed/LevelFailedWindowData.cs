using Common.Infrastructure.WindowsManagement;

namespace Common.UnityLogic.UI.Windows.LevelFailed
{
    public struct LevelFailedWindowData : IWindowData
    {
        public string WindowName => "LevelFailed";
        public bool DestroyOnClosing => true;
    }
}