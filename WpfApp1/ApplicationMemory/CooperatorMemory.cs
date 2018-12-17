using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using networkCooperator=AdminPanel.NetworkMiddleware.NetworkData.Cooperator;

namespace AdminPanel.ApplicationMemory
{
    public class CooperatorMemory
    {
        private List<networkCooperator> _cooperators;

        public CooperatorMemory()
        {
            this._cooperators = new List<networkCooperator>();
        }

        public void SetNewCooperator(networkCooperator cooperator)
        {
            if (this._cooperators != null)
            {
                this._cooperators.Add(cooperator);
            }
            else
            {
                this._cooperators = new List<networkCooperator>();
                this._cooperators.Add(cooperator);
            }
        }

        public void SetNewCooperators(List<networkCooperator> cooperators)
        {
            if(this._cooperators != null)
            {
                this._cooperators.AddRange(cooperators);
            }
            else
            {
                this._cooperators = new List<networkCooperator>();
                this._cooperators.AddRange(cooperators);
            }
        }

        public List<networkCooperator> GetCooperators() => this._cooperators;
        public networkCooperator GetCooperatorByName(string firstName) => this._cooperators.Where(x => x.FirstName.Equals(firstName)).FirstOrDefault();
        public networkCooperator GetCooperatorByUser(Guid id) => this._cooperators.Where(x => x.Id.Equals(id)).FirstOrDefault();
    }
}
