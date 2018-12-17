using System.Windows.Controls;

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for ElementMainMneu.xaml
    /// </summary>
    public partial class ElementMainMneu : UserControl
    {
        private ApplicationMemory.MemoryBuild _memory;

        public ElementMainMneu()
        {
            InitializeComponent();
        }

        public void SetMemoryDump(ApplicationMemory.MemoryBuild memory)
        {
            this._memory = memory;
        }
    }
}
