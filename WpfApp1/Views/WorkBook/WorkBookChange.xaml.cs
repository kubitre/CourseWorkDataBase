using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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

namespace AdminPanel.Views.WorkBook
{
    /// <summary>
    /// Interaction logic for WorkBookChange.xaml
    /// </summary>
    public partial class WorkBookChange : MetroWindow
    {
        private ApplicationMemory.MemoryBuild _memory;
        private List<NetworkMiddleware.NetworkData.Cooperator> cooperators;
        private List<NetworkMiddleware.NetworkData.Position> positions;
        private bool isUpdate = false;

        public WorkBookChange()
        {
            InitializeComponent();
            this.cooperators = new List<NetworkMiddleware.NetworkData.Cooperator>();
            this.positions = new List<NetworkMiddleware.NetworkData.Position>();
            DownloadDictionaries();
        }

        public WorkBookChange(Models.WorkBook work)
        {
            InitializeComponent();
            this.cooperators = new List<NetworkMiddleware.NetworkData.Cooperator>();
            this.positions = new List<NetworkMiddleware.NetworkData.Position>();

            DownloadDictionaries();

            
            this.choseCooperator.SelectedItem = work.FirstName;
            this.chosePosition.SelectedItem = work.Position;
            this.chooseDateOrder.SelectedDate = work.OrderDate;
            this.orderNumber_input.Text = work.OrderNumber.ToString();
            this.reason_input.Text = work.Reason;

            this.Title = "Обновление трудовой книжки";
            this.addNewWorkBook.Content = "Обновить";
            isUpdate = true;
        }

        private void DownloadDictionaries()
        {
            var clientNetworkForCooperator = new NetworkMiddleware.Client();
            if(clientNetworkForCooperator.RequestHandle(NetworkMiddleware.NetworkResponseCodes.CooperatorCodes.COOPERATOR_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetworkForCooperator.Response);
                foreach(var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Cooperator>>(response.Reponse))
                {
                    this.cooperators.Add(element);
                    this.choseCooperator.Items.Add(element.FirstName);
                }
            }

            var clientNetworkForPositions = new NetworkMiddleware.Client();
            if(clientNetworkForPositions.RequestHandle(NetworkMiddleware.NetworkResponseCodes.PositionCodes.POSITION_GET_CODE, 100))
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkMiddleware.NetworkData.ReponseAllRequests>(clientNetworkForPositions.Response);
                foreach (var element in Newtonsoft.Json.JsonConvert.DeserializeObject<List<NetworkMiddleware.NetworkData.Position>>(response.Reponse))
                {
                    this.positions.Add(element);
                    this.chosePosition.Items.Add(element.Name);
                }
            }
        }

        private void addNewWorkBook_Click(object sender, RoutedEventArgs e)
        {
            var workBook = new NetworkMiddleware.NetworkData.WorkBook
            {
                FirstName = this.choseCooperator.SelectedItem.ToString(),
                Position = this.chosePosition.SelectedItem.ToString(),
                OrderDate = this.chooseDateOrder.DisplayDate,
                OrderNumber = int.Parse(this.orderNumber_input.Text),
                Reason = this.reason_input.Text
            };

            var clientNetwork = new NetworkMiddleware.Client();
            if (isUpdate)
            {
                if(clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.WorkBookCodes.WORKBOOK_UPDATE_CODE, Newtonsoft.Json.JsonConvert.SerializeObject(workBook)))
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
            else
            {
                if (clientNetwork.RequestHandle(NetworkMiddleware.NetworkResponseCodes.WorkBookCodes.WORKBOOK_CREATE_CODE, Newtonsoft.Json.JsonConvert.SerializeObject(workBook)))
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
        }
    }
}
