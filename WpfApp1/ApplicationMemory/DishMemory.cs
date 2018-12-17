using networkDish = AdminPanel.NetworkMiddleware.NetworkData.Dish;
using System.Collections.Generic; 
namespace AdminPanel.ApplicationMemory
{
    public class DishMemory
    {
        private List<networkDish> _dishes;

        public void SetNewDishToMemory(networkDish dish)
        {
            if(this._dishes != null)
            {
                this._dishes.Add(dish);
            }
            else
            {
                this._dishes = new List<networkDish>();
                this._dishes.Add(dish);
            }
        }

        public void SetNewDishesInRange(List<networkDish> dishes)
        {
            if(this._dishes != null)
            {
               this._dishes.AddRange(dishes);
            }
            else
            {
                this._dishes = new List<networkDish>();
                this._dishes.AddRange(dishes);
            }
        }

        public List<networkDish> GetDishes() => this._dishes;
    }
}
