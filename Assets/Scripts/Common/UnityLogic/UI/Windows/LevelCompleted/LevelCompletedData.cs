using Common.Infrastructure.WindowsManagement;

namespace Common.UnityLogic.UI.Windows.LevelCompleted
{
    public struct LevelCompletedData : IWindowData
    {
        public string WindowName => "LevelCompleted";
        public bool DestroyOnClosing => true;
    }
}